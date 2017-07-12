using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using BroadridgeTestProject.Common;
//using BroadridgeTestProject.Binders;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Services;
using BroadridgeTestProject.Wrappers;

namespace BroadridgeTestProject.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Route("api/task/GetTask")]
        [Obsolete("need to use get")]
        [HttpGet]
        public TaskDto GetTask(int taskId)
        {
            return _taskService.GetTask(taskId);
        }

        //TODO: need to add paging and sorting
        [Route("api/task/GetTaskList")]
        [Obsolete("need to use get")]
        [HttpGet]
        public IList<TaskDto> GetTaskList(TaskListType taskListType)
        {
            return _taskService.GetTaskList(taskListType, Consts.AllTasks);
        }

        [Route("api/task/SaveTask")]
        [HttpPost]
        [Obsolete("need to use post")]
        public void SaveTask(/*[ModelBinder(typeof(TaskDtoBinder))]*/ [FromUri] TaskDto taskDto)
        {
            _taskService.SaveTask(taskDto);
        }

        [Route("api/task/CompleteTask")]
        [HttpPost]
        public void CompleteTask(int taskId)
        {
            _taskService.CompleteTask(taskId);
        }

        [Route("api/task/RemoveTask")]
        [HttpPost]
        public void RemoveTask(int taskId)
        {
            _taskService.RemoveTask(taskId);
        }

        [Route("api/task/GetUpdates")]
        [HttpPost]
        public IList<TaskDto> GetUpdates([FromUri] NumberList numberList)
        {
            if (numberList?.nums?.Any() != true)
            {
                return new List<TaskDto>();
            }
            return _taskService.GetUpdates(numberList.nums);            
        }
        
        public void Post([FromUri] TaskDto taskDto)
        {
            _taskService.SaveTask(taskDto);
        }

        public TaskDto Get(int taskId)
        {
            return _taskService.GetTask(taskId);
        }

        //TODO: need to add sorting        
        public IEnumerable<TaskDto> Get(TaskListType taskListType, int? pageNo, Sort? sort = null, string sortColumn = null)
        {
            if (pageNo == null)
            {
                pageNo = 1;
            }

            return _taskService.GetTaskList(taskListType, pageNo.Value);
        }

        [Route("api/taskListSettigs")]
        [HttpGet]
        public TaskListSettigs GetTaskListSettigs()
        {
            return _taskService.GetTaskListSettigs();
        }
    }
}
