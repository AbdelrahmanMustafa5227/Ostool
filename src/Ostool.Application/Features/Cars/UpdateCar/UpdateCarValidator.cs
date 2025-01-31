using FluentValidation;
using Ostool.Application.Features.Cars.UpdateCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.AddCar
{
    internal class UpdateCarValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarValidator()
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