using Ostool.Application.Features.Advertisements.Responses;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Repositories
{
    public interface IAdvertisementRepository
    {
        void Add(Advertisement advertisement);
        void Update(Advertisement advertisement);
        Task<AdvertisementDetailedResponse?> GetById(Guid id);
        Task<List<AdvertisementResponse>> GetAll();
        Task<List<AdvertisementResponse>> GetAllByVendor(string vendorName);
        Task<List<AdvertisementResponse>> GetAllByCarModel(string carModel);
    }
}