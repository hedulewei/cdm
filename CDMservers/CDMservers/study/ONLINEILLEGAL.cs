namespace CDMservers.study
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.ONLINEILLEGAL")]
    public partial class ONLINEILLEGAL
    {
        [Key]
        [StringLength(20)]
        public string IDENTITY { get; set; }

        [Required]
        [StringLength(200)]
        public string JSON { get; set; }
    }
}
