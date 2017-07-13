using System.Collections.Generic;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Dto;

namespace BroadridgeTestProject.Services
{
    public interface ITaskService
    {
        TaskDto GetTask(int taskId);

        void SaveTask(TaskDto taskDto);

        IList<TaskDto> GetTaskList(TaskListType taskListType, int pageNo, Sort? sort = null, string sortColumn = null);

        void CompleteTask(int taskId);

        void RemoveTask(int taskId);

        IList<TaskDto> GetUpdates(IEnumerable<int> taskIds);

        TaskListSettigs GetTaskListSettigs();

        IEnumerable<TaskChartDto> GetCountTasksByPriority();
    }
}