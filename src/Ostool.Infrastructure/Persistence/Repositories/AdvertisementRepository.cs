using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly AppDbContext _appDbContext;

        public AdvertisementRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Add(Advertisement advertisement)
        {
            _appDbContext.Advertisements.Add(advertisement);
        }

        public async Task<List<Advertisement>> GetAll()
        {
            return await _appDbContext.Advertisements.ToListAsync();
        }

        public async Task<List<Advertisement>> GetAllByCarModel(string carModel)
        {
            return await _appDbContext.Advertisements.Where(x => x.Car.Model == carModel).ToListAsync();
        }

        public async Task<List<Advertisement>> GetAllByVendor(string vendorName)
        {
            return await _appDbContext.Advertisements.Where(x => x.Vendor.Name == vendorName).ToListAsync();
        }

        public void Update(Advertisement advertisement)
        {
            _appDbContext.Advertisements.Update(advertisement);
        }
    }
}
