using CleanWebApi.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanWebApi.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 500); 

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
