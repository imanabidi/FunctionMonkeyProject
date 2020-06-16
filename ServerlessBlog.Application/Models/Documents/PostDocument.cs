using System;

namespace ServerlessBlog.Application.Models.Documents
{
    public class PostDocument
    {
        public Guid Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Guid CreatedByUserId { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }

        public SemanticVersion Version { get; set; }
    }
}