namespace CDMservers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.VITALLOG")]
    public partial class VITALLOG
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string USERNAME { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime TIME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string KEYWORD { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string IP { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(2048)]
        public string OPERATION { get; set; }
    }
}
