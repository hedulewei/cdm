namespace Common
{
    /// <summary>
    /// 业务量查询
    /// </summary>
    public class BusinessVolumeQuery : CommonRequest
    {
      
        public string StartTime { get; set; }//开始时间//out
        public string EndTime { get; set; }//业务办理完结时间out
    }
}