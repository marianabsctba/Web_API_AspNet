using System;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Data
{
    public class WebDbContext : DbContext
    {
        public WebDbContext (DbContextOptions<WebDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .Ignore(x => x.QuantityDonations);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Domain.Model.Models.Donation> Donations { get; set; }

        public DbSet<Domain.Model.Models.User> Users { get; set; }
    }
}
