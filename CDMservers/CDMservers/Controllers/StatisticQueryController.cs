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
        private readonly Model1525 _db = new Model1525();
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

                switch (param.CountyCode.ToLower())
                {
                    case "changdao": retlist.AddRange(from oneUsers in userlist
                                                      let count = _db.BUSINESS_CHANGDAO.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                      let processcount = _db.BUSINESS_CHANGDAO.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                      let completecount = _db.BUSINESS_CHANGDAO.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                      select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "zhaoyuan": retlist.AddRange(from oneUsers in userlist
                                                      let count = _db.BUSINESS_ZHAOYUAN.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                      let processcount = _db.BUSINESS_ZHAOYUAN.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                      let completecount = _db.BUSINESS_ZHAOYUAN.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                      select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "penglai": retlist.AddRange(from oneUsers in userlist
                                                     let count = _db.BUSINESS_PENGLAI.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let processcount = _db.BUSINESS_PENGLAI.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let completecount = _db.BUSINESS_PENGLAI.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "laizhou": retlist.AddRange(from oneUsers in userlist
                                                     let count = _db.BUSINESS_LAIZHOU.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let processcount = _db.BUSINESS_LAIZHOU.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let completecount = _db.BUSINESS_LAIZHOU.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "laiyang":
                           retlist.AddRange(from oneUsers in userlist
                                         let count = _db.BUSINESS_LAIYANG.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                            let processcount = _db.BUSINESS_LAIYANG.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                            let completecount = _db.BUSINESS_LAIYANG.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                         select new OneUserVolume {UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount});
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "",Volumes = retlist};

                    case "longkou": retlist.AddRange(from oneUsers in userlist
                                                     let count = _db.BUSINESS_LONGKOU.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let processcount = _db.BUSINESS_LONGKOU.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let completecount = _db.BUSINESS_LONGKOU.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "muping": retlist.AddRange(from oneUsers in userlist
                                                    let count = _db.BUSINESS_MUPING.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                    let processcount = _db.BUSINESS_MUPING.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                    let completecount = _db.BUSINESS_MUPING.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                    select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "laishan": retlist.AddRange(from oneUsers in userlist
                                                     let count = _db.BUSINESS_LAISHAN.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let processcount = _db.BUSINESS_LAISHAN.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let completecount = _db.BUSINESS_LAISHAN.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "qixia": retlist.AddRange(from oneUsers in userlist
                                                   let count = _db.BUSINESS_QIXIA.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                   let processcount = _db.BUSINESS_QIXIA.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                   let completecount = _db.BUSINESS_QIXIA.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                   select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "fushan":
                           retlist.AddRange(from oneUsers in userlist
                                         let count = _db.BUSINESS_FUSHAN.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                            let processcount = _db.BUSINESS_FUSHAN.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                            let completecount = _db.BUSINESS_FUSHAN.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                         select new OneUserVolume {UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount});
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "",Volumes = retlist};
                    case "haiyang": retlist.AddRange(from oneUsers in userlist
                                                     let count = _db.BUSINESS_HAIYANG.Count(q => q.UPLOADER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let processcount = _db.BUSINESS_HAIYANG.Count(q => q.PROCESS_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     let completecount = _db.BUSINESS_HAIYANG.Count(q => q.COMPLETE_PAY_USER == oneUsers.USERNAME && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0)
                                                     select new OneUserVolume { UserName = oneUsers.USERNAME, UploadVolume = count, ProcessVolume = processcount, CompletePayVolume = completecount });
                        return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "", Volumes = retlist };
                    case "zhifu":
                    case "shisuo":
                    case "dacheng":
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