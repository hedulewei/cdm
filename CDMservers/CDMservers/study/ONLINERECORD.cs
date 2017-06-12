namespace CDMservers.study
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.ONLINERECORD")]
    public partial class ONLINERECORD
    {
        [Key]
        [StringLength(20)]
        public string IDENTITY { get; set; }

        [Required]
        [StringLength(2000)]
        public string RECORD { get; set; }
    }
}
