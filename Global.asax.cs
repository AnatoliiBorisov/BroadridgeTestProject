using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using BroadridgeTestProject.Binders;
using BroadridgeTestProject.Controllers;
using BroadridgeTestProject.Dto;
using BroadridgeTestProject.Providers;
using BroadridgeTestProject.Services;
using BroadridgeTestProject.Cashe;
using Microsoft.Practices.Unity;

namespace BroadridgeTestProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeUnityContainer();
        }

        private void InitializeUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterSingleton<ISerializationService, SerializationService>();
            container.RegisterSingleton<IApplicationCasheService, ApplicationCasheService>();

            container.RegisterSingleton<ISettingService, SettingService>();
            container.RegisterSingleton<ISettingProvider, SettingProvider>();

            container.RegisterSingleton<ITaskService, TaskService>();
            container.RegisterSingleton<ITaskProvider, TaskProvider>();

            container.RegisterType<HomeController>();
            container.RegisterType<SettingController>();
            container.RegisterType<ColorNameController>();
            container.RegisterType<PriorityController>();


            container.RegisterType<TaskController>();            

            IoCContainer.Container = container;

            GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(container);
        }       
    }
}
