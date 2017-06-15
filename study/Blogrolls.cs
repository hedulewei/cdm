using System;
using System.Collections.Generic;

namespace study
{
    public partial class Blogrolls
    {
        public string Id { get; set; }
        public string AvatarId { get; set; }
        public string GitHubId { get; set; }
        public string NickName { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }

        public virtual Blobs Avatar { get; set; }
    }
}
