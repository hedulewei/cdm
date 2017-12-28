namespace StudyServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.CARINFOR")]
    public partial class CARINFOR
    {
        public decimal ID { get; set; }

        [StringLength(50)]
        public string CAR_NUM { get; set; }

        [StringLength(50)]
        public string BRAND { get; set; }

        [StringLength(50)]
        public string MODEL_TYPE { get; set; }

        [StringLength(50)]
        public string VIN { get; set; }

        [StringLength(30)]
        public string PLATE_TYPE { get; set; }

        [StringLength(20)]
        public string OWNER { get; set; }

        [StringLength(20)]
        public string OWNER_ID { get; set; }

        public decimal? CAR_LENGTH { get; set; }

        public decimal? CAR_WIDTH { get; set; }

        public decimal? CAR_HEIGHT { get; set; }

        public decimal? STANDARD_LENGTH { get; set; }

        public decimal? STANDARD_WIDTH { get; set; }

        public decimal? STANDARD_HEIGHT { get; set; }

        [StringLength(10)]
        public string QUEUE_NUM { get; set; }

        public DateTime? TIME { get; set; }

        [StringLength(20)]
        public string SERIAL_NUM { get; set; }

        public decimal? FINISH { get; set; }

        public decimal? TASK_TYPE { get; set; }

        [StringLength(50)]
        public string INSPECTOR { get; set; }

        [StringLength(50)]
        public string RECHECKER { get; set; }

        [StringLength(20)]
        public string UNLOAD_TASK_NUM { get; set; }

        public decimal? INVALID_TASK { get; set; }
    }
}
