using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Vendors.UpdateVendor
{
    internal class UpdateVendorCommandValidator : AbstractValidator<UpdateVendorCommand>
    {
        public UpdateVendorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.ContactNumber).Length(11).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}