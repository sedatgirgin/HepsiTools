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
        
            modelBuilder.Entity<User>().HasMany(I => I.UserLisans).WithOne(I => I.User).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lisans>().HasMany(I => I.UserLisans).WithOne(I => I.Lisans).HasForeignKey(I => I.LisansId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLisans>().HasIndex(I => new
            {
                I.UserId,
                I.LisansId,
                I.IsActive
            }).IsUnique();


            modelBuilder.Entity<User>().HasMany(I => I.WooCommerceDatas).WithOne(I => I.User).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>().HasMany(I => I.ConnectionInfo).WithOne(I => I.User).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>().HasMany(I => I.ConnectionInfo).WithOne(I => I.User).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WooCommerceData>().HasMany(I => I.Products).WithOne(I => I.WooCommerceData).HasForeignKey(I => I.WooCommerceDataId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WooCommerceData>().HasMany(I => I.Orders).WithOne(I => I.WooCommerceData).HasForeignKey(I => I.WooCommerceDataId).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<CompetitionCompany>();

            modelBuilder.Entity<ConnectionInfo>().HasMany(I => I.CompetitionAnalyses).WithOne(I => I.ConnectionInfo).HasForeignKey(I => I.ConnectionInfoId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<CompetitionCompany> Companie { get; set; }
        public virtual DbSet<Lisans> Lisans { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<UserLisans> UserLisans { get; set; }
        public virtual DbSet<CompetitionAnalyses> CompetitionAnalyses { get; set; }
        public virtual DbSet<ConnectionInfo> ConnectionInfo { get; set; }
        public virtual DbSet<WooCommerceData> WooCommerceData { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }


    }
}
