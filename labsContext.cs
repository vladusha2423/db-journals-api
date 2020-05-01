using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lab5
{
    public partial class labsContext : DbContext
    {
        public labsContext()
        {
        }

        public labsContext(DbContextOptions<labsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Journal> Journal { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Release> Release { get; set; }
        public virtual DbSet<Rubricator> Rubricator { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=labs;Username=postgres;Password=2423");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journal>(entity =>
            {
                entity.HasKey(e => e.Index)
                    .HasName("journal_pkey");

                entity.ToTable("journal");

                entity.Property(e => e.Index)
                    .HasColumnName("index")
                    .HasColumnType("numeric");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasColumnName("publisher")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => new { e.Author, e.Title, e.Id })
                    .HasName("postpk");

                entity.ToTable("post");

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(60);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(6);

                entity.Property(e => e.Notice)
                    .HasColumnName("notice")
                    .HasMaxLength(30);

                entity.Property(e => e.Pages)
                    .HasColumnName("pages")
                    .HasMaxLength(7);

                entity.HasOne(d => d.CodeNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.Code)
                    .HasConstraintName("post_code_fkey");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_id_fkey");
            });

            modelBuilder.Entity<Release>(entity =>
            {
                entity.ToTable("release");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric");

                entity.Property(e => e.Index)
                    .HasColumnName("index")
                    .HasColumnType("numeric");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.IndexNavigation)
                    .WithMany(p => p.Release)
                    .HasForeignKey(d => d.Index)
                    .HasConstraintName("release_index_fkey");
            });

            modelBuilder.Entity<Rubricator>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("rubricator_pkey");

                entity.ToTable("rubricator");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(6);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
