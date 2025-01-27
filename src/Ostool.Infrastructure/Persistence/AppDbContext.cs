using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence
{
    public class AppDbContext : DbContext , IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<CarSpecs> CarSpecs => Set<CarSpecs>();
        public DbSet<Vendor> Vendors => Set<Vendor>();
        public DbSet<Advertisement> Advertisements => Set<Advertisement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
