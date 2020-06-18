using System;

namespace ServerlessBlog.Commands.Models
{
    public class Post : PostAbstract
    {
        public Guid Id { get; set; }
        public DateTime PostedAtUtc { get; set; }
        public Guid CreatedByUserId { get; set; }
        
    }
}