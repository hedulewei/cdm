using System.Collections.Generic;

namespace Common
{
    public class PoliceUser
    {
        public AuthorityLevel AuthorityLevel { get; set; }
        public string RealName { get; set; }
        public string UserName { get; set; }
        public string PoliceCode { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; }
        public string CountyCode { get; set; }
        public UserRole UserRole { get; set; }
        public string Notation { get; set; }
        public Dictionary<string,bool> Permission { get; set; }
    }
}