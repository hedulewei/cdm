namespace StudyServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.CORPORATEINFO")]
    public partial class CORPORATEINFO
    {
        [Required]
        [StringLength(50)]
        public string CODE { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME { get; set; }

        [StringLength(100)]
        public string ADDRESS { get; set; }

        [StringLength(50)]
        public string PHONENUMBER { get; set; }

        public decimal ID { get; set; }
    }
}
