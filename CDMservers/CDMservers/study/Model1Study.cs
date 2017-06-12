namespace CDMservers.study
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1Study : DbContext
    {
        public Model1Study()
            : base("name=Model1Study")
        {
        }

        public virtual DbSet<ONLINEILLEGAL> ONLINEILLEGAL { get; set; }
        public virtual DbSet<ONLINERECORD> ONLINERECORD { get; set; }
        public virtual DbSet<ONLINEUSER> ONLINEUSER { get; set; }
        public virtual DbSet<SYNCUSER> SYNCUSER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ONLINEILLEGAL>()
                .Property(e => e.IDENTITY)
                .IsUnicode(false);

            modelBuilder.Entity<ONLINEILLEGAL>()
                .Property(e => e.JSON)
                .IsUnicode(false);

            modelBuilder.Entity<ONLINERECORD>()
                .Property(e => e.IDENTITY)
                .IsUnicode(false);

            modelBuilder.Entity<ONLINERECORD>()
                .Property(e => e.RECORD)
                .IsUnicode(false);

            modelBuilder.Entity<ONLINEUSER>()
                .Property(e => e.IDENTITY)
                .IsUnicode(false);

            modelBuilder.Entity<ONLINEUSER>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ONLINEUSER>()
                .Property(e => e.PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<ONLINEUSER>()
                .Property(e => e.WECHAT)
                .IsUnicode(false);

            modelBuilder.Entity<ONLINEUSER>()
                .Property(e => e.LOG)
                .IsUnicode(false);

            modelBuilder.Entity<SYNCUSER>()
                .Property(e => e.IDENTITY)
                .IsUnicode(false);

            modelBuilder.Entity<SYNCUSER>()
                .Property(e => e.NOTICESTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<SYNCUSER>()
                .Property(e => e.STUDYSTATUS)
                .IsUnicode(false);
        }
    }
}
