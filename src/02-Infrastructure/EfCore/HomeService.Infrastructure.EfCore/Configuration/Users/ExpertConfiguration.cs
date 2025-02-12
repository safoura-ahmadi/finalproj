﻿
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HomeService.Domain.Core.Entities.Users;

namespace HomeService.Infrastructure.EfCore.Configuration.Users;

public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User)
            .WithOne(u => u.Expert);

        builder.Property(e => e.Biography)
               .HasMaxLength(500);

      

      
    }
}