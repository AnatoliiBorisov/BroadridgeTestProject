using System.Collections.Generic;
using BroadridgeTestProject.Common;

namespace BroadridgeTestProject.Providers
{
    internal interface ISettingProvider
    {
        void SaveSettings(IDictionary<SettingNames, string> values);

        IDictionary<SettingNames, string> GetSettings();
    }
}