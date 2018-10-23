using Autofac.Integration.SignalR;
using SignalRChat.Web.Bootstrapper.Modules;
using System.Reflection;
using Autofac;

namespace SignalRChat.Web.Bootstrapper
{
    public class IocConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();
 
            builder.RegisterModule(new HubModule(Assembly.Load("SignalRChat.Web")));
 
            builder.RegisterModule(new MassTransitModule(Assembly.Load("SignalRChat.Web")));
 
            return builder.Build();
        }
    }
}