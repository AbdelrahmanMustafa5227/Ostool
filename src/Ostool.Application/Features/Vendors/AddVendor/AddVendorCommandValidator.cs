using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Features.Vendors.AddVendor
{
    internal class AddVendorCommandValidator : AbstractValidator<AddVendorCommand>
    {
        public AddVendorCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.ContactNumber).Length(11).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}