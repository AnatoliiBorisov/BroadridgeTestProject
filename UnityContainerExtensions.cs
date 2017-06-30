using System;
using Microsoft.Practices.Unity;

namespace BroadridgeTestProject
{
    internal static class UnityContainerExtensions
    {
        public static void RegisterSingleton<TFrom, TTo>(this UnityContainer container) where TTo: TFrom
        {
            //container.RegisterInstance(typeof(T), instance);
            container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
    }
}