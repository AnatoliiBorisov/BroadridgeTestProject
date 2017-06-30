using System;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace BroadridgeTestProject
{
    internal class IoCContainer : ScopeContainer, IDependencyResolver
    {
        private static UnityContainer _container;

        public static UnityContainer Container
        {
            get { return _container; }

            set
            {
                if (_container != null)
                {
                    throw new TypeInitializationException("Container is initialized", null);
                }

                _container = value;
            }
        }

        public IoCContainer(IUnityContainer container) : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new ScopeContainer(child);
        }
    }
}