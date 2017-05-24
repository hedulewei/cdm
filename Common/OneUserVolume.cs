namespace Common
{
    public class OneUserVolume
    {
        public string UserName { get; set; }
        public int UploadVolume { get; set; }//业务量
        public int ProcessVolume { get; set; }//业务量
        public int CompletePayVolume { get; set; }//业务量
    }
}