using System.Collections.Generic;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Dto;

namespace BroadridgeTestProject.Services
{
    public interface ISettingService
    {
        SettingDto GetSettingDto();

        void SaveSettingDto(SettingDto settingDto);

        IList<ColorName> GetColorNames();

        IList<PriorityName> GetPriorities();

        int GetTaskBatchSize();
    }
}