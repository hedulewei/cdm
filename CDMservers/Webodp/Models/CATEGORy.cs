namespace Webodp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.CATEGORIES")]
    public partial class CATEGORy
    {
        [Key]
        [Column("CATEGORY")]
        [StringLength(20)]
        public string CATEGORY1 { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }
    }
}
