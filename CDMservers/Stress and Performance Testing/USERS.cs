namespace Stress_and_Performance_Testing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.USERS")]
    public partial class USERS
    {
        public decimal ID { get; set; }

        [Key]
        [StringLength(50)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(50)]
        public string PASSWORD { get; set; }

        [Required]
        [StringLength(1024)]
        public string LIMIT { get; set; }

        [Required]
        [StringLength(50)]
        public string DEPARTMENT { get; set; }

        [Required]
        [StringLength(20)]
        public string POST { get; set; }

        [Required]
        [StringLength(20)]
        public string POLICENUM { get; set; }

        [Required]
        [StringLength(20)]
        public string REALNAME { get; set; }

        public bool DISABLED { get; set; }

        public decimal? PDA_TYPE { get; set; }

        [StringLength(345)]
        public string FIRSTFINGER { get; set; }

        [StringLength(345)]
        public string SECONDFINGER { get; set; }

        [Required]
        [StringLength(20)]
        public string COUNTYCODE { get; set; }

        [Required]
        [StringLength(2)]
        public string AUTHORITYLEVEL { get; set; }
    }
}
