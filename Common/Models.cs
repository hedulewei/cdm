using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    public enum VoiceType { Fee, Reject ,PlayOver}
    public enum LedMsgType { Processing, Reject ,Done}

    public enum ClientType { Voice, Led }

    public enum UserRole { Audit, Accept, Pay, Certificate }

    public class OrdinalInput
    {
        public string countyCode { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string counterNum { get; set; }
    }
}
