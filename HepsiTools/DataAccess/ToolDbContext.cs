using HepsiTools.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HepsiTools.DataAccess
{
    public class ToolDbContext : IdentityDbContext
    {
        public ToolDbContext()
        {
        }

        public ToolDbContext(DbContextOptions<ToolDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(I => I.Companies).
              WithOne(I => I.User).HasForeignKey(I => I.UserId);

            modelBuilder.Entity<User>().HasMany(I => I.UserLisans).
            WithOne(I => I.User).HasForeignKey(I => I.UserId);

            modelBuilder.Entity<Company>().HasMany(I => I.Orders).
          WithOne(I => I.Company).HasForeignKey(I => I.CompanyId);

            modelBuilder.Entity<Lisans>().HasMany(I => I.UserLisans).
          WithOne(I => I.Lisans).HasForeignKey(I => I.LisansId);

            modelBuilder.Entity<UserLisans>().HasIndex(I => new
            {
                I.UserId,
                I.LisansId,
                I.IsActive
            }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Lisans> Lisan { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<UserLisans> UserLisans { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }


    }
}
