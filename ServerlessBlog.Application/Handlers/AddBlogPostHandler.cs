using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Commands;

namespace ServerlessBlog.Application.Handlers
{
    /// <summary>
    /// DDD domain and actual logic the job, Bounded Context
    /// wont be aware of serviceBus or triggers ,
    /// uses Command Mediator pattern gives us clean Separation
    /// </summary>
    public class AddBlogPostHandler : ICommandHandler<AddPostCommand>
    {
        public Task ExecuteAsync(AddPostCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
