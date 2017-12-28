namespace StudyServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.BUSINESSCATEGORY")]
    public partial class BUSINESSCATEGORY
    {
        [Key]
        [StringLength(20)]
        public string BUSINESSCODE { get; set; }

        [Required]
        [StringLength(20)]
        public string BUSINESSNAME { get; set; }

        [Required]
        [StringLength(20)]
        public string CATEGORY { get; set; }

        [Required]
        [StringLength(50)]
        public string SERVICEAPI { get; set; }
    }
}
