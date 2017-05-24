using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CDMservers.Models;
using Newtonsoft.Json;

namespace CDMservers
{
    public static class UserService
    {
        public static List<int> GetPermissionType(DbSet<USERS> usertable,string username)
        {
            var ret = new List<int>();
            var user = usertable.FirstOrDefault(c => c.USERNAME == username);
            if (user == null) return ret;
            var limit = JsonConvert.DeserializeObject<Dictionary<string, bool>>(user.LIMIT);
            foreach (KeyValuePair<string, bool> keyValuePair in limit)
            {
                if (keyValuePair.Value&&keyValuePair.Key.Length==5)
                {
                    int permtype;
                    if (int.TryParse(keyValuePair.Key.Substring(1), out permtype))
                    {
                        ret.Add(permtype);
                    }
                }
            }
            return ret;
        } 
    }
}