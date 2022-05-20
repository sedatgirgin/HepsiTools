using HepsiTools.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
                optionsBuilder.UseNpgsql("User ID=okrpwbsxbljoxg;Password=057dc652a0fc8a972a889eca2f56f9cf33cf967175809767071a41be8d47c891;Server=ec2-44-194-117-205.compute-1.amazonaws.com; Port=5432;Database=d5r4cg6smcu1ba;Integrated Security=true;Pooling=true;");
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
