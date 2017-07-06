using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.DAL;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Models;

namespace BroadridgeTestProject.Providers
{
    internal class TaskProvider : ITaskProvider
    {
        public Task GetTask(int taskId)
        {
            using (var context = new BroadridgeContext())
            {
                return context.Tasks.FirstOrDefault(x => x.TaskID == taskId);
            }
        }

        public void SaveTask(Task task)
        {
            using (var context = new BroadridgeContext())
            {
                if (task.TaskID == 0)
                {
                    task.TimeCreate = DateTime.Now;
                    
                    context.Tasks.Add(task);
                }
                else
                {
                    var savedTask = context.Tasks.FirstOrDefault(x => x.TaskID == task.TaskID);
                    if (savedTask != null)
                    {
                        savedTask.Name = task.Name;
                        savedTask.Description = task.Description;
                        savedTask.Priority = task.Priority;
                    }                    
                }

                context.SaveChanges();
            }
        }

        public IList<Task> GetTaskList(TaskListType taskListType)
        {
            using (var context = new BroadridgeContext())
            {
                return context.Tasks.Where(x => taskListType == TaskListType.All 
                                                || (taskListType == TaskListType.Active && x.TimeComplete > DateTime.Now)
                                                || (taskListType == TaskListType.Completed && x.TimeComplete <= DateTime.Now))
                                    .ToList();
            }
        }

        public IList<Task> GetTaskList(IEnumerable<int> taskListType)
        {
            using (var context = new BroadridgeContext())
            {
                return context.Tasks.Where(x => taskListType.Contains(x.TaskID))
                                    .ToList();
            }
        }

        public int GetTaskCount()
        {
            using (var context = new BroadridgeContext())
            {
                return context.Tasks.Count();
            }
        }

        public IEnumerable<TaskChartDto> GetCountTasksByPriority()
        {
            using (var context = new BroadridgeContext())
            {
                return context.Tasks.GroupBy(x => x.Priority)
                                    .Select(x => new TaskChartDto
                                    {
                                        Priority = x.Key,
                                        PriorityName = x.Key.ToString(),
                                        Count = x.Count()
                                    })
                                    .ToList();
            }
        }

        public void CompleteTask(int taskId)
        {
            using (var context = new BroadridgeContext())
            {
                var task = context.Tasks.FirstOrDefault(x => x.TaskID == taskId);

                if (task != null)
                {
                    task.TimeComplete = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }

        public void RemoveTask(int taskId)
        {
            using (var context = new BroadridgeContext())
            {
                var task = context.Tasks.FirstOrDefault(x => x.TaskID == taskId);

                if (task != null)
                {
                    context.Tasks.Remove(task);

                    context.SaveChanges();
                }
            }
        }
    }
}