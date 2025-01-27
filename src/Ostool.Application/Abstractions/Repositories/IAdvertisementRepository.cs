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
        Task<List<Advertisement>> GetAll();
        Task<List<Advertisement>> GetAllByVendor(string vendorName);
        Task<List<Advertisement>> GetAllByCarModel(string carModel);
    }
}