using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ActionPlugin  : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the execution context service from the service provider.
            IPluginExecutionContext PluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            // Obtain the Organization Service factory service from the service provider
            IOrganizationServiceFactory ServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            // Use the factory to generate the Organization Service.
            IOrganizationService OrganizationService = ServiceFactory.CreateOrganizationService(PluginExecutionContext.UserId);
            // Generate the CrmContext to use with LINQ etc
            var CrmContext = new OrganizationServiceContext(OrganizationService);

            var target = PluginExecutionContext.InputParameters["Target"] as Entity; // might be EntityReference
                                                                                     // input 

            var workorderTypeInput =   (int)PluginExecutionContext.InputParameters["WorkorderType"];//target.GetAttributeValue<OptionSetValue>("log_typeofworkorder");// ==> Cant set picklist as input: 
            var statusInput = (int)PluginExecutionContext.InputParameters["StatusInput"];// target.GetAttributeValue<OptionSetValue>("statuscode"); // 



            var qWorkorder = new QueryExpression(target.LogicalName);// { ColumnSet = new ColumnSet("workordertype") };
            //qWorkorder.Criteria.AddCondition($"{target.LogicalName}id", ConditionOperator.Equal, target.Id);
            qWorkorder.Criteria.AddCondition("log_installationid", ConditionOperator.Equal, target.Id);
            qWorkorder.Criteria.AddCondition("log_typeofworkorder", ConditionOperator.Equal, workorderTypeInput); // Type: installation
            qWorkorder.Criteria.AddCondition("statuscode", ConditionOperator.Equal, statusInput); // Active
            var getTargetWithType = OrganizationService.RetrieveMultiple(qWorkorder).Entities;


            PluginExecutionContext.OutputParameters["Exists"] = getTargetWithType.Count > 0;

            //var arg1 = PluginExecutionContext.InputParameters["NewArgument"] as string;
            //var arg2 = PluginExecutionContext.InputParameters["NewArgument2"] as string;
            //int result = Int32.Parse(arg1);
            //int result2 = Int32.Parse(arg2);
            //PluginExecutionContext.OutputParameters["NewArgument1"] = result + result2 == 13 ? true : (object)false;


        }
    }
}
