
namespace SignalRChat.Web.Bootstrapper.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Autofac;
    using Autofac.Integration.SignalR;

    public class HubModule : Module
    {
        private readonly System.Reflection.Assembly[] _assembliesToScan;
 
        public HubModule(params System.Reflection.Assembly[] assembliesToScan)
        {
            _assembliesToScan = assembliesToScan;
        }
 
        protected override void Load(ContainerBuilder builder)
        {
            // Register your SignalR hubs.
            builder.RegisterHubs(_assembliesToScan);
        }
    }
}
