namespace StudyServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.BUSINESS_HAIYANG")]
    public partial class BUSINESS_HAIYANG
    {
        public decimal ID { get; set; }

        public decimal TYPE { get; set; }

        public DateTime START_TIME { get; set; }

        public DateTime END_TIME { get; set; }

        public decimal STATUS { get; set; }

        [Required]
        [StringLength(10)]
        public string QUEUE_NUM { get; set; }

        [StringLength(25)]
        public string ID_NUM { get; set; }

        [StringLength(100)]
        public string ADDRESS { get; set; }

        [StringLength(20)]
        public string SERIAL_NUM { get; set; }

        [StringLength(100)]
        public string REJECT_REASON { get; set; }

        [StringLength(100)]
        public string NAME { get; set; }

        [StringLength(50)]
        public string PHONE_NUM { get; set; }

        [StringLength(50)]
        public string PROCESS_USER { get; set; }

        [StringLength(50)]
        public string FILE_RECV_USER { get; set; }

        public decimal? TRANSFER_STATUS { get; set; }

        [StringLength(50)]
        public string UPLOADER { get; set; }

        [StringLength(50)]
        public string COMPLETE_PAY_USER { get; set; }

        [StringLength(100)]
        public string ATTENTION { get; set; }

        [StringLength(20)]
        public string UNLOAD_TASK_NUM { get; set; }

        [Required]
        [StringLength(20)]
        public string COUNTYCODE { get; set; }

        [StringLength(50)]
        public string POSTPHONE { get; set; }

        [StringLength(200)]
        public string POSTADDR { get; set; }

        public decimal? CHECK_FILE { get; set; }

        [StringLength(20)]
        public string CAR_NUM { get; set; }

        [StringLength(20)]
        public string TAX_TYPE { get; set; }

        [StringLength(20)]
        public string TAX_NUM { get; set; }

        [StringLength(20)]
        public string ORIGIN_TYPE { get; set; }

        [StringLength(20)]
        public string ORIGIN_NUM { get; set; }
    }
}
