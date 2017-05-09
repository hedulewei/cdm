namespace Stress_and_Performance_Testing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.CATEGORIES")]
    public partial class CATEGORIES
    {
        [Key]
        [StringLength(20)]
        public string CATEGORY { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }
    }
}
