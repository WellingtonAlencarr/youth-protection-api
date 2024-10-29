using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;
using YouthProtectionApi.Models.Enums;

namespace YouthProtectionApi.DataBase
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { }

        public DbSet<UserModel> TB_USER { get; set; }
        public DbSet<PublicationsModel> TB_PUBLICATION { get; set; }
        public DbSet<CommentsModel> TB_COMMENT { get; set; }
           
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().ToTable("TB_USER");
            modelBuilder.Entity<PublicationsModel>().ToTable("TB_PUBLICATION");
            modelBuilder.Entity<CommentsModel>().ToTable("TB_COMMENT");

            modelBuilder.Entity<UserModel>()
                .HasKey(e => e.UserId);

            modelBuilder.Entity<UserModel>()
                .HasMany(e => e.Publications)
                .WithOne(e => e.UserModel)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

            modelBuilder.Entity<UserModel>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.UserModel)
                .HasForeignKey(e => e.CommentId)
                .IsRequired(false);

            modelBuilder.Entity<PublicationsModel>()
                .HasKey(e => e.PublicationId);

            modelBuilder.Entity<PublicationsModel>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<PublicationsModel>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.PublicationsModel)
                .HasForeignKey(e => e.CommentId);

            modelBuilder.Entity<PublicationsModel>()
                .Property(e => e.CreatedAt)
                .HasConversion(
                    e => e.ToUniversalTime(),
                    e => DateTime.SpecifyKind(e, DateTimeKind.Utc));

            modelBuilder.Entity<PublicationsModel>()
                .Property(e => e.ModificationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<PublicationsModel>()
                .Property(e => e.ModificationDate)
                .HasConversion(
                    e => e.ToUniversalTime(),
                    e => DateTime.SpecifyKind(e, DateTimeKind.Utc));

            modelBuilder.Entity<CommentsModel>()
                .HasKey(e => e.CommentId);

            modelBuilder.Entity<CommentsModel>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasConversion(
                e => e.ToUniversalTime(),
                e => DateTime.SpecifyKind(e, DateTimeKind.Utc));
               

            modelBuilder.Entity<UserModel>().Property(u => u.Role).HasConversion<string>();
            modelBuilder.Entity<UserModel>().Property(u => u.UserStatus).HasConversion<string>();
            modelBuilder.Entity<PublicationsModel>().Property(u => u.PublicationsRole).HasConversion<string>();
            modelBuilder.Entity<PublicationsModel>().Property(u => u.PublicationStatus).HasConversion<string>();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("Varchar").HaveMaxLength(200);
        }

       
    }
}
