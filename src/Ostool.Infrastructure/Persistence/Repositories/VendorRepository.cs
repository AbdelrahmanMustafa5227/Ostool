using Ostool.Application.Abstractions.Repositories;
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

        public void Update(Vendor vendor)
        {
            _dbContext.Vendors.Update(vendor);
        }
    }
}
