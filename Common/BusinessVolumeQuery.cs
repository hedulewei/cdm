using System.Collections.Generic;

namespace Common
{
    public class CommonRequest
    {
        public string CountyCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class BusinessVolumeQuery : CommonRequest
    {
      
        public string StartTime { get; set; }//开始时间//out
        public string EndTime { get; set; }//业务办理完结时间out
    }
    public class OneUserVolume
    {
        public string UserName { get; set; }
        public int Volume { get; set; }//业务量
    }

    public class BusinessVolumeQueryResult:CommonResult
    {
        public BusinessVolumeQueryResult()
        {
            Volumes=new List<OneUserVolume>();
        }
        public List<OneUserVolume> Volumes { get; set; }//list
    }
}