using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace study
{
    public class GetLearnerInfoResponse : CommonResponse
    {
        public GetLearnerInfoResponse()
        {
            Name = string.Empty;
        }
        public string Name { get; set; }
        public string Identity { get; set; }
        public DrivingLicenseType DrivingLicenseType { get; set; }
        public string Photo { get; set; }
    }
}
