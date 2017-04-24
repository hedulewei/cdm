using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.UI.WebControls;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class UsersController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        [Route("UserTransaction")]
        [HttpPost]
        public SimpleResult UserTransaction([FromBody] UserTransaction param)
        {
            try
            {
                Log.Info("UserTransaction input:" + JsonConvert.SerializeObject(param));
                var userslist = new List<PoliceUser>();
                switch (param.UserTransactionType)
                {
                    case UserTransactionType.Add:
                        using (var userdb = new UserDbc())
                        {
                            var u = new USERS
                            {
                                AUTHORITYLEVEL = ((int)param.UserInfo.AuthorityLevel).ToString(),
                                COUNTYCODE = param.UserInfo.CountyCode,
                                LIMIT = JsonConvert.SerializeObject(param.UserInfo.Permission),
                                PASSWORD = param.UserInfo.Password,
                                POLICENUM = param.UserInfo.PoliceCode,
                                ID = new Random().Next(),
                                DEPARTMENT = " ff",
                                POST=param.UserInfo.UserRole.ToString(),
                               USERNAME=param.UserInfo.UserName,
                                REALNAME=param.UserInfo.RealName,
                            };

                            userdb.USERS.Add(u);
                            userdb.SaveChanges();
                        }
                        break;
                    case UserTransactionType.Disable:
                        using (var userdb = new UserDbc())
                        {
                            var theuser =
                                userdb.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                            theuser.DISABLED = false;
                            userdb.SaveChanges();
                        }
                        break;
                    case UserTransactionType.Update:
                        using (var userdb = new UserDbc())
                        {
                            var theuser =
                                userdb.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                            theuser.REALNAME = param.UserInfo.RealName;
                            theuser.LIMIT =JsonConvert.SerializeObject( param.UserInfo.Permission);
                            userdb.SaveChanges();
                        }
                        break;
                    case UserTransactionType.ResetPass:
                        using (var userdb = new UserDbc())
                        {
                            var theuser =
                                userdb.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                            theuser.PASSWORD = param.UserInfo.Password;
                            userdb.SaveChanges();
                        }
                        break;
                    case UserTransactionType.GetUserList:
                        using (var userdb = new UserDbc())
                        {
                            var theuser =
                                userdb.USERS.Where(a => a.COUNTYCODE == param.UserInfo.CountyCode);
                            foreach (USERS users in theuser)
                            {
                                userslist.Add(new PoliceUser
                                {
                                    AuthorityLevel=(AuthorityLevel)int.Parse(users.AUTHORITYLEVEL),CountyCode=users.COUNTYCODE,
                                    Notation = string.Empty,Password=users.PASSWORD,Permission=JsonConvert.DeserializeObject<Dictionary<string,bool>>(users.LIMIT),
                                    PoliceCode=users.POLICENUM,RealName=users.REALNAME,UserName=users.USERNAME,UserRole=(UserRole)int.Parse(users.POST)
                                });
                            }
                        }
                        break;
                    default:
                    
                        break;
                }

                return new SimpleResult { StatusCode = "000000", Content = "",Users=userslist };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Log.InfoFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Log.InfoFormat("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new SimpleResult { StatusCode = "000003", Content = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("UserTransaction", ex);
                return new SimpleResult { StatusCode = "000003", Content = ex.Message };
            }
        }
    }
}
