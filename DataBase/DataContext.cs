using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;

namespace YouthProtectionApi.DataBase
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { }

        public DbSet<UserModel> TB_USER { get; set; }
        public DbSet<PublicationsModel> TB_PUBLICATION { get; set; }
           
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().ToTable("TB_USER");
            modelBuilder.Entity<PublicationsModel>().ToTable("TB_PUBLICATION");

            modelBuilder.Entity<UserModel>()
                .HasMany(e => e.Publications)
                .WithOne(e => e.UserModel)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

            modelBuilder.Entity<UserModel>().Property(u => u.Role).HasDefaultValue("User");
            modelBuilder.Entity<PublicationsModel>().Property(u => u.PublicationsRole).HasDefaultValue("Privado");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("Varchar").HaveMaxLength(200);
        }

       
    }
}
