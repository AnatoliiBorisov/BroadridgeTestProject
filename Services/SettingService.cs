using System;
using System.Collections.Generic;
using System.Linq;
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

        private const int DateTimeFormatDefault = 1;

        private const int PageSizeDefault = 8;

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

                var dateFormateId = settings.ContainsKey(SettingNames.DateTimeFormat)
                    ? _serializationService.DeserializeObject<int>(settings[SettingNames.DateTimeFormat])
                    : DateTimeFormatDefault;

                var pageSize = settings.ContainsKey(SettingNames.PageSize)
                    ? _serializationService.DeserializeObject<int>(settings[SettingNames.PageSize])
                    : PageSizeDefault;

                var dateFormate = _settingProvider.GetDateFormat(dateFormateId);

                settingDto = new SettingDto
                {
                    DateFormateId = dateFormateId,
                    AltRowsColor = altRowsColor,
                    AltRowsColorName = altRowsColor.ToString(),
                    DateTimeFormat = dateFormate.DateTimeFormat,
                    DateFormat = dateFormate.ShortDateFormat.ToUpperInvariant(),
                    PageSize = pageSize
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
            var dateTimeFormatSerialized = _serializationService.SerializeObject(settingDto.DateFormateId);
            var pageSizeSerialized = _serializationService.SerializeObject(settingDto.PageSize);

            var settingNames = new Dictionary<SettingNames, string>(2);

            settingNames.Add(SettingNames.DateTimeFormat, dateTimeFormatSerialized);
            settingNames.Add(SettingNames.AltRowsColor, colorSerialized);
            settingNames.Add(SettingNames.PageSize, pageSizeSerialized);

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
            return GetSettingDto().PageSize;
        }

        public IEnumerable<DateFormateDto> GetDateFormates()
        {
            return _settingProvider.GetDateFormates()
                                   .Select(x => new DateFormateDto
                                                    {
                                                        DateFormatID = x.DateFormatID,
                                                        DateTimeFormate = x.DateTimeFormat
                                                    })
                                   .ToList();
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