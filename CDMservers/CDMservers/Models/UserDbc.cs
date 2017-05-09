namespace CDMservers.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UserDbc : DbContext
    {
        public UserDbc()
            : base("name=UserDbc2")
        {
        }

        public virtual DbSet<USERS> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
        }
    }
}
