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
    internal class FavouritesConfiguration : IEntityTypeConfiguration<Favourites>
    {
        public void Configure(EntityTypeBuilder<Favourites> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasOne(x => x.Visitor)
                .WithMany(x => x.SavedAds)
                .HasForeignKey(x => x.VisitorId);

            builder.HasOne(x => x.Advertisement)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.AdvertisementId);
        }
    }
}
