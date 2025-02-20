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
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Visitors.FirstOrDefaultAsync(x => x.Email == email) as AppUser ??
                   await _appDbContext.Vendors.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> IsEmailUsed(string email)
        {
            return await _appDbContext.Visitors.AnyAsync(x => x.Email == email) ||
                   await _appDbContext.Vendors.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsVendor(string email)
        {
            return await _appDbContext.Vendors.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsVisitor(string email)
        {
            return await _appDbContext.Visitors.AnyAsync(x => x.Email == email);
        }

        public void Register(Visitor user)
        {
            _appDbContext.Visitors.Add(user);
        }

        public void Register(Vendor user)
        {
            _appDbContext.Vendors.Add(user);
        }
    }
}