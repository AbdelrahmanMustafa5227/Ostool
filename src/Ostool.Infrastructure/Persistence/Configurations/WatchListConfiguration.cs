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
    internal class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
    {
        public void Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder.HasKey(x => new { x.VisitorId, x.CarId });

            builder.HasOne(x => x.Visitor)
                .WithMany(x => x.WatchList)
                .HasForeignKey(x => x.VisitorId);

            builder.HasOne(x => x.Car)
                .WithMany(x => x.InterestedUsers)
                .HasForeignKey(x => x.CarId);
        }
    }
}