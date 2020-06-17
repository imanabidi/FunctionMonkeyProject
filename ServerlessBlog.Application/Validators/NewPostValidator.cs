using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Application.Validators
{
    internal class NewPostValidator : AbstractValidator<NewPost>
    {
        public NewPostValidator()
        {
            RuleFor(x => x.Body).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        }
    }
}
