using Ostool.Application.Features.Vendors.AddVendor;
using Ostool.Application.Features.Vendors.UpdateVendor;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Mappings
{
    public static class VendorMappings
    {
        public static Vendor ToModel(this AddVendorCommand command)
        {
            return new Vendor
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Email = command.Email,
                ContactNumber = command.ContactNumber
            };
        }

        public static void ApplyChanges(this Vendor vendor, UpdateVendorCommand command)
        {
            vendor.Id = command.Id;
            vendor.Name = command.Name;
            vendor.Email = command.Email;
            vendor.ContactNumber = command.ContactNumber;
        }
    }
}