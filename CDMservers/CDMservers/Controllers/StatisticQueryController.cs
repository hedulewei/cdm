using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class StatisticQueryController : ApiController
    {
        private readonly Model1524 _db = new Model1524();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 业务量查询method
        /// </summary>
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

                Log.Info("BusinessVolumeQuery input:" + JsonConvert.SerializeObject(param));//file roll log

                LogIntoDb.Log(_db, param.UserName, "BusinessVolumeQuery", JsonConvert.SerializeObject(param));

                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}

                var userlist = _db.USERS.Where(q => q.COUNTYCODE == param.CountyCode);
                var retlist = new List<OneUserVolume>();
                var startdate = DateTime.Parse(param.StartTime);
                var endtime = DateTime.Parse(param.EndTime);

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
                        retlist.AddRange(from oneUsers in userlist
                                         let count = _db.ZHIFUBUSINESS.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                         let processcount = _db.ZHIFUBUSINESS.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                         let completecount = _db.ZHIFUBUSINESS.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                         select new OneUserVolume {UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount});
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