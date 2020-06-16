using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Commands
{
    public class AddPostCommand : ICommand
    {
        /// <summary>
        /// Later we will use Identity server to fill this up
        /// </summary>
        public Guid UserId { get; set; }

        public NewPost Post { get; set; }
    }
}
