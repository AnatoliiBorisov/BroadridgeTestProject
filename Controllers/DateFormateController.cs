using System.Collections.Generic;
using System.Web.Http;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Services;

namespace BroadridgeTestProject.Controllers
{
    public class DateFormateController : ApiController
    {
        private readonly ISettingService _settingService;

        public DateFormateController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // GET api/<controller>
        public IEnumerable<DateFormateDto> Get()
        {
            return _settingService.GetDateFormates();
        }        
    }
}