using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Helpers;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly AppDbContext _dbContext;

        public VendorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Vendor vendor)
        {
            _dbContext.Vendors.Add(vendor);
        }

        public void Delete(Vendor vendor)
        {
            _dbContext.Vendors.Remove(vendor);
        }

        public async Task<bool> Exists(string EmailAddress)
        {
            return await _dbContext.Vendors.AnyAsync(x => x.Email == EmailAddress);
        }

        public async Task<QueryResult<Vendor>> GetAll(int pageNumber)
        {
            var totalItems = await _dbContext.Vendors.CountAsync();

            var vendors = await _dbContext
                .Vendors
                .Skip((pageNumber - 1) * 10)
                .Take(10)
                .ToListAsync();

            return new QueryResult<Vendor>(vendors, totalItems);
        }

        public async Task<Vendor?> GetById(Guid Id)
        {
            return await _dbContext.Vendors.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public void Update(Vendor vendor)
        {
            _dbContext.Vendors.Update(vendor);
        }
    }
}