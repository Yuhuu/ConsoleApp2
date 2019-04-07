using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ActionPlugin  : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            // Obtain the execution context service from the service provider.
            var PluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            // Obtain the Organization Service factory service from the service provider
            var ServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            // Use the factory to generate the Organization Service.
            var OrganizationService = ServiceFactory.CreateOrganizationService(PluginExecutionContext.UserId);
            // Generate the CrmContext to use with LINQ etc
            var CrmContext = new OrganizationServiceContext(OrganizationService);

            var target = PluginExecutionContext.InputParameters["Target"] as Entity; // might be EntityReference
            var arg1 = PluginExecutionContext.InputParameters["NewArgument"] as string;
            var arg2 = PluginExecutionContext.InputParameters["NewArgument"] as string;
            PluginExecutionContext.OutputParameters["NewArgument"] = arg1 + arg2;

        }
    }
}
