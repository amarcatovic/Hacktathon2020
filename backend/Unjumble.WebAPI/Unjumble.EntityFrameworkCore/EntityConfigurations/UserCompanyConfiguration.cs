using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Unjumble.Core.Models.Domain;

namespace Unjumble.EntityFrameworkCore.EntityConfigurations
{
    public class UserCompanyConfiguration : IEntityTypeConfiguration<UserCompany>
    {
        public void Configure(EntityTypeBuilder<UserCompany> builder)
        {
            builder.HasKey(u => new { u.ComapnyId, u.UserId });

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserCompanies)
                .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.Company)
                .WithMany(u => u.UserCompanies)
                .HasForeignKey(uc => uc.ComapnyId);
        }
    }
}
