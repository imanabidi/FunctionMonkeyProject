using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using ServerlessBlog.Application;
using ServerlessBlog.Commands;

namespace ServerlessBlog
{
    public class FunctionAppConfiguration : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder builder)
        {
           builder.Setup(  (collection, registry) =>
           {
               collection.AddApplication(registry);

           } ).Functions( function => function.HttpRoute("/api/v1/post", 
               route =>  route.HttpFunction<AddPostCommand>(HttpMethod.Post)));
        }
    }
}
