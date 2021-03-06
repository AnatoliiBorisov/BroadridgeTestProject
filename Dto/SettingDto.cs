﻿using BroadridgeTestProject.Common;

namespace BroadridgeTestProject.Dto
{
    public class SettingDto
    {
        public int DateFormateId { get; set; }

        public string DateTimeFormat { get; set; }

        public string DateFormat { get; set; }

        public Color AltRowsColor { get; set; }

        public string AltRowsColorName { get; set; }

        public int PageSize { get; set; }
    }
}