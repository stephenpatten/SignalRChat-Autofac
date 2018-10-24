
namespace SignalRChat.Web.Bootstrapper
{
    using Autofac;
    using Autofac.Integration.SignalR;

    using MassTransit;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Microsoft.AspNet.SignalR.Infrastructure;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Call our IoC static helper method to start the typical Autofac SignalR setup
            var container = IocConfig.RegisterDependencies();

            // Get your HubConfiguration. In OWIN, we create one rather than using GlobalHost
            var hubConfig = new HubConfiguration();

            // Sets the dependency resolver to be autofac.
            hubConfig.Resolver = new AutofacDependencyResolver(container);

            // OWIN SIGNALR SETUP:

            // Register the Autofac middleware FIRST, then the standard SignalR middleware.
            app.UseAutofacMiddleware(container);
            app.MapSignalR("/signalr", hubConfig);

            // http://docs.autofac.org/en/latest/integration/signalr.html
            // https://stackoverflow.com/questions/33511095/how-to-configure-autofac-and-signalr-in-a-mvc-5-application
            // There's not a lot of documentation or discussion for owin getting the hubcontext
            // Got this from here: https://stackoverflow.com/questions/29783898/owin-signalr-autofac
            // http://www.ilove-it.com/post/2017/01/05/autofac-with-signalr-and-webapi-in-owin-registration-solution-without-using-container-update
            var builder = new ContainerBuilder();

            var connManager = hubConfig.Resolver.Resolve<IConnectionManager>();

            builder.RegisterInstance(connManager)
                .As<IConnectionManager>()
                .SingleInstance();

            builder.Update(container);
 
            // Starts the bus.
            container.Resolve<IBusControl>().Start();
        }
    }
}
