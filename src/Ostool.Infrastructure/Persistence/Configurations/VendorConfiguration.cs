using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Configurations
{
    internal class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder
                .Property(x => x.Name)
                .HasMaxLength(50);

            builder
                .Property(x => x.ContactNumber)
                .HasMaxLength(20);

            builder
                .HasIndex(x => x.Email)
                .IsUnique(true);

            builder
                .HasMany(x => x.Advertisements)
                .WithOne(x => x.Vendor)
                .HasForeignKey(x => x.VendorId);
        }
    }
}
