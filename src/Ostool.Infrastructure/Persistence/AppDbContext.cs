using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Domain.Entities;
using Ostool.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Vendor> Vendors => Set<Vendor>();
        public DbSet<Visitor> Visitors => Set<Visitor>();

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<CarSpecs> CarSpecs => Set<CarSpecs>();
        public DbSet<Advertisement> Advertisements => Set<Advertisement>();
        public DbSet<Favourites> Favourites => Set<Favourites>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}