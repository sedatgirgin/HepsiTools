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
            modelBuilder.Entity<User>().HasMany(I => I.Lisans).WithOne(I => I.User).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(I => I.Companys).WithOne(I => I.User).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(I => I.WooCommerceDatas).WithOne(I => I.User).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Company>().HasMany(I => I.CompetitionAnalyses).WithOne(I => I.Company).HasForeignKey(I => I.CompanyId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CompetitionAnalyses>().HasMany(I => I.CompetitionAnalysesHistories).WithOne(I => I.CompetitionAnalyses).HasForeignKey(I => I.CompetitionAnalysesId).OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompetitionAnalyses> CompetitionAnalyses { get; set; }
        public virtual DbSet<CompetitionAnalysesHistory> CompetitionAnalysesHistory { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<Lisans> Lisans { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WooCommerceData> WooCommerceData { get; set; }
    }
}
