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
using System.Threading.Tasks;
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
    public class CrossDistrictController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private UserDbc db = new UserDbc();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("CrossDistrictAuthorization")]
        [HttpPost]
        public  SimpleResult CrossDistrictAuthorization([FromBody] UserTransaction param)
        {
            try
            {
                if (param == null)
                {
                    return new SimpleResult { StatusCode = "000003", Content = "请求错误，请检查输入参数！" };
                }
                Log.Info("CrossDistrictAuthorization input:" + JsonConvert.SerializeObject(param));
                var theuser = db.USERS.FirstOrDefault(a => a.USERNAME == param.UserName);
                if (theuser == null)
                    return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserName };
                if(theuser.AUTHORITYLEVEL!=((int)AuthorityLevel.Administrator).ToString())
                    return new SimpleResult { StatusCode = "000007", Content = param.UserName + "没有权限" };
                var userUpdate =
                              db.USERS.FirstOrDefault(a => a.USERNAME == param.UserInfo.UserName);
                if (userUpdate == null)
                {
                    return new SimpleResult { StatusCode = "000005", Content = "无此用户:" + param.UserInfo.UserName };
                }
                var perm = JsonConvert.DeserializeObject<Dictionary<string, bool>>(userUpdate.LIMIT);
                foreach (var kv in param.UserInfo.Permission)
                {
                    if (perm.ContainsKey(kv.Key))
                        perm[kv.Key] = kv.Value;
                    else
                    {
                        perm.Add(kv.Key, kv.Value);
                    }
                   
                }
                userUpdate.LIMIT = JsonConvert.SerializeObject(perm);
                 db.SaveChangesAsync();
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
                return new SimpleResult { StatusCode = "000003", Content = "DbUpdateException:" +  ex };
            }
            catch (Exception ex)
            {
                Log.Error("CrossDistrictAuthorization", ex);
                return new SimpleResult { StatusCode = "000003", Content = ex.Message };
            }

            return new SimpleResult
            {
                StatusCode = "000000",
                Content = "ok"
            };
        }
       
    }
}