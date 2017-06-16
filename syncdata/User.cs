using System;
using System.Collections.Generic;

namespace syncdata
{
    public partial class User
    {
        public string Identity { get; set; }
        public string Drugrelated { get; set; }
        public string Fullmark { get; set; }
        public string Inspect { get; set; }
        public string Licensetype { get; set; }
        public string Name { get; set; }
        public DateTime? Noticedate { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public string Stoplicense { get; set; }
        public DateTime? Studycompletedate { get; set; }
        public string Studylog { get; set; }
        public DateTime? Studystartdate { get; set; }
        public DateTime Syncdate { get; set; }
        public string Wechat { get; set; }
    }
}
