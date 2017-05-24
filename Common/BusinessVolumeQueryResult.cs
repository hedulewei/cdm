using System.Collections.Generic;

namespace Common
{
    public class BusinessVolumeQueryResult:CommonResult
    {
        public BusinessVolumeQueryResult()
        {
            Volumes=new List<OneUserVolume>();
        }
        public List<OneUserVolume> Volumes { get; set; }//list
    }
}