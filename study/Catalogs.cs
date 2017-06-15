using System;
using System.Collections.Generic;

namespace study
{
    public partial class Catalogs
    {
        public Catalogs()
        {
            Posts = new HashSet<Posts>();
        }

        public string Id { get; set; }
        public int Pri { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
