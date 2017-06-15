using System;
using System.Collections.Generic;

namespace study
{
    public partial class Blobs
    {
        public Blobs()
        {
            Blogrolls = new HashSet<Blogrolls>();
        }

        public string Id { get; set; }
        public byte[] Bytes { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public DateTime Time { get; set; }

        public virtual ICollection<Blogrolls> Blogrolls { get; set; }
    }
}
