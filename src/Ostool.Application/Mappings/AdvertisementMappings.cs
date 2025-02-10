using Ostool.Application.Features.Advertisements.PostAd;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Mappings
{
    public static class AdvertisementMappings
    {
        public static Advertisement ToModel(this PostAdCommand postAdCommand)
        {
            return new Advertisement
            {
                Id = Guid.NewGuid(),
                CarId = postAdCommand.CarId,
                VendorId = postAdCommand.VendorId,
                Description = postAdCommand.Description,
                ExpirationDate = postAdCommand.ExpirationDate,
                Price = postAdCommand.Price,
                Year = postAdCommand.Year,
                PostedDate = DateTime.Now
            };
        }
    }
}