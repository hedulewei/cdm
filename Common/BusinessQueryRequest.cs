namespace Common
{
    public class BusinessQueryRequest: BasisRequest
    {
        public string startTime { get; set; }//开始时间//out
        public string endTime { get; set; }//业务办理完结时间out
        public int businessCategory { get; set; }//业务种类//input,out
        public string queueNum { get; set; }//排队号,in,out
        public string serialNum { get; set; }//公安网六合一平台流水号,in
       
        public string IDum { get; set; }//身份证号码或公司组织结构代码,in
        public string BusinessUser { get; set; }
        public string processUser { get; set; }//办理民警in
        public int status { get; set; }//当前任务状态和进度in,out
      
        public string transferStatus { get; set; }//档案移交状态
        public string fileRecvUser { get; set; }//归档民警in
    }
}