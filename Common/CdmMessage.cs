namespace Common
{
    public class CdmMessage
    {
        public CdmMessage()
        {
            CountyCode = string.Empty;
            ClientType= ClientType.Led;
        }
        public string CountyCode { get; set; }
        public ClientType ClientType { get; set; }
        public VoiceType VoiceType { get; set; }
        public LedMsgType LedMsgType { get; set; }
        public string Content { get; set; }

    }
}