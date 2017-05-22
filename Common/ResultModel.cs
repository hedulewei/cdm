namespace Common
{
    public class ResultModel
    {

        public ResultModel()
        {
            BussinessModel = new BusinessModel();
        }
        public string StatusCode { get; set; }
        public string Result { get; set; }
        public BusinessModel BussinessModel { get; set; }
    }
}