using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.CarSpecs.AddCarDetails
{
    internal class AddCarDetailsCommandValidator : AbstractValidator<AddCarDetailsCommand>
    {
        public AddCarDetailsCommandValidator()
        {
            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.BodyStyle).NotEmpty().IsInEnum();
            RuleFor(x => x.GroundClearance).NotEmpty().InclusiveBetween(1, 30);

            RuleFor(x => x.EngineType).NotEmpty().IsInEnum();
            RuleFor(x => x.HorsePower).NotEmpty().InclusiveBetween(50, 500);
            RuleFor(x => x.Displacement).NotEmpty().InclusiveBetween(800, 2500);
            RuleFor(x => x.numOfCylinders).NotEmpty().InclusiveBetween(2, 16);

            RuleFor(x => x.TransmissionType).NotEmpty().IsInEnum();
            RuleFor(x => x.numOfGears).NotEmpty().InclusiveBetween(4, 20);

            RuleFor(x => x.ZeroToSixty).NotEmpty().ExclusiveBetween(0, 20);
            RuleFor(x => x.TopSpeed).NotEmpty().InclusiveBetween(50, 500);

            RuleFor(x => x.SeatingCapacity).NotEmpty().InclusiveBetween(2, 15);
            RuleFor(x => x.HasSumRoof).NotEmpty();

        }
    }
}