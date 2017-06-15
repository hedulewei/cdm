using System;
using System.Collections.Generic;

namespace study
{
    public partial class Posttags
    {
        public int Id { get; set; }
        public string PostId { get; set; }
        public string Tag { get; set; }

        public virtual Posts Post { get; set; }
    }
}
