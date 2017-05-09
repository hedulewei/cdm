namespace CDMservers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.CONFIG")]
    public partial class CONFIG
    {
        [Key]
        [StringLength(20)]
        public string COUNTYCODE { get; set; }

        [Required]
        [StringLength(20)]
        public string BUSINESSTABLENAME { get; set; }
    }
}
