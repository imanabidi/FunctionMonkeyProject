using System;
using System.Threading.Tasks;
using AutoMapper;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Application.Models;
using ServerlessBlog.Application.Models.Documents;
using ServerlessBlog.Application.Repositories;
using ServerlessBlog.Commands;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Application.Handlers
{
    /// <summary>
    /// DDD domain and actual logic the job, Bounded Context
    /// wont be aware of serviceBus or triggers ,
    /// uses Command Mediator pattern gives us clean Separation
    /// you will not find any other usages of this and all the thigs are based on discovery
    /// </summary>
    public class GetPostQueryHandler : ICommandHandler<GetPostQuery, Post>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _postRepository = repository;
            _mapper = mapper;
        }

        public async Task<Post> ExecuteAsync(GetPostQuery query, Post previousResult)
        {
            PostDocument postDocument = await _postRepository.Get(query.PostId);
            var post = _mapper.Map<Post>(postDocument);

            return post;
        }






    }
}
