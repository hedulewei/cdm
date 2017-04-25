using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using CDMservers.Models;
using Common;
using Newtonsoft.Json;

namespace CDMservers
{
    public static class PermissionCheck
    {
        public static bool Check(BusinessModel bm)
        {
            using (var cdmdb=new UserDbc())
            {
                var user = cdmdb.USERS.FirstOrDefault(c => c.USERNAME == bm.userName);
                if (user == null) return false;
                var perm = JsonConvert.DeserializeObject<Dictionary<string, bool>>(user.LIMIT);
                if (perm.Where(keyValuePair => bm.type.ToString(CultureInfo.InvariantCulture) == keyValuePair.Key).Any(keyValuePair => keyValuePair.Value))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool Check(UserTransaction bm)
        {
            using (var cdmdb = new UserDbc())
            {
                var user = cdmdb.USERS.FirstOrDefault(c => c.USERNAME == bm.UserName);
                if (user == null) return false;
                var perm = JsonConvert.DeserializeObject<Dictionary<string, bool>>(user.LIMIT);
                return true;
                //if (perm.Where(keyValuePair => bm.type.ToString(CultureInfo.InvariantCulture) == keyValuePair.Key).Any(keyValuePair => keyValuePair.Value))
                //{
                //    return true;
                //}
            }
            return false;
        }
    }
}