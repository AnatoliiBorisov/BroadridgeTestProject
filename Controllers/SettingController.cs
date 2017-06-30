using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Services;

namespace BroadridgeTestProject.Controllers
{   
    public class SettingController : ApiController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #region old version
        [Route("api/setting/GetSettingDto")]
        [HttpGet]
        [Obsolete]
        public SettingDto GetSettingDto()
        {
            return _settingService.GetSettingDto();
        }

        [Route("api/setting/GetColorNames")]
        [HttpGet]
        [Obsolete]
        public IList<ColorName> GetColorNames()
        {
            return _settingService.GetColorNames();
        }

        [Route("api/setting/GetPriorities")]
        [HttpGet]
        [Obsolete]
        public IList<PriorityName> GetPriorities()
        {
            return _settingService.GetPriorities();
        }

        [Route("api/Setting/SaveSetting")]
        [HttpPost]
        [Obsolete]
        public void SaveSettingDto([FromUri] SettingDto settingDto)
        {
            _settingService.SaveSettingDto(settingDto);
        }
        #endregion old version

        [HttpGet]
        public SettingDto Get()
        {
            return _settingService.GetSettingDto();
        }

        [HttpPost]
        public void Post([FromUri] SettingDto settingDto)
        {
            _settingService.SaveSettingDto(settingDto);
        }
    }
}
