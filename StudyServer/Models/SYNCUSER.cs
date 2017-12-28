namespace StudyServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.SYNCUSER")]
    public partial class SYNCUSER
    {
        [Key]
        [StringLength(20)]
        public string IDENTITY { get; set; }

        [Required]
        [StringLength(20)]
        public string NOTICESTATUS { get; set; }

        [Required]
        [StringLength(20)]
        public string STUDYSTATUS { get; set; }

        public DateTime? NOTICEDATE { get; set; }

        public DateTime SYNCDATE { get; set; }

        public DateTime? STUDYSTARTDATE { get; set; }

        public DateTime? STUDYCOMPLETEDATE { get; set; }

        [StringLength(1)]
        public string STOPLICENSE { get; set; }
    }
}
