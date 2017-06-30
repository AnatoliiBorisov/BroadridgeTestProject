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

            var settingDateTimeFormat = new Setting();
            settingDateTimeFormat.Name = SettingNames.DateTimeFormat;
            settingDateTimeFormat.Value = settingService.SerializeObject("dd.MM.yyyy HH:mm:ss");

            context.Settings.Add(settingDateTimeFormat);

            var altRowsColorFormat = new Setting();
            altRowsColorFormat.Name = SettingNames.AltRowsColor;
            altRowsColorFormat.Value = settingService.SerializeObject(Color.Gray);

            context.Settings.Add(altRowsColorFormat);

            context.SaveChanges();
        }
    }
}