using System.Data.Entity;
using BroadridgeTestProject.Common;
using BroadridgeTestProject.Models;
using BroadridgeTestProject.Services;
using Microsoft.Practices.Unity;

namespace BroadridgeTestProject.DAL
{
    internal class BroadridgeInitializer : DropCreateDatabaseIfModelChanges<BroadridgeContext>
    {
        protected override void Seed(BroadridgeContext context)
        {
            var settingService = IoCContainer.Container.Resolve<ISerializationService>();

            var dateFormat1 = new DateFormat();
            dateFormat1.DateTimeFormat = "dd.MM.yyyy HH:mm:ss";
            dateFormat1.ShortDateFormat = "dd.MM.yyyy";
            context.DateFormats.Add(dateFormat1);

            var dateFormat2 = new DateFormat();
            dateFormat2.DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
            dateFormat2.ShortDateFormat = "dd-MM-yyyy";
            context.DateFormats.Add(dateFormat2);

            var settingDateTimeFormat = new Setting();
            settingDateTimeFormat.Name = SettingNames.DateTimeFormat;
            settingDateTimeFormat.Value = settingService.SerializeObject(1);

            context.Settings.Add(settingDateTimeFormat);

            var altRowsColorFormat = new Setting();
            altRowsColorFormat.Name = SettingNames.AltRowsColor;
            altRowsColorFormat.Value = settingService.SerializeObject(Color.Silver);

            context.Settings.Add(altRowsColorFormat);

            context.SaveChanges();
        }
    }
}