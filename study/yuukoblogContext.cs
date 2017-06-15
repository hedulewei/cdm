using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace study
{
    public partial class yuukoblogContext : DbContext
    {
        public virtual DbSet<Blobs> Blobs { get; set; }
        public virtual DbSet<Blogrolls> Blogrolls { get; set; }
        public virtual DbSet<Catalogs> Catalogs { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Posttags> Posttags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"Server=192.168.8.252;User Id=ycl;Password=yclemail;Database=yuukoblog");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blobs>(entity =>
            {
                entity.ToTable("blobs");

                entity.HasIndex(e => e.FileName)
                    .HasName("IX_Blobs_FileName");

                entity.HasIndex(e => e.Time)
                    .HasName("IX_Blobs_Time");

                entity.Property(e => e.Id).HasColumnType("char(38)");

                entity.Property(e => e.ContentLength).HasColumnType("bigint(20)");

                entity.Property(e => e.ContentType).HasColumnType("varchar(128)");

                entity.Property(e => e.FileName).HasColumnType("varchar(128)");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<Blogrolls>(entity =>
            {
                entity.ToTable("blogrolls");

                entity.HasIndex(e => e.AvatarId)
                    .HasName("IX_BlogRolls_AvatarId");

                entity.HasIndex(e => e.GitHubId)
                    .HasName("IX_BlogRolls_GitHubId");

                entity.Property(e => e.Id).HasColumnType("char(38)");

                entity.Property(e => e.AvatarId).HasColumnType("char(38)");

                entity.Property(e => e.GitHubId).HasColumnType("varchar(64)");

                entity.Property(e => e.NickName).HasColumnType("varchar(64)");

                entity.Property(e => e.Type).HasColumnType("int(11)");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("varchar(128)");

                entity.HasOne(d => d.Avatar)
                    .WithMany(p => p.Blogrolls)
                    .HasForeignKey(d => d.AvatarId)
                    .HasConstraintName("FK_BlogRolls_Blobs_AvatarId");
            });

            modelBuilder.Entity<Catalogs>(entity =>
            {
                entity.ToTable("catalogs");

                entity.HasIndex(e => e.Pri)
                    .HasName("IX_Catalogs_PRI");

                entity.Property(e => e.Id).HasColumnType("char(38)");

                entity.Property(e => e.Pri)
                    .HasColumnName("PRI")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Url).HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.ToTable("posts");

                entity.HasIndex(e => e.CatalogId)
                    .HasName("IX_Posts_CatalogId");

                entity.HasIndex(e => e.IsPage)
                    .HasName("IX_Posts_IsPage");

                entity.HasIndex(e => e.Time)
                    .HasName("IX_Posts_Time");

                entity.HasIndex(e => e.Url)
                    .HasName("IX_Posts_Url")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("char(38)");

                entity.Property(e => e.CatalogId).HasColumnType("char(38)");

                entity.Property(e => e.IsPage).HasColumnType("bit(1)");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.HasOne(d => d.Catalog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CatalogId)
                    .HasConstraintName("FK_Posts_Catalogs_CatalogId");
            });

            modelBuilder.Entity<Posttags>(entity =>
            {
                entity.ToTable("posttags");

                entity.HasIndex(e => e.PostId)
                    .HasName("IX_PostTags_PostId");

                entity.HasIndex(e => e.Tag)
                    .HasName("IX_PostTags_Tag");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.PostId)
                    .IsRequired()
                    .HasColumnType("char(38)");

                entity.Property(e => e.Tag).HasColumnType("varchar(64)");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Posttags)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_PostTags_Posts_PostId");
            });
        }
    }
}