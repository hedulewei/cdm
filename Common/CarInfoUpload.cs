using System;

namespace Common
{
    public class CarInfoUploadRequest:BasisRequest
    {
    
        public string CAR_NUM { get; set; }
        public string BRAND { get; set; }
        public string MODEL_TYPE { get; set; }
        public string VIN { get; set; }
        public string PLATE_TYPE { get; set; }
        public string OWNER { get; set; }
        public string OWNER_ID { get; set; }

        public decimal? CAR_LENGTH { get; set; }

        public decimal? CAR_WIDTH { get; set; }

        public decimal? CAR_HEIGHT { get; set; }

        public decimal? STANDARD_LENGTH { get; set; }

        public decimal? STANDARD_WIDTH { get; set; }

        public decimal? STANDARD_HEIGHT { get; set; }

       
        public string QUEUE_NUM { get; set; }

     

      
        public string SERIAL_NUM { get; set; }

        public decimal? FINISH { get; set; }

        public decimal? TASK_TYPE { get; set; }

       
        public string INSPECTOR { get; set; }

       
        public string RECHECKER { get; set; }

       
        public string UNLOAD_TASK_NUM { get; set; }

        public decimal? INVALID_TASK { get; set; }
    }
}