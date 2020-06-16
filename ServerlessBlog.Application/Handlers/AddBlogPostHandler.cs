using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Application.Models;
using ServerlessBlog.Application.Models.Documents;
using ServerlessBlog.Application.Repositories;
using ServerlessBlog.Commands;

namespace ServerlessBlog.Application.Handlers
{
    /// <summary>
    /// DDD domain and actual logic the job, Bounded Context
    /// wont be aware of serviceBus or triggers ,
    /// uses Command Mediator pattern gives us clean Separation
    /// </summary>
    public class AddPostHandler : ICommandHandler<AddPostCommand>
    {
        private readonly IPostRepository _repository;

        public AddPostHandler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(AddPostCommand command)
        {
            PostDocument postDocument = CreatePostDocument(command);

            await AddPostDocumentToRepository(postDocument);
        }

        private async Task AddPostDocumentToRepository(PostDocument postDocument)
        {
            await _repository.Add(postDocument);
        }

        public PostDocument CreatePostDocument(AddPostCommand command)
        {
            var postDocument = new PostDocument()
            {
                Id = Guid.NewGuid(),
                CreationDateTime = DateTime.UtcNow,
                Body = command.Post.Description,
                Title = command.Post.Title,
                Version = Constants.Posts.CurrentDocumentVersion,
                CreatedByUserId = command.UserId
            };

            return postDocument;
        }
    }
}
