namespace Common
{
    public class CdmClient
    {
        public CdmClient()
        {
            ConnectId = string.Empty;
        }
        public string ConnectId { get; set; }
        public string CountyCode { get; set; }
        public ClientType ClientType { get; set; }
    }
}