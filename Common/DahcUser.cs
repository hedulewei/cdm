using System.Collections.Generic;

namespace Common
{
    public class ArchivesTransferRequest
    {
        public string CountyCode { get; set; }
        public string UserName { get; set; }
        public List<int> Ids { get; set; }//
    }
    public class DahcUser
    {
        public string UserName { get; set; }
        public int Id { get; set; }//
    }
    public class CommonResult
    {
        public string StatusCode { get; set; }
        public string Result { get; set; }
    }
    public class UsersOfArchiveQueryResult 
    {
        public UsersOfArchiveQueryResult()
        {
            DahcUsers = new List<DahcUser>();
        }
        public string StatusCode { get; set; }
        public string Result { get; set; }
        public List<DahcUser> DahcUsers { get; set; }//list
    }
}