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
            optionsBuilder.UseMySql(@"Server=192.168.10.94;User Id=study;Password=yunyi@6688A;Database=study");
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
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Drugrelated)
                    .HasColumnName("drugrelated")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Finishdate)
                    .HasColumnName("finishdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fullmark)
                    .HasColumnName("fullmark")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Identity)
                    .IsRequired()
                    .HasColumnName("identity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Inspect)
                    .HasColumnName("inspect")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Licensetype)
                    .IsRequired()
                    .HasColumnName("licensetype")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Noticedate)
                    .HasColumnName("noticedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Startdate)
                    .HasColumnName("startdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Stoplicense)
                    .HasColumnName("stoplicense")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Studylog)
                    .HasColumnName("studylog")
                    .HasColumnType("varchar(500)");

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
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Drugrelated)
                    .HasColumnName("drugrelated")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Fullmark)
                    .HasColumnName("fullmark")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Inspect)
                    .HasColumnName("inspect")
                    .HasColumnType("varchar(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Licensetype)
                    .IsRequired()
                    .HasColumnName("licensetype")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Noticedate)
                    .HasColumnName("noticedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Startdate)
                    .HasColumnName("startdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Stoplicense)
                    .HasColumnName("stoplicense")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Studylog)
                    .HasColumnName("studylog")
                    .HasColumnType("varchar(500)");

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