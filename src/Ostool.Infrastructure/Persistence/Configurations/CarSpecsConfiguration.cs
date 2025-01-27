using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ostool.Domain.Entities;
using Ostool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Configurations
{
    internal class CarSpecsConfiguration : IEntityTypeConfiguration<CarSpecs>
    {
        public void Configure(EntityTypeBuilder<CarSpecs> builder)
        {
            builder.HasKey(c => c.CarId);
            builder.Property(x => x.CarId).ValueGeneratedNever();

            builder
                .Property(x => x.EngineType)
                .HasConversion(x => (int)x, x => (EngineType)x);

            builder
                .Property(x => x.TransmissionType)
                .HasConversion(x => (int)x, x => (TransmissionType)x);

        }
    }
}
