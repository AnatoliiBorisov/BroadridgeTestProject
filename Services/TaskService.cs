using System;
using System.Collections.Generic;
using System.Linq;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Models;
using BroadridgeTestProject.Providers;

namespace BroadridgeTestProject.Services
{
    internal class TaskService : ITaskService
    {
        private readonly ITaskProvider _taskProvider;

        private readonly ISettingService _settingService;

        public TaskService(ITaskProvider taskProvider,
                           ISettingService settingService)
        {
            _taskProvider = taskProvider;

            _settingService = settingService;
        }

        public TaskDto GetTask(int taskId)
        {
            var task = _taskProvider.GetTask(taskId);

            return ConvertToDto(task);
        }       

        public void SaveTask(TaskDto taskDto)
        {
            var task = ConvertFromDto(taskDto);

            _taskProvider.SaveTask(task);
        }

        public IList<TaskDto> GetTaskList(TaskListType taskListType, int pageNo)
        {
            var taskBatchSize = _settingService.GetTaskBatchSize();

            IEnumerable<Task> taskList = _taskProvider.GetTaskList(taskListType);
            if (pageNo != Consts.AllTasks)
            {
                taskList = taskList.Skip((pageNo - 1) * taskBatchSize).Take(taskBatchSize);
            }

            return taskList.Select(ConvertToDto).ToList();
        }

        public void CompleteTask(int taskId)
        {
            _taskProvider.CompleteTask(taskId);
        }

        public void RemoveTask(int taskId)
        {
            _taskProvider.RemoveTask(taskId);
        }

        public IList<TaskDto> GetUpdates(IEnumerable<int> taskIds)
        {
            return _taskProvider.GetTaskList(taskIds)
                                .Select(ConvertToDto)
                                .ToList(); 
        }

        public TaskListSettigs GetTaskListSettigs()
        {
            return new TaskListSettigs
            {
                ItemsPerPage = _settingService.GetTaskBatchSize(),

                TotalItems = _taskProvider.GetTaskCount()
            };
        }

        public IEnumerable<TaskChartDto> GetCountTasksByPriority()
        {
            return _taskProvider.GetCountTasksByPriority();
        }


        //TODO: need to use automapper
        internal TaskDto ConvertToDto(Task task)
        {
            if (task == null)
            {
                return null;
            }

            return new TaskDto
            {
                TaskID = task.TaskID,
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority,
                TimeCreate = task.TimeCreate,
                TimeToComplete = task.TimeComplete
            };
        }

        //TODO: need to use automapper
        internal Task ConvertFromDto(TaskDto taskDto)
        {
            if (taskDto == null)
            {
                return null;
            }

            return new Task
            {
                TaskID = taskDto.TaskID,
                Name = taskDto.Name,
                Description = taskDto.Description,
                Priority = taskDto.Priority,
                TimeCreate = taskDto.TimeCreate,
                TimeComplete = taskDto.TimeToComplete
            };
        }
    }
}