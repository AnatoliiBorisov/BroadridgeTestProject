using System.Collections.Generic;
using System.Linq;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.DAL;
using BroadridgeTestProject.Models;

namespace BroadridgeTestProject.Providers
{
    internal class SettingProvider : ISettingProvider
    {
        public void SaveSettings(IDictionary<SettingNames, string> values)
        {
            using (var context = new BroadridgeContext())
            {
                foreach (var value in values)
                {
                    var setting = context.Settings.FirstOrDefault(x => x.Name == value.Key);

                    if (setting == null)
                    {
                        setting = new Setting();
                        setting.Name = value.Key;

                        context.Settings.Add(setting);
                    }

                    setting.Value = value.Value;
                }

                context.SaveChanges();
            }
        }

        public IDictionary<SettingNames, string> GetSettings()
        {
            using (var context = new BroadridgeContext())
            {
                return context.Settings.ToDictionary(x => x.Name, x => x.Value);
            }
        }

        public DateFormat GetDateFormat(int dateFormatId)
        {
            using (var context = new BroadridgeContext())
            {
                return context.DateFormats.FirstOrDefault(x => x.DateFormatID == dateFormatId);
            }
        }

        public IEnumerable<DateFormat> GetDateFormates()
        {
            using (var context = new BroadridgeContext())
            {
                return context.DateFormats.ToList();
            }
        }
    }
}