using System;
using System.Collections.Generic;

namespace study
{
    public partial class Posts
    {
        public Posts()
        {
            Posttags = new HashSet<Posttags>();
        }

        public string Id { get; set; }
        public string CatalogId { get; set; }
        public string Content { get; set; }
        public bool IsPage { get; set; }
        public string Summary { get; set; }
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Posttags> Posttags { get; set; }
        public virtual Catalogs Catalog { get; set; }
    }
}
