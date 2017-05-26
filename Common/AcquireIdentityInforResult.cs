namespace Common
{
    public class AcquireIdentityInforResult : CommonResult
    {
     
        public string Address { get; set; }//
        public string Name { get; set; }//
        public string IdentityCardNumber { get; set; }//
     
        public Gender Gender { get; set; }//
        public string Nationality { get; set; }//
        public string Birthday { get; set; }//

        public string Postcode { get; set; }//
        public string PostAddress { get; set; }//
        public string Mobile { get; set; }//
        public string Telephone { get; set; }//
        public string Email { get; set; }//
        public string FingerprintOne { get; set; }//
        public string FingerprintTwo { get; set; }//
        public string IrisLeft { get; set; }//
        public string IrisRight { get; set; }//
    }
}