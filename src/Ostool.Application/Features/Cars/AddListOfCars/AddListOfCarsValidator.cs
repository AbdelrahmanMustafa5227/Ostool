using FluentValidation;
using Ostool.Application.Features.Cars.AddCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Cars.AddListOfCars
{
    internal class AddListOfCarsValidator : AbstractValidator<AddListOfCarsCommand>
    {
        public AddListOfCarsValidator()
        {
            RuleForEach(c => c.Cars).SetValidator(new AddCarValidator());
        }
    }
}