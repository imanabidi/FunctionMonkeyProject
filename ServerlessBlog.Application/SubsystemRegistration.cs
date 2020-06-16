using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ServerlessBlog.Application
{
    public static class SubsystemRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, 
            ICommandRegistry commandRegistry) {
            commandRegistry.Discover(typeof(SubsystemRegistration).Assembly);

            return serviceCollection;
        }
    }
}
