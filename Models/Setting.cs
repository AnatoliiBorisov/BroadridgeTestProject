using BroadridgeTestProject.Common;

namespace BroadridgeTestProject.Models
{
    public class Setting
    {
        public int SettingID { get; set; }

        public SettingNames Name { get; set; }

        public string Value { get; set; }
    }
}