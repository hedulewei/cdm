namespace StudyServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.STUDYHISTORYRECORD")]
    public partial class STUDYHISTORYRECORD
    {
        [Required]
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

        [Key]
        public decimal ORDINAL { get; set; }
    }
}
