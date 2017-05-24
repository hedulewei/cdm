using System.Collections.Generic;

namespace Common
{
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