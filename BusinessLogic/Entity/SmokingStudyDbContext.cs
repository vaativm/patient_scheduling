using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WebAppointments.BusinessLogic.Entity
{
    public partial class SmokingStudyDbContext : DbContext
    {
        public SmokingStudyDbContext()
        {
        }

        public SmokingStudyDbContext(DbContextOptions<SmokingStudyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Facilities> Facilities { get; set; }
        public virtual DbSet<Participants> Participants { get; set; }
        public virtual DbSet<VisitSettings> VisitSettings { get; set; }
        public virtual DbSet<Visits> Visits { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<PSFSchedule> PSFSchedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                       .AddJsonFile("appsettings.json")
                       .Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("SmokingStudyDbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });
            //Remove facility on testing the new model 
            modelBuilder.Entity<Facilities>(entity =>
            {
                entity.ToTable("facilities");

                // entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Mflcode)
                    .HasColumnName("mflcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Participants>(entity =>
            {
                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.AlternateContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateMobilePhone)
                   .HasMaxLength(50)
                   .IsUnicode(false);


                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired();

                entity.Property(e => e.StudyId)
                    .HasColumnName("StudyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VisitSettings>(entity =>
            {
                entity.Property(e => e.VisitType).HasMaxLength(50);
            });

            modelBuilder.Entity<Visits>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateParticipantCame).HasColumnType("datetime");

                entity.Property(e => e.NextAppointment).HasColumnType("datetime");

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visits_Participants");

                entity.HasOne(d => d.VisitSetting)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.VisitSettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visits_VisitSettings");
            });

            modelBuilder.Entity<PSFSchedule>(entity =>
            {
                entity.HasKey(k => new { k.StudyId, k.VisitStage });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
