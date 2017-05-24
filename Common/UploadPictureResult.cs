namespace Common
{
    public class UploadPictureResult
    {

        public string StatusCode { get; set; }
        public string Result { get; set; }
        public int Id { get; set; }//每笔业务编号，唯一//out
        public string FileName { get; set; }//文件名
    }
}