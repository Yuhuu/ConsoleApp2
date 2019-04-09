using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ProgramFetchQuery
    {


        /*
          * Retrieve all accounts owned by the user with read access rights to the accounts 
          * and the name contains customer
          * then it is printed out in Debug output.
          **/
        public class MyPlugin : IPlugin
        {

            public void Execute(IServiceProvider serviceProvider)
            {
                var TARGET = new Entity("workorder");
                var input1 = TARGET.GetAttributeValue<OptionSetValue>("field1");
                var input2 = TARGET.GetAttributeValue<OptionSetValue>("field2");
                var output = input1.Value == input2.Value;
                TARGET["outputparametersBool"] = output;
            }
        }
        public static void Run(OrganizationServiceProxy service)
        {

            // Retrieve all accounts owned by the user with read access rights to the accounts and   
            // where the last name of the user is not Cannon.   
            string fetch = @"  
                <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                  <entity name='log_workorders'>
                    <attribute name='log_workordersid' />
                    <attribute name='log_typeofworkorder' />
                    <attribute name='statuscode' />
                    <attribute name='log_installationid' />
                    <order attribute='log_typeofworkorder' descending='true' />
                    <filter type='and'>
                      <condition attribute='statuscode' operator='eq' value='Input statuscode' />
                      <condition attribute='log_typeofworkorder' operator='eq' value='log_typeofworkorder' />
                    </filter>
                    <link-entity name='log_installation' from='log_installationid' to='log_installationid' alias='a_83d1fdd92da7e61180cf005056a6638b' />
                  </entity>
                </fetch>";




            EntityCollection result = service.RetrieveMultiple(new FetchExpression(fetch));
            foreach (var c in result.Entities)
            {
                Debug.WriteLine(c.Attributes["log_workordersid"].ToString());
            }
        }
    }
}

