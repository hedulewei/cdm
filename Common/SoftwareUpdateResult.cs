namespace Common
{
    public class SoftwareUpdateResult: CommonResult
    {
        public string NewVersionFileName { get; set; }// empty代表没有新版本，否则代表新版本文件名
        public byte[] FileContent { get; set; }//文件二进制流内容
    }
}