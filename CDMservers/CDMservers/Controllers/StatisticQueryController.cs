using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CDMservers.Models;
using Common;
using Ionic.Zip;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class StatisticQueryController : ApiController
    {
        private Model1519 db = new Model1519();

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("BusinessVolumeQuery")]
        [HttpPost]
        public BusinessVolumeQueryResult BusinessVolumeQuery([FromBody] BusinessVolumeQuery param)
        {
            try
            {
                if (param == null)
                {
                    return new BusinessVolumeQueryResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("BusinessVolumeQuery input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(db, param.UserName, "BusinessVolumeQuery", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}
              
                switch (param.CountyCode)
                {
                    //case "changdao":
                   

                    //    break;
                    //case "zhaoyuan":
                  

                    //    break;
                    //case "penglai":
                  

                    //    break;
                    //case "laizhou":
                   

                    //    break;
                    //case "laiyang":
                    

                    //    break;
                    //case "longkou":
                  

                    //    break;
                    //case "muping":
                   

                    //    break;
                    //case "laishan":
                   

                    //    break;
                    //case "qixia":
                   

                    //    break;
                    //case "fushan":
                  

                    //    break;
                   
                    //case "haiyang":
                   
                    //    break;
                    case "zhifu":
                        var userlist = db.USERS.Where(q => q.COUNTYCODE == param.CountyCode);
                        var retlist = new List<OneUserVolume>();
                        var startdate = DateTime.Parse(param.StartTime);
                        var endtime = DateTime.Parse(param.EndTime);
                        foreach (USERS oneUsers in userlist)
                        {
                           var count= db.ZHIFUBUSINESS.Count(q => (q.PROCESS_USER == oneUsers.USERNAME ||
                               q.COMPLETE_PAY_USER == oneUsers.USERNAME ||
                               q.FILE_RECV_USER == oneUsers.USERNAME)&&
                               q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
                            retlist.Add(new OneUserVolume{UserName = oneUsers.USERNAME,Volume = count});
                        }
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "",Volumes = retlist};
                        break;
                    default:

                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result ="" };
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("BusinessVolumeQuery", ex);
                return new BusinessVolumeQueryResult { StatusCode = "000003", Result = ex.Message };
            }

        }
    }
}