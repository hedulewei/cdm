namespace Webodp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.COUNTY")]
    public partial class COUNTY
    {
        [Key]
        [StringLength(20)]
        public string COUNTYCODE { get; set; }

        [Required]
        [StringLength(20)]
        public string NAME { get; set; }
    }
}
