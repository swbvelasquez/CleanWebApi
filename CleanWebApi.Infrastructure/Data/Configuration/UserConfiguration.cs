using CleanWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanWebApi.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("UserId");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode(false);

            builder.Property(e => e.DateOfBirth)
                .HasColumnType("date");

            builder.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);

        }
    }
}
