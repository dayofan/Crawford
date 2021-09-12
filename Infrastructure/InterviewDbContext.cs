using Crawford.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Crawford.Infrastructure
{
    public class InterviewDbContext : DbContext
    {
        public InterviewDbContext()
        {
        }

        public InterviewDbContext(DbContextOptions<InterviewDbContext> options)
            : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileActivityLink> FileActivityLinks { get; set; }
        public DbSet<LegacyActivity> LegacyActivities { get; set; }
        public DbSet<LegacyActivityCategory> LegacyActivityCategories { get; set; }
        public DbSet<LegacyActivityType> LegacyActivityTypes { get; set; }
        public DbSet<LossType> LossTypes { get; set; }
        public DbSet<PartyClaimRole> PartyClaimRoles { get; set; }
        public DbSet<PartyType> PartyTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ActivityDetails)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.ActivityTypeCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ClaimReference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LegacyActivityId).HasMaxLength(50);

                entity.Property(e => e.PartyType)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LossLoc).HasMaxLength(200);

                entity.Property(e => e.Policy).HasMaxLength(100);

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Created)
                    .WithMany()
                    .HasForeignKey(d => d.CreatedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__CreatedI__10416098");

                entity.HasOne(d => d.LastUpdated)
                    .WithMany()
                    .HasForeignKey(d => d.LastUpdatedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__LastUpda__113584D1");

                entity.HasOne(d => d.LossAdjuster)
                    .WithMany()
                    .HasForeignKey(d => d.LossAdjusterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__LossAdju__0F4D3C5F");

                entity.HasOne(d => d.LossType)
                    .WithMany()
                    .HasForeignKey(d => d.LossTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__LossType__0E591826");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Email");

                entity.Property(e => e.BodyText).HasColumnType("ntext");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.PartyTypeCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.RecipientTo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SentBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SentTime).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BlobName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PublicId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<FileActivityLink>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LegacyActivityId).HasMaxLength(50);
            });

            modelBuilder.Entity<LegacyActivity>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AdhocOrPartyInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CategoryInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ClaimReference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Detail)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<LegacyActivityCategory>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LegacyActivityType>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CategoryCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LossType>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LossTypeCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.LossTypeDescription)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PartyClaimRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PartyClaimRole");

                entity.Property(e => e.AdhocOrPartyInd)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ClaimReference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PartyReference).HasMaxLength(20);

                entity.Property(e => e.PartyTypeCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Telephone).HasMaxLength(35);
            });

            modelBuilder.Entity<PartyType>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
