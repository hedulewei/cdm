using System.Collections.Generic;

namespace Common
{
    public class SimpleResult
    {
        public SimpleResult()
        {
            Users = new List<PoliceUser>();
        }
        public string StatusCode { get; set; }
        public string Content { get; set; }
        public List<PoliceUser> Users { get; set; }
    }
}