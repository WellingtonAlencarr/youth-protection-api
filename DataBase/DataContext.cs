using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;

namespace YouthProtectionApi.DataBase
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { }

        public DbSet<UserModel> TB_USER { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().ToTable("TB_USER");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("Varchar").HaveMaxLength(200);
        }
    }
}
