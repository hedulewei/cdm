namespace CDMservers.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewDblog : DbContext
    {
        public NewDblog()
            : base("name=NewDblog")
        {
        }

        public virtual DbSet<VITALLOG> VITALLOG { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
