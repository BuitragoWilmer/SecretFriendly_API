using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class SecretSanta_DBContext : DbContext
    {
        public SecretSanta_DBContext()
        {
        }

        public SecretSanta_DBContext(DbContextOptions<SecretSanta_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Gift> Gifts { get; set; } = null!;
        public virtual DbSet<GroupsFamily> GroupsFamilies { get; set; } = null!;
        public virtual DbSet<Round> Rounds { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRound> UserRounds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=192.168.100.200;port=3306;database=SecretSanta_DB;user=root;password=1000", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasIndex(e => e.RoundId, "fk_round");

                entity.HasIndex(e => e.SecretSantaId, "secret_santa_id");

                entity.HasIndex(e => e.UserId, "user_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.RoundId)
                    .HasColumnType("int(11)")
                    .HasColumnName("round_id");

                entity.Property(e => e.SecretSantaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("secret_santa_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Round)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.RoundId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_round");

                entity.HasOne(d => d.SecretSanta)
                    .WithMany(p => p.AssignmentSecretSanta)
                    .HasForeignKey(d => d.SecretSantaId)
                    .HasConstraintName("Assignments_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AssignmentUser)
                    .HasForeignKey<Assignment>(d => d.UserId)
                    .HasConstraintName("Assignments_ibfk_1");
            });

            modelBuilder.Entity<Gift>(entity =>
            {
                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .HasColumnName("message");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Gifts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Gifts_ibfk_1");
            });

            modelBuilder.Entity<GroupsFamily>(entity =>
            {
                entity.ToTable("GroupsFamily");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.ToTable("Round");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.GroupFamilyId, "fk_groupsFamily");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.GroupFamilyId).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.HasOne(d => d.GroupFamily)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GroupFamilyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_groupsFamily");
            });

            modelBuilder.Entity<UserRound>(entity =>
            {
                entity.ToTable("UserRound");

                entity.HasIndex(e => e.RoundId, "fk_round_to_userround");

                entity.HasIndex(e => new { e.UserId, e.RoundId }, "user_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.RoundId)
                    .HasColumnType("int(11)")
                    .HasColumnName("round_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Round)
                    .WithMany(p => p.UserRounds)
                    .HasForeignKey(d => d.RoundId)
                    .HasConstraintName("fk_round_to_userround");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRounds)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user_to_userround");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
