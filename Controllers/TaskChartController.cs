using System.Collections.Generic;
using System.Web.Http;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Services;

namespace BroadridgeTestProject.Controllers
{
    [Authorize]
    public class TaskChartController : ApiController
    {
        private readonly ITaskService _taskService;

        public TaskChartController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IEnumerable<TaskChartDto> Get()
        {
            return _taskService.GetCountTasksByPriority();
        }
    }
}
