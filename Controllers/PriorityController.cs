using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Services;

namespace BroadridgeTestProject.Controllers
{
    [Authorize]
    public class PriorityController : ApiController
    {
        private readonly ISettingService _settingService;

        public PriorityController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IEnumerable<PriorityName> Get()
        {
            return _settingService.GetPriorities();
        }       
    }
}