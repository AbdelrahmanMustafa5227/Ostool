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
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.UseTpcMappingStrategy();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Email).IsRequired();

            // Not Required For Google Auth
            builder.Property(x => x.Password).IsRequired(false);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.AuthProvider)
                .IsRequired()
                .HasConversion(x => (int)x, x => (AuthProvider)x);
        }
    }
}