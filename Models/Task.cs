using System;
using BroadridgeTestProject.Common;

namespace BroadridgeTestProject.Models
{
    public class Task
    {
        public int TaskID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public DateTime TimeCreate { get; set; }

        public DateTime TimeComplete { get; set; }
    }
}