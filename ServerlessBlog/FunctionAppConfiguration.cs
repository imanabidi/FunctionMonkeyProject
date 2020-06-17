using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using AutoMapper;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.FluentValidation;
using ServerlessBlog.Application;
using ServerlessBlog.Commands;

namespace ServerlessBlog
{
    public class FunctionAppConfiguration : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder builder)
        {
            builder
                .Setup((collection, registry) =>
                {
                    collection
                        .AddApplication(registry)
                        .AddAutoMapper(typeof(SubsystemRegistration));
                })
                .AddFluentValidation()
                .Functions(function => function
                    .HttpRoute("/" +
                               "v1/post", route => route
                        .HttpFunction<AddPostCommand>(HttpMethod.Post)
                        .HttpFunction<GetPostQuery>("/{postId}", HttpMethod.Get))
                    )

                ;
        }
    }
}
