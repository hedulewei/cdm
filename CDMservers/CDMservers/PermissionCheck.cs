using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;

namespace CDMservers
{
    public static class PermissionCheck
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool Check(BusinessModel bm)
        {
            using (var cdmdb=new UserDbc())
            {
                var user = cdmdb.USERS.FirstOrDefault(c => c.USERNAME == bm.userName);
                if (user == null) return false;
                if (user.DISABLED == false) return false;
                Log.InfoFormat("permissioionCheck-{0}-,-{1}-", int.Parse(user.AUTHORITYLEVEL), (int)AuthorityLevel.Administrator);
                if (int.Parse(user.AUTHORITYLEVEL) == (int)AuthorityLevel.Administrator) return true;
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
                if (user.DISABLED == false) return false;
              //  Log.InfoFormat("permissioionCheck-{0}-,-{1}-", int.Parse(user.AUTHORITYLEVEL), (int)AuthorityLevel.Administrator);
                switch ((AuthorityLevel)int.Parse(user.AUTHORITYLEVEL))
                {
                    case AuthorityLevel.CountyMagistrate:
                        //switch (bm.UserTransactionType)
                        //{
                        //        case UserTransactionType.GetUserList:
                        //        case UserTransactionType.Add:
                              //  case UserTransactionType.Update:
                                    if (bm.UserInfo.CountyCode != user.COUNTYCODE ) return false;
                        //        break;
                        //}
                       
                        break;
                    case AuthorityLevel.Administrator:
                        return true;
                        break;
                    default:
                        switch (bm.UserTransactionType)
                        {
                            case UserTransactionType.Update: return true;
                                break;
                        }
                        return false;
                        break;
                }
              //  var perm = JsonConvert.DeserializeObject<Dictionary<string, bool>>(user.LIMIT);
                return true;
            }
            return false;
        }
    }
}