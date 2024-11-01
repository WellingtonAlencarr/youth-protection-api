using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;
using YouthProtectionApi.Models;
using YouthProtectionApi.Models.Enums;

namespace YouthProtectionApi.DataBase
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { }

        public DbSet<UserModel> TB_USER { get; set; }
        public DbSet<PublicationsModel> TB_PUBLICATION { get; set; }
        public DbSet<AttendanceModel> TB_ATTENDANCES { get; set; }
        public DbSet<ChatModel> TB_CHAT { get; set; }
        public DbSet<MessageModel> TB_MESSAGES { get; set; }

           
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().ToTable("TB_USER");
            modelBuilder.Entity<PublicationsModel>().ToTable("TB_PUBLICATION");
            modelBuilder.Entity<AttendanceModel>().ToTable("TB_ATTENDANCES");
            modelBuilder.Entity<ChatModel>().ToTable("TB_CHAT");
            modelBuilder.Entity<MessageModel>().ToTable("TB_MESSAGES");

            modelBuilder.Entity<UserModel>()
                .HasKey(e => e.UserId);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Publications)
                .WithOne(p => p.UserModel)
                .HasForeignKey(p => p.UserId)
                .IsRequired(false);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .IsRequired();

            modelBuilder.Entity<PublicationsModel>()
                .HasKey(e => e.PublicationId);

            modelBuilder.Entity<PublicationsModel>()
                .Property(e => e.CreatedAt)
                .HasConversion(
                    e => e.ToUniversalTime(),
                    e => DateTime.SpecifyKind(e, DateTimeKind.Utc));

            modelBuilder.Entity<PublicationsModel>()
                .Property(e => e.ModificationDate)
                .HasConversion(
                    e => e.ToUniversalTime(),
                    e => DateTime.SpecifyKind(e, DateTimeKind.Utc));

            modelBuilder.Entity<AttendanceModel>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<AttendanceModel>()
                .HasOne(a => a.Volunteer)
                .WithMany()
                .HasForeignKey(a => a.VolunteerId)
                .IsRequired();

            modelBuilder.Entity<AttendanceModel>()
                .HasOne(a => a.Publication)
                .WithMany()
                .HasForeignKey(a => a.PublicationId)
                .IsRequired();

            modelBuilder.Entity<ChatModel>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<ChatModel>()
                .HasOne(c => c.Attendance)
                .WithMany()
                .HasForeignKey(c => c.AttendanceId)
                .IsRequired();

            modelBuilder.Entity<MessageModel>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<MessageModel>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                 .IsRequired();

            modelBuilder.Entity<MessageModel>()
                .Property(e => e.SentAt)
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
