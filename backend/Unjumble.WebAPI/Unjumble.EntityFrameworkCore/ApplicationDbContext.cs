using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Unjumble.Core.Models;
using Unjumble.Core.Models.Domain;
using Unjumble.EntityFrameworkCore.EntityConfigurations;

namespace Unjumble.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<ActivationCode> ActivationCodes { get; set; }
        public DbSet<Documentation> Documentation { get; set; }
        public DbSet<DocumentationPiece> documentationPieces { get; set; }
        public DbSet<IssueBase> IssueBases { get; set; }
        public DbSet<IssueBaseComment> IssueBaseComments { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserCompanyConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
