using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace MvcUnityIoC
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = IoCManager.Container;

            RegisterTypes(container);
            return container;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            // Register here your custom types
        }
    }
}