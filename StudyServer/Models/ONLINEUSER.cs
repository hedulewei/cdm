namespace StudyServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.ONLINEUSER")]
    public partial class ONLINEUSER
    {
        [Key]
        [StringLength(20)]
        public string IDENTITY { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME { get; set; }

        [Required]
        [StringLength(20)]
        public string PHONE { get; set; }

        [Required]
        [StringLength(50)]
        public string WECHAT { get; set; }

        [StringLength(2000)]
        public string LOG { get; set; }
    }
}
