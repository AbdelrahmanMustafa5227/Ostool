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
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Brand)
                .HasColumnType("varchar(50)");

            builder
                .Property(x => x.Brand)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.Year);

            builder
                .Property(x => x.AvgPrice)
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(x => x.carSpecs)
                .WithOne(x => x.Car);

            builder
                .HasMany(x => x.advertisements)
                .WithOne(x => x.Car)
                .HasForeignKey(x => x.CarId);

            builder
                .HasIndex(x => x.Model)
                .IsUnique(true);

        }
    }
}
