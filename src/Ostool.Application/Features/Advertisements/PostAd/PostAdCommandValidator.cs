using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Advertisements.PostAd
{
    internal class PostAdCommandValidator : AbstractValidator<PostAdCommand>
    {
        public PostAdCommandValidator()
        {
            RuleFor(x => x.VendorId).NotEmpty();
            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Year).NotEmpty().GreaterThan(1990).LessThan(DateTime.Now.Year);
            RuleFor(x => x.ExpirationDate).GreaterThanOrEqualTo(DateTime.Now.AddDays(1));
        }
    }
}