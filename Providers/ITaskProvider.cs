using System.Collections.Generic;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Models;

namespace BroadridgeTestProject.Providers
{
    public interface ITaskProvider
    {
        Task GetTask(int taskId);

        void SaveTask(Task task);

        IList<Task> GetTaskList(TaskListType taskListType);

        void CompleteTask(int taskId);

        void RemoveTask(int taskId);

        IList<Task> GetTaskList(IEnumerable<int> taskListType);

        int GetTaskCount();

        IEnumerable<TaskChartDto> GetCountTasksByPriority();
    }
}