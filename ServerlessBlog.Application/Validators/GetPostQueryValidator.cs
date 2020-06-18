using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentValidation;
using ServerlessBlog.Commands;

namespace ServerlessBlog.Application.Validators
{
    public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {
            RuleFor(x => x.PostId).NotEmpty();
        }
    }
}
