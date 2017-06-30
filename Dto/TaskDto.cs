using System;
//using BroadridgeTestProject.Binders;
using BroadridgeTestProject.Common;

namespace BroadridgeTestProject.Dto
{
    //[System.Web.Http.ModelBinding.ModelBinder(typeof(TaskDtoBinder))]
    public class TaskDto
    {
        public int TaskID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public DateTime TimeCreate { get; set; }

        public DateTime TimeToComplete { get; set; }
    }    
}