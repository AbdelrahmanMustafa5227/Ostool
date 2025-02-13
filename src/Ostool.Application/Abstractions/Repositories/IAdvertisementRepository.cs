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
        Task<QueryResult<AdvertisementResponse>> GetAll(int pageNumber);
        Task<QueryResult<AdvertisementResponse>> GetAllByVendor(string vendorName, int pageNumber);
        Task<QueryResult<AdvertisementResponse>> GetAllByCarModel(string carModel, int pageNumber);
    }
}