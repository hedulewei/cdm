using System.Collections.Generic;

namespace Common
{
    public class ArchivesTransferRequest
    {
        public string CountyCode { get; set; }
        public string UserName { get; set; }
        public List<int> Ids { get; set; }//
    }
}