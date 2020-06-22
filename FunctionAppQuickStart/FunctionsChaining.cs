using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FunctionAppQuickStart
{
    // can be cloned from github too
    // https://github.com/Azure/azure-functions-durable-extension/blob/master/samples/precompiled/HelloSequence.cs
    // Function chaining in Durable Functions - Hello sequence sample
    // https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-sequence?tabs=csharp

    public static class FunctionsChaining
    {
        [FunctionName("E1_HelloSequence")]
        public static async Task<List<string>> Run([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            outputs.Add(await context.CallActivityAsync<string>("E1_SayHello", "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>("E1_SayHello", "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>("E1_SayHello_DirectInput", "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }

        [FunctionName("E1_SayHello")]
        public static string SayHello([ActivityTrigger] IDurableActivityContext context)
        {
            var input = context.GetInput<string>();
            return $"hello {input}!";
        }



        /// <summary>
        /// Instead of binding to an IDurableActivityContext, you can bind directly to the type that is passed into the activity function. For example:
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [FunctionName("E1_SayHello_DirectInput")]
        public static string SayHelloDirectInput([ActivityTrigger] string name)
        {
            return $"Hello {name}! (direct)";
        }


       



    }
}
