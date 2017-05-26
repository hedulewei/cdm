namespace Common
{
    public class IdentityInforUploadRequest : BasisRequest
    {
        public string IdentityCardNumber { get; set; }//
        public string FingerprintOne { get; set; }//
        public string FingerprintTwo { get; set; }//
        public string IrisLeft { get; set; }//
        public string IrisRight { get; set; }//
    }
}