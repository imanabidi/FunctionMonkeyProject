using System;

namespace ServerlessBlog.Commands.Models
{
    public abstract class PostAbstract
    {
        public String Title { get; set; }
        public String Description { get; set; }
    }
}