using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentValidation;
using ServerlessBlog.Commands;

namespace ServerlessBlog.Application.Validators
{
    public class AddPostCommandValidator : AbstractValidator<AddPostCommand>
    {
        public AddPostCommandValidator()
        {
            RuleFor(x => x.Post).NotNull().SetValidator(new NewPostValidator());
        }
    }
}
