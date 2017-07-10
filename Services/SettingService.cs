using System;
using System.Collections.Generic;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Providers;
using BroadridgeTestProject.Cashe;

namespace BroadridgeTestProject.Services
{
    internal class SettingService : ISettingService
    {
        private readonly ISettingProvider _settingProvider;

        private readonly ISerializationService _serializationService;

        private readonly IApplicationCasheService _applicationCasheService;

        private const Color ColorDefault = Color.Gray;

        private const string DateTimeFormatDefault = "dd.MM.yyyy HH:mm:ss";

        public SettingService(ISerializationService serializationService,
                              IApplicationCasheService applicationCasheService,
                              ISettingProvider settingProvider)
        {
            _settingProvider = settingProvider;

            _applicationCasheService = applicationCasheService;

            _serializationService = serializationService;
        }

        //TODO: need to get settings by user
        public SettingDto GetSettingDto()
        {
            var settingDto = _applicationCasheService.GetValue(ApplicationCasheNames.SettingDto);

            if (settingDto == null)
            {
                var settings = _settingProvider.GetSettings();

                var altRowsColor = settings.ContainsKey(SettingNames.AltRowsColor)
                    ? _serializationService.DeserializeObject<Color>(settings[SettingNames.AltRowsColor])
                    : ColorDefault;

                var dateTimeFormat = settings.ContainsKey(SettingNames.DateTimeFormat)
                    ? _serializationService.DeserializeObject<string>(settings[SettingNames.DateTimeFormat])
                    : DateTimeFormatDefault;

                settingDto = new SettingDto
                {
                    AltRowsColor = altRowsColor,
                    AltRowsColorName = altRowsColor.ToString(),
                    DateTimeFormat = dateTimeFormat,
                    DateFormat = "DD-MM-YYYY" //TODO: need to calclulate from DateTimeFormat
                };

                _applicationCasheService.AddValue(ApplicationCasheNames.SettingDto, settingDto);
            }

            return (SettingDto) settingDto;
        }

        //TODO: need to save settings by user
        public void SaveSettingDto(SettingDto settingDto)
        {
            if (settingDto == null)
            {
                throw new ArgumentNullException(nameof(settingDto));
            }

            var colorSerialized = _serializationService.SerializeObject(settingDto.AltRowsColor);
            var dateTimeFormatSerialized = _serializationService.SerializeObject(settingDto.DateTimeFormat);

            var settingNames = new Dictionary<SettingNames, string>(2);

            settingNames.Add(SettingNames.DateTimeFormat, dateTimeFormatSerialized);
            settingNames.Add(SettingNames.AltRowsColor, colorSerialized);

            _settingProvider.SaveSettings(settingNames);

            _applicationCasheService.RemoveValue(ApplicationCasheNames.SettingDto);
        }

        public IList<ColorName> GetColorNames()
        {
            var colorNames = new List<ColorName>();

            colorNames.Add(CreateColorName(Color.Red));
            colorNames.Add(CreateColorName(Color.Gray));
            colorNames.Add(CreateColorName(Color.Green));
            colorNames.Add(CreateColorName(Color.Blue));
            colorNames.Add(CreateColorName(Color.WhiteSmoke));
            colorNames.Add(CreateColorName(Color.Silver));

            return colorNames;
        }

        public IList<PriorityName> GetPriorities()
        {
            var priorities = new List<PriorityName>();

            priorities.Add(CreatePriorityName(Priority.Low));
            priorities.Add(CreatePriorityName(Priority.Normal));
            priorities.Add(CreatePriorityName(Priority.Hight));
            
            return priorities;
        }

        public int GetTaskBatchSize()
        {
            //TODO: Move to user settings
            return 8;
        }

        //TODO: to generic
        internal ColorName CreateColorName(Color color)
        {
            return new ColorName(color, color.ToString());
        }       

        //TODO: to generic
        internal PriorityName CreatePriorityName(Priority priority)
        {
            return new PriorityName(priority, priority.ToString());
        }
    }
}