namespace CDMservers.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Business : DbContext
    {
        public Business()
            : base("name=business")
        {
        }

        public virtual DbSet<BUSSINESS> Bussiness { get; set; }
        public virtual DbSet<fushanbusiness> Fushanbusiness { get; set; }
        public virtual DbSet<haiyangbusiness> Haiyangbusiness { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.START_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.END_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.START_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.END_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.START_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.END_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);
        }
    }
}
