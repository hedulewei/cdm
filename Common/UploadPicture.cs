namespace Common
{
    public class UploadPicture
    {
        public string CountyCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Kind { get; set; }//业务种类//input,out
        public int Id { get; set; }//每笔业务编号，唯一//out
        public string FileName { get; set; }//文件名
        public byte[] FileContent { get; set; }//图片文件二进制流内容
    }
    public class UploadPictureResult
    {

        public string StatusCode { get; set; }
        public string Result { get; set; }
        public int Id { get; set; }//每笔业务编号，唯一//out
        public string FileName { get; set; }//文件名
    }
}