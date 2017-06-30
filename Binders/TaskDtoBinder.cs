//using System;
//using System.Globalization;
//using System.Web.Http.Controllers;
//using BroadridgeTestProject.Common;
//using BroadridgeTestProject.Dto;
//using BroadridgeTestProject.Services;
//using Microsoft.Practices.Unity;

//namespace BroadridgeTestProject.Binders
//{
//    public class TaskDtoBinder : System.Web.Http.ModelBinding.IModelBinder
//    {
//        private static readonly object SyncObject = new object();

//        private ISettingService _settingService;

//        private ISettingService SettingService
//        {
//            get
//            {
//                if (_settingService == null)
//                {
//                    lock (SyncObject)
//                    {
//                        if (_settingService == null)
//                        {
//                            _settingService = IoCContainer.Container.Resolve<ISettingService>();
//                        }
//                    }
//                }

//                return _settingService;
//            }            
//        }

//        public bool BindModel(HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
//        {
//            var taskDto = new TaskDto();

//            var taskID = bindingContext.ValueProvider.GetValue("TaskID");
//            if (taskID != null)
//            {
//                taskDto.TaskID = int.Parse(taskID.AttemptedValue);
//            }

//            var name = bindingContext.ValueProvider.GetValue("Name");
//            if (name != null)
//            {
//                taskDto.Name = name.AttemptedValue;
//            }

//            var description = bindingContext.ValueProvider.GetValue("Description");
//            if (description != null)
//            {
//                taskDto.Name = description.AttemptedValue;
//            }

//            var priority = bindingContext.ValueProvider.GetValue("Priority");
//            if (priority != null)
//            {
//                taskDto.Priority = (Priority)Enum.Parse(typeof(Priority), priority.AttemptedValue);
//            }

//            var timeCreate = bindingContext.ValueProvider.GetValue("TimeCreate");
//            var settingDto = SettingService.GetSettingDto();
//            if (timeCreate != null)
//            {
//                taskDto.TimeCreate = DateTime.ParseExact(timeCreate.AttemptedValue, settingDto.DateTimeFormat, CultureInfo.InvariantCulture);
//            }

//            var timeToComplete = bindingContext.ValueProvider.GetValue("TimeToComplete");
//            if (timeToComplete != null)
//            {
//                taskDto.TimeToComplete = TimeSpan.Parse(timeToComplete.AttemptedValue);
//            }

//            bindingContext.Model = taskDto;

//            return true;
//        }
//    }
//}