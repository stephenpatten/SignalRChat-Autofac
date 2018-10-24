﻿
namespace SignalRChat.Web.Bootstrapper
{
    using System.Reflection;

    using Autofac;
    using Autofac.Integration.SignalR;

    using SignalRChat.Web.Bootstrapper.Modules;

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