using System.Collections.Generic;
using System.Web.Http;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Services;

namespace BroadridgeTestProject.Controllers
{
    public class ColorNameController : ApiController
    {
        private readonly ISettingService _settingService;

        public ColorNameController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IEnumerable<ColorName> Get()
        {
            return _settingService.GetColorNames();
        }
    }
}
