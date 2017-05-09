namespace Stress_and_Performance_Testing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.BUSINESSORDINAL")]
    public partial class BUSINESSORDINAL
    {
        [Required]
        [StringLength(10)]
        public string BUSINESSDATE { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string CATEGORY { get; set; }

        public decimal ORDINAL { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string COUNTYCODE { get; set; }
    }
}
