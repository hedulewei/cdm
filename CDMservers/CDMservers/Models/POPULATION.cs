namespace CDMservers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.POPULATION")]
    public partial class POPULATION
    {
        [StringLength(200)]
        public string NAME { get; set; }

        [StringLength(5)]
        public string SEX { get; set; }

        [StringLength(10)]
        public string NATION { get; set; }

        [StringLength(15)]
        public string BORN { get; set; }

        [StringLength(260)]
        public string ADDRESS { get; set; }

        [StringLength(10)]
        public string POSTCODE { get; set; }

        [StringLength(260)]
        public string POSTADDRESS { get; set; }

        [StringLength(15)]
        public string MOBILE { get; set; }

        [StringLength(20)]
        public string TELEPHONE { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [Key]
        [StringLength(50)]
        public string IDNUM { get; set; }

        [StringLength(345)]
        public string FIRSTFINGER { get; set; }

        [StringLength(345)]
        public string SECONDFINGER { get; set; }

        [StringLength(2100)]
        public string LEFTEYE { get; set; }

        [StringLength(2100)]
        public string RIGHTEYE { get; set; }
    }
}
