namespace StudyServer.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<BUSINESS_CHANGDAO> BUSINESS_CHANGDAO { get; set; }
        public virtual DbSet<BUSINESS_FUSHAN> BUSINESS_FUSHAN { get; set; }
        public virtual DbSet<BUSINESS_HAIYANG> BUSINESS_HAIYANG { get; set; }
        public virtual DbSet<BUSINESS_LAISHAN> BUSINESS_LAISHAN { get; set; }
        public virtual DbSet<BUSINESS_LAIYANG> BUSINESS_LAIYANG { get; set; }
        public virtual DbSet<BUSINESS_LAIZHOU> BUSINESS_LAIZHOU { get; set; }
        public virtual DbSet<BUSINESS_LONGKOU> BUSINESS_LONGKOU { get; set; }
        public virtual DbSet<BUSINESS_MUPING> BUSINESS_MUPING { get; set; }
        public virtual DbSet<BUSINESS_PENGLAI> BUSINESS_PENGLAI { get; set; }
        public virtual DbSet<BUSINESS_QIXIA> BUSINESS_QIXIA { get; set; }
        public virtual DbSet<BUSINESS_ZHAOYUAN> BUSINESS_ZHAOYUAN { get; set; }
        public virtual DbSet<BUSINESS_ZHIFU> BUSINESS_ZHIFU { get; set; }
        public virtual DbSet<BUSINESSCATEGORY> BUSINESSCATEGORY { get; set; }
        public virtual DbSet<BUSINESSORDINAL> BUSINESSORDINAL { get; set; }
        public virtual DbSet<CARINFOR> CARINFOR { get; set; }
        public virtual DbSet<CATEGORIES> CATEGORIES { get; set; }
        public virtual DbSet<CONFIG> CONFIG { get; set; }
        public virtual DbSet<CORPORATEINFO> CORPORATEINFO { get; set; }
        public virtual DbSet<COUNTY> COUNTY { get; set; }
        public virtual DbSet<ONLINEILLEGAL> ONLINEILLEGAL { get; set; }
        public virtual DbSet<ONLINERECORD> ONLINERECORD { get; set; }
        public virtual DbSet<ONLINEUSER> ONLINEUSER { get; set; }
        public virtual DbSet<POPULATION> POPULATION { get; set; }
        public virtual DbSet<STUDYHISTORYRECORD> STUDYHISTORYRECORD { get; set; }
        public virtual DbSet<SYNCUSER> SYNCUSER { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<ZHIFUBUSINESS> ZHIFUBUSINESS { get; set; }
        public virtual DbSet<VITALLOG> VITALLOG { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_CHANGDAO>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_FUSHAN>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_HAIYANG>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAISHAN>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIYANG>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LAIZHOU>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_LONGKOU>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_MUPING>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_PENGLAI>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_QIXIA>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHAOYUAN>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESS_ZHIFU>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSCATEGORY>()
                .Property(e => e.BUSINESSCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSCATEGORY>()
                .Property(e => e.BUSINESSNAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSCATEGORY>()
                .Property(e => e.CATEGORY)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSCATEGORY>()
                .Property(e => e.SERVICEAPI)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSORDINAL>()
                .Property(e => e.BUSINESSDATE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSORDINAL>()
                .Property(e => e.CATEGORY)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSORDINAL>()
                .Property(e => e.ORDINAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESSORDINAL>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.BRAND)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.MODEL_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.VIN)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.PLATE_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.OWNER)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.OWNER_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.CAR_LENGTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.CAR_WIDTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.CAR_HEIGHT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.STANDARD_LENGTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.STANDARD_WIDTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.STANDARD_HEIGHT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.QUEUE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.FINISH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.TASK_TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.INSPECTOR)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.RECHECKER)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.UNLOAD_TASK_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.INVALID_TASK)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CATEGORIES>()
                .Property(e => e.CATEGORY)
                .IsUnicode(false);

            modelBuilder.Entity<CATEGORIES>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<CONFIG>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<CONFIG>()
                .Property(e => e.BUSINESSTABLENAME)
                .IsUnicode(false);

            modelBuilder.Entity<CORPORATEINFO>()
                .Property(e => e.CODE)
                .IsUnicode(false);

            modelBuilder.Entity<CORPORATEINFO>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<CORPORATEINFO>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<CORPORATEINFO>()
                .Property(e => e.PHONENUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<CORPORATEINFO>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<COUNTY>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<COUNTY>()
                .Property(e => e.NAME)
                .IsUnicode(false);

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

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.SEX)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.NATION)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.BORN)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.POSTCODE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.POSTADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.MOBILE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.TELEPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.IDNUM)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.FIRSTFINGER)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.SECONDFINGER)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.LEFTEYE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.RIGHTEYE)
                .IsUnicode(false);

            modelBuilder.Entity<STUDYHISTORYRECORD>()
                .Property(e => e.IDENTITY)
                .IsUnicode(false);

            modelBuilder.Entity<STUDYHISTORYRECORD>()
                .Property(e => e.NOTICESTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<STUDYHISTORYRECORD>()
                .Property(e => e.STUDYSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<STUDYHISTORYRECORD>()
                .Property(e => e.STOPLICENSE)
                .IsUnicode(false);

            modelBuilder.Entity<STUDYHISTORYRECORD>()
                .Property(e => e.ORDINAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<SYNCUSER>()
                .Property(e => e.IDENTITY)
                .IsUnicode(false);

            modelBuilder.Entity<SYNCUSER>()
                .Property(e => e.NOTICESTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<SYNCUSER>()
                .Property(e => e.STUDYSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<SYNCUSER>()
                .Property(e => e.STOPLICENSE)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USERS>()
                .Property(e => e.USERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.LIMIT)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.DEPARTMENT)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.POST)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.POLICENUM)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.REALNAME)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.PDA_TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USERS>()
                .Property(e => e.FIRSTFINGER)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.SECONDFINGER)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.AUTHORITYLEVEL)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.POSTPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.POSTADDR)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.CHECK_FILE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.TAX_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.TAX_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.ORIGIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<ZHIFUBUSINESS>()
                .Property(e => e.ORIGIN_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<VITALLOG>()
                .Property(e => e.USERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<VITALLOG>()
                .Property(e => e.KEYWORD)
                .IsUnicode(false);

            modelBuilder.Entity<VITALLOG>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<VITALLOG>()
                .Property(e => e.OPERATION)
                .IsUnicode(false);
        }
    }
}
