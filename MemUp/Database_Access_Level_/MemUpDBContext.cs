using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database_Access_Level
{
    public partial class MemUpDBContext : DbContext
    {
        public MemUpDBContext()
        {
        }

        public MemUpDBContext(DbContextOptions<MemUpDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Collection> Collections { get; set; } = null!;
        public virtual DbSet<Mem> Mems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=1234;Database=MemUpDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("collections");

                entity.HasIndex(e => e.CollectionName, "collections_collection_name_key")
                    .IsUnique();

                entity.Property(e => e.CollectionId).HasColumnName("collection_id");

                entity.Property(e => e.CollectionName)
                    .HasMaxLength(64)
                    .HasColumnName("collection_name");

                entity.Property(e => e.DailyLimit)
                    .HasColumnName("daily_limit")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collections)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("collection_owner");
            });

            modelBuilder.Entity<Mem>(entity =>
            {
                entity.ToTable("mems");

                entity.Property(e => e.MemId).HasColumnName("mem_id");

                entity.Property(e => e.AdditionalInfo).HasColumnName("additional_info");

                entity.Property(e => e.AnswerText).HasColumnName("answer_text");

                entity.Property(e => e.CollectionId).HasColumnName("collection_id");

                entity.Property(e => e.ImagePath).HasColumnName("image_path");

                entity.Property(e => e.QuestionText).HasColumnName("question_text");

                entity.Property(e => e.ReviewTime).HasColumnName("review_time");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.Mems)
                    .HasForeignKey(d => d.CollectionId)
                    .HasConstraintName("parent_collection");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Mems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_profile");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.UserName, "users_user_name_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.DayStreak).HasColumnName("day_streak");

                entity.Property(e => e.MessagesStatus)
                    .HasColumnName("messages_status")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.MpBalance).HasColumnName("mp_balance");

                entity.Property(e => e.UserName)
                    .HasMaxLength(16)
                    .HasColumnName("user_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
