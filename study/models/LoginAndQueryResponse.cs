namespace study
{
    public class LoginAndQueryResponse : CommonResponse
    {
        public string Token { get; set; }
        public bool AllowedToStudy { get; set; }
        public string AllStatus { get; set; }
    }
}
