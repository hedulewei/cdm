using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.UI.WebControls;
using CDMservers.Models;
using Common;
using log4net;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

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
                if (!PermissionCheck.Check(param))
                {
                    return new SimpleResult { StatusCode = "000007", Content = "没有权限" };
                }
                var userslist = new List<PoliceUser>();
                switch (param.UserTransactionType)
                {
                    case UserTransactionType.Add:
                        Log.Info("in user add-----------------");
                        using (var userdb = new UserDbc())
                        {
                            var u = new USERS
                            {
                                AUTHORITYLEVEL = ((int)param.UserInfo.AuthorityLevel).ToString(),
                                COUNTYCODE = param.UserInfo.CountyCode,
                                LIMIT = JsonConvert.SerializeObject(param.UserInfo.Permission),
                                PASSWORD = CdmEncrypt(param.UserInfo.Password),
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
                            if (theuser == null)
                            {
                                return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                            }
                            theuser.DISABLED = false;
                            userdb.SaveChanges();
                        }
                        break;
                    case UserTransactionType.Update:
                        using (var userdb = new UserDbc())
                        {
                            var theuser =
                                userdb.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                            if (theuser == null)
                            {
                                return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                            }
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
                            if (theuser == null)
                            {
                                return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                            }
                            theuser.PASSWORD =CdmEncrypt( param.UserInfo.Password);
                            userdb.SaveChanges();
                        }
                        break;
                    case UserTransactionType.GetUserList:
                        using (var userdb = new UserDbc())
                        {
                            var theuser =
                                userdb.USERS.Where(a => a.COUNTYCODE == param.UserInfo.CountyCode);
                            userslist.AddRange(theuser.Select(users => new PoliceUser
                            {
                                AuthorityLevel = (AuthorityLevel) int.Parse(users.AUTHORITYLEVEL),
                                CountyCode = users.COUNTYCODE,
                                Notation = string.Empty,
                                Password = users.PASSWORD,
                                Permission = JsonConvert.DeserializeObject<Dictionary<string, bool>>(users.LIMIT),
                                PoliceCode = users.POLICENUM,
                                RealName = users.REALNAME, 
                                UserName = users.USERNAME,
                                UserRole = (UserRole) int.Parse(users.POST)
                            }));
                        }

                        return new SimpleResult { StatusCode = "000000", Content = "", Users = userslist };
                        break;
                    default:
                        using (var userdb = new UserDbc())
                        {
                            var theuser =
                                userdb.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                            if (theuser == null)
                            {
                                return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                            }
                            if(theuser.PASSWORD != CdmEncrypt(param.UserInfo.Password))
                            {
                                return new SimpleResult { StatusCode = "000004", Content = "密码错误" };
                            }
                        }
                        break;
                }
                Log.Info("before ok----------------");
                return new SimpleResult { StatusCode = "000000", Content = "ok" };
            }
            catch (DbEntityValidationException e)
            {
                var err = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    var err1 =
                        string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    err += err1;
                    Log.InfoFormat(err1);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var err2 = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        Log.InfoFormat(err2);
                        err += err2;
                    }
                }
                return new SimpleResult { StatusCode = "000003", Content = "DbEntityValidationException:"+err };
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new SimpleResult { StatusCode = "000003", Content = "EntityDataSourceValidationException:"+ex.Message };
            }
            catch (OracleException ex)
            {
                Log.Error("OracleException", ex);
                return new SimpleResult { StatusCode = "000003", Content = "OracleException:" + ex.Message };
            }
            catch (DbUpdateException ex)
            {
                Log.Error("DbUpdateException", ex);
                return new SimpleResult { StatusCode = "000003", Content = "DbUpdateException:" +( ex.ToString().Contains("违反唯一约束条件 (CITY.USERSNAME)")?"用户名已经存在":ex.ToString()) };
            }
            catch (Exception ex)
            {
                Log.Error("UserTransaction", ex);
                return new SimpleResult { StatusCode = "000003", Content = ex.Message };
            }
        }

        private string CdmEncrypt(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(password);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
           return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
        }
    }
}
