using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace study
{
    public partial class studyContext : DbContext
    {
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"Server=localhost;User Id=root;Password=root;Database=study");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.Ordinal)
                    .HasName("ordinal_UNIQUE");

                entity.ToTable("history");

                entity.Property(e => e.Ordinal)
                    .HasColumnName("ordinal")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Drugrelated)
                    .HasColumnName("drugrelated")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Fullmark)
                    .HasColumnName("fullmark")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Identity)
                    .IsRequired()
                    .HasColumnName("identity")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Inspect)
                    .HasColumnName("inspect")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Noticedate)
                    .HasColumnName("noticedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.Stoplicense)
                    .HasColumnName("stoplicense")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Studycompletedate)
                    .HasColumnName("studycompletedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Studylog)
                    .HasColumnName("studylog")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.Studystartdate)
                    .HasColumnName("studystartdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Syncdate)
                    .HasColumnName("syncdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Wechat)
                    .HasColumnName("wechat")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Identity)
                    .HasName("identity_UNIQUE");

                entity.ToTable("user");

                entity.Property(e => e.Identity)
                    .HasColumnName("identity")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Drugrelated)
                    .HasColumnName("drugrelated")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Fullmark)
                    .HasColumnName("fullmark")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Inspect)
                    .HasColumnName("inspect")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Licensetype)
                    .HasColumnName("licensetype")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Noticedate)
                    .HasColumnName("noticedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.Stoplicense)
                    .HasColumnName("stoplicense")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Studycompletedate)
                    .HasColumnName("studycompletedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Studylog)
                    .HasColumnName("studylog")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.Studystartdate)
                    .HasColumnName("studystartdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Syncdate)
                    .HasColumnName("syncdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Wechat)
                    .HasColumnName("wechat")
                    .HasColumnType("varchar(45)");
            });
        }
    }
}