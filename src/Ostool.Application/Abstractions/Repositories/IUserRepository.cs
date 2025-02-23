using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);
        Task<bool> IsEmailUsed(string email);
        Task<bool> IsVendor(string email);
        Task<bool> IsVisitor(string email);
        Task<bool> VisitorExists(Guid visitorId);
        void Register(Visitor user);
        void Register(Vendor user);
    }
}