using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace CDMservers.Controllers
{
    public class OperatorsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserDbc _db = new UserDbc();
        private readonly NewDblog _dbLog = new NewDblog();

        [Route("UserTransaction")]
        [HttpPost]
        public SimpleResult UserTransaction([FromBody] UserTransaction param)
        {
            try
            {
                Log.Info("UserTransaction input:" + JsonConvert.SerializeObject(param));
                if (param.UserTransactionType == UserTransactionType.Login)
                {

                    var theuser =
                        _db.USERS.FirstOrDefault(a => a.USERNAME == param.UserName);
                    if (theuser == null)
                    {
                        return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserName };
                    }
                    if (theuser.PASSWORD != CdmEncrypt(param.Password))
                    {
                        return new SimpleResult { StatusCode = "000004", Content = "密码错误" };
                    }
                    return new SimpleResult
                    {
                        StatusCode = "000000",
                        Content = "ok",
                        Users =
                            {
                                new PoliceUser
                                {
                                    AuthorityLevel = (AuthorityLevel) int.Parse(theuser.AUTHORITYLEVEL),
                                    RealName = theuser.REALNAME,
                                    CountyCode = theuser.COUNTYCODE,
                                    Permission = JsonConvert.DeserializeObject<Dictionary<string,bool>>(theuser.LIMIT),
                                    PoliceCode = theuser.POLICENUM
                                }
                            }
                    };

                }

                if (!PermissionCheck.Check(param))
                {
                    return new SimpleResult { StatusCode = "000007", Content = "没有权限" };
                }
                var userslist = new List<PoliceUser>();
                switch (param.UserTransactionType)
                {
                    case UserTransactionType.Add:

                        var u = new USERS
                        {
                            AUTHORITYLEVEL = ((int)param.UserInfo.AuthorityLevel).ToString(),
                            COUNTYCODE = param.UserInfo.CountyCode,
                            LIMIT = JsonConvert.SerializeObject(param.UserInfo.Permission),
                            PASSWORD = CdmEncrypt("888888"),
                            POLICENUM = param.UserInfo.PoliceCode,
                            ID = new Random().Next(),
                            DEPARTMENT = " ff",
                            POST = param.UserInfo.UserRole.ToString(),
                            DISABLED = true,
                            USERNAME = param.UserInfo.UserName,
                            REALNAME = param.UserInfo.RealName,
                        };

                        _db.USERS.Add(u);
                        _db.SaveChanges();

                        break;
                    case UserTransactionType.Disable:

                        var disableuser =
                            _db.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                        if (disableuser == null)
                        {
                            return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                        }
                        disableuser.DISABLED = false;
                        _db.SaveChanges();

                        break;
                    case UserTransactionType.Update:

                        var userUpdate =
                            _db.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                        if (userUpdate == null)
                        {
                            return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                        }
                        userUpdate.REALNAME = param.UserInfo.RealName;
                        userUpdate.COUNTYCODE = param.UserInfo.CountyCode;
                        userUpdate.POST = ((int)param.UserInfo.UserRole).ToString();
                        _db.SaveChanges();

                        break;
                    case UserTransactionType.PermissionUpdate:

                        var userPu =
                            _db.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                        if (userPu == null)
                        {
                            return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                        }
                        userPu.LIMIT = JsonConvert.SerializeObject(param.UserInfo.Permission);
                        _db.SaveChanges();

                        break;
                    case UserTransactionType.ResetPass:

                        var rpUser =
                            _db.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                        if (rpUser == null)
                        {
                            return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                        }
                        rpUser.PASSWORD = CdmEncrypt("888888");
                        _db.SaveChanges();

                        break;
                    //case UserTransactionType.GetUserList:
                    //    using (var userdb = new UserDbc())
                    //    {
                    //        var theuser =
                    //            userdb.USERS.Where(a => a.COUNTYCODE == param.UserInfo.CountyCode);
                    //        userslist.AddRange(theuser.Select(users => new PoliceUser
                    //        {
                    //          //  AuthorityLevel = (AuthorityLevel) int.Parse(users.AUTHORITYLEVEL),
                    //            CountyCode = users.COUNTYCODE,
                    //            Notation = string.Empty,
                    //         //   Permission = JsonConvert.DeserializeObject<Dictionary<string, bool>>(users.LIMIT),
                    //            PoliceCode = users.POLICENUM,
                    //            RealName = users.REALNAME, 
                    //            UserName = users.USERNAME,
                    //          //  UserRole = (UserRole) int.Parse(users.POST)
                    //        }));
                    //    }

                    //    return new SimpleResult { StatusCode = "000000", Content = "", Users = userslist };
                    //    break;
                    //default:// for test
                    case UserTransactionType.GetUserList:

                        var theuser =
                            _db.USERS.Where(a => a.COUNTYCODE == param.UserInfo.CountyCode);
                        foreach (USERS users in theuser)
                        {
                            var pu = new PoliceUser();
                            Log.InfoFormat("from db:{0}", users.LIMIT);
                            pu.Permission = JsonConvert.DeserializeObject<Dictionary<string, bool>>(users.LIMIT);
                            pu.CountyCode = users.COUNTYCODE;
                            pu.Notation = string.Empty;
                            pu.PoliceCode = users.POLICENUM;
                            pu.RealName = users.REALNAME;
                            pu.UserName = users.USERNAME;
                            pu.AuthorityLevel = (AuthorityLevel)int.Parse(users.AUTHORITYLEVEL);
                            userslist.Add(pu);
                        }
                        //userslist.AddRange(theuser.Select(users => new PoliceUser
                        //{
                        //    //  AuthorityLevel = (AuthorityLevel) int.Parse(users.AUTHORITYLEVEL),
                        //    CountyCode = users.COUNTYCODE,
                        //    Notation = string.Empty,
                        //       Permission = JsonConvert.DeserializeObject<Dictionary<string, bool>>(users.LIMIT),
                        //    PoliceCode = users.POLICENUM,
                        //    RealName = users.REALNAME,
                        //    UserName = users.USERNAME,
                        //    //  UserRole = (UserRole) int.Parse(users.POST)
                        //}));
                        // Task.Run( () => LogIntoDb.Log(_dbLog, param.UserName, DateTime.Now, param.UserTransactionType.ToString(), JsonConvert.SerializeObject(param)));
                        LogIntoDb.Log(_dbLog, param.UserName, DateTime.Now, param.UserTransactionType.ToString(), JsonConvert.SerializeObject(param), HttpContext.Current.Request.UserHostAddress);
                        return new SimpleResult { StatusCode = "000000", Content = "", Users = userslist };
                        break;
                }
                //  Log.Info("before ok----------------");
                LogIntoDb.Log(_dbLog, param.UserName, DateTime.Now, param.UserTransactionType.ToString(), JsonConvert.SerializeObject(param), HttpContext.Current.Request.UserHostAddress);
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
                return new SimpleResult { StatusCode = "000003", Content = "DbEntityValidationException:" + err };
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new SimpleResult { StatusCode = "000003", Content = "EntityDataSourceValidationException:" + ex.Message };
            }
            catch (OracleException ex)
            {
                Log.Error("OracleException", ex);
                return new SimpleResult { StatusCode = "000003", Content = "OracleException:" + ex.Message };
            }
            catch (DbUpdateException ex)
            {
                Log.Error("DbUpdateException", ex);
                return new SimpleResult { StatusCode = "000003", Content = "DbUpdateException:" + (ex.ToString().Contains("违反唯一约束条件 (CITY.USERSNAME)") ? "用户名已经存在" : ex.ToString()) };
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
                _dbLog.Dispose();
            }
            base.Dispose(disposing);
        }
        public static string GetIP()
        {
            try
            {
                //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
                string userHostAddress =
                    HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                //否则直接读取REMOTE_ADDR获取客户端IP地址
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.UserHostAddress;
                }
                //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
                if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
                {
                    return userHostAddress;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.Length > 20 ? ex.Message.Substring(0, 20) : ex.Message;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

    }
}