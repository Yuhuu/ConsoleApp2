using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace GetInstallationWorkorders
{
    public class CustomWorkflow : CodeActivity
    {
        [Input("Account Name")]
        [Default("Test Account: {575A8B41-F8D7-4DCE-B2EA-3FFDE936AB1B}")]
        public InArgument<OptionSetValue> WorkorderType { get; set; }

        [Input("Task Subject")]
        [Default("Task related to account {575A8B41-F8D7-4DCE-B2EA-3FFDE936AB1B}")]
        public InArgument<OptionSetValue> StatusCode { get; set; }

        [Input("Updated Task Subject")]
        [Default("UPDATED: Task related to account {575A8B41-F8D7-4DCE-B2EA-3FFDE936AB1B}")]
        public OutArgument<bool> WorkflowsExists { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var myinput = WorkorderType.Get(context);
        }
    }
}
