using System.Collections.Generic;

namespace Common
{
    public class BusinessListResult
    {

        public BusinessListResult()
        {
            BussinessList = new List<BusinessModel>();
        }
        public string StatusCode { get; set; }
        public string Result { get; set; }
        public List<BusinessModel> BussinessList { get; set; }
    }
}