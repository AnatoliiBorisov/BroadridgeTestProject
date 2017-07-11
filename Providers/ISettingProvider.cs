using System.Collections.Generic;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Models;

namespace BroadridgeTestProject.Providers
{
    internal interface ISettingProvider
    {
        void SaveSettings(IDictionary<SettingNames, string> values);

        IDictionary<SettingNames, string> GetSettings();

        DateFormat GetDateFormat(int dateFormatId);

        IEnumerable<DateFormat> GetDateFormates();
    }
}