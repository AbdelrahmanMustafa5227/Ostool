using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.AddCar
{
    internal class AddCarValidator : AbstractValidator<AddCarCommand>
    {
        public AddCarValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty();

            RuleFor(x => x.AvgPrice)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Brand)
                .NotEmpty();

        }
    }
}