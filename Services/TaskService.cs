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

        private readonly Dictionary<string, Func<Task, object>> _selectors;

        public TaskService(ITaskProvider taskProvider,
                           ISettingService settingService)
        {
            _taskProvider = taskProvider;

            _settingService = settingService;

            _selectors = new Dictionary<string, Func<Task, object>>
            {
                {"Name", FuncTaskNameKeySelector},
                {"Priority", FuncTaskPriorityKeySelector },
                {"TimeCreate", FuncTaskTimeCreateKeySelector  },
                {"TimeToComplete", FuncTaskTimeToCompleteKeySelector },
                {"Status", FuncTaskStatusKeySelector }
            };
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

        public IList<TaskDto> GetTaskList(TaskListType taskListType, int pageNo, Sort? sort = null, string sortColumn = null)
        {
            var taskBatchSize = _settingService.GetTaskBatchSize();

            IEnumerable<Task> taskList = _taskProvider.GetTaskList(taskListType);

            Func<Task, object> funcTaskKeySelector = null;
            if (sort != null 
                && !string.IsNullOrEmpty(sortColumn)
                && _selectors.TryGetValue(sortColumn, out funcTaskKeySelector))
            {
                taskList = sort == Sort.Asc ? taskList.OrderBy(funcTaskKeySelector) 
                                            : taskList.OrderByDescending(funcTaskKeySelector);
            }

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

        #region selectors for sort table
        private object FuncTaskNameKeySelector(Task task)
        {
            return task.Name;
        }

        private object FuncTaskPriorityKeySelector(Task task)
        {
            return task.Priority;
        }

        private object FuncTaskTimeCreateKeySelector(Task task)
        {
            return task.TimeCreate;
        }

        private object FuncTaskTimeToCompleteKeySelector(Task task)
        {
            return task.TimeComplete;
        }

        private object FuncTaskStatusKeySelector(Task task)
        {
            return task.TimeComplete > DateTime.Now ? TaskStatus.Active : TaskStatus.Complete;
        }
        #endregion selectors for sort table
    }
}