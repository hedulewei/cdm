using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    public enum BusinessStatus {AutoSubmit, Upload, PartUpload, Processing, Reject, Fee,Discard,Paid,License }
    public enum VoiceType { Fee, Reject }
    public enum LedMsgType { Processing, Reject ,Done}
    public enum CountyCode { HaiYang, FuShan,QiXia }
   
    public enum ClientType { Voice, Led }

    public enum UserRole { Audit, Accept, Pay, Certificate }
    public enum UserTransactionType { Add, Update, GetUserList, Disable,ResetPass ,Login,ChangePass}
    public enum AuthorityLevel { Ordinary, CountyMagistrate, Administrator }

    public class OrdinalInput
    {
        public string countyCode { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string counterNum { get; set; }
    }
}
