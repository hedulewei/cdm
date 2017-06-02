using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CDMservers.Models;
using Common;
using log4net;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Globalization;
using System.Data.Entity.Validation;
using System.Web.UI.WebControls;

namespace CDMservers.Controllers
{
    public class BusinessNewController : ApiController
    {
        private readonly Model1525 cd = new Model1525();
        private readonly UserDbc _dbuUserDbc = new UserDbc();
        private readonly NewDblog _dbLog = new NewDblog();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cd.Dispose();
                _dbuUserDbc.Dispose();
                _dbLog.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("BusinessesQueryEx")]
        [HttpPost]
        public BusinessListResult BusinessesQueryEx([FromBody] BusinessQueryRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new BusinessListResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("BusinessesQuery input:" + JsonConvert.SerializeObject(param));

                switch (param.CountyCode)
                {
                    case  CountyCode.ChangDao: return ChangdaoBusinessInfo(param);
                    case CountyCode.ZhaoYuan: return ZhaoyuanBusinessInfo(param);
                    case CountyCode.PengLai: return PenglaiBusinessInfo(param);
                    case CountyCode.LaiZhou: return LaizhouBusinessInfo(param);
                    case CountyCode.LaiYang: return LaiyangBusinessInfo(param);

                    case CountyCode.LongKou: return LongkouBusinessInfo(param);
                    case CountyCode.MuPing: return MupingBusinessInfo(param);
                    case CountyCode.LaiShan: return LaishanBusinessInfo(param);
                    case CountyCode.QiXia: return QixiaBusinessInfo(param);
                    case CountyCode.FuShan: return FushanBusinessInfo(param);

                    case CountyCode.HaiYang: return HaiyangBusinessInfo(param);
                    case CountyCode.ShiSuo:
                    case CountyCode.DaCheng:
                    case CountyCode.ZhiFu:
                        return ZhifuBusinessInfo(param);
                    default:
                        var ret=new BusinessListResult { StatusCode = "000000", Result = ""  };
                        var blist = new List<BusinessModel>();
                        blist.AddRange(ZhifuBusinessInfo(param).BussinessList);
                        blist.AddRange(ChangdaoBusinessInfo(param).BussinessList);

                        blist.AddRange(ZhaoyuanBusinessInfo(param).BussinessList);
                        blist.AddRange(PenglaiBusinessInfo(param).BussinessList);
                        blist.AddRange(LaizhouBusinessInfo(param).BussinessList);
                        blist.AddRange(LaiyangBusinessInfo(param).BussinessList);
                        blist.AddRange(HaiyangBusinessInfo(param).BussinessList);

                        blist.AddRange(LongkouBusinessInfo(param).BussinessList);
                        blist.AddRange(MupingBusinessInfo(param).BussinessList);
                        blist.AddRange(LaishanBusinessInfo(param).BussinessList);
                        blist.AddRange(QixiaBusinessInfo(param).BussinessList);
                        blist.AddRange(FushanBusinessInfo(param).BussinessList);
                        ret.BussinessList = blist;
                        return ret;
                }
               
             
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
                return new BusinessListResult { StatusCode = "000003", Result = e.Message };
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("BusinessesQueryEx", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }
        }
        private BusinessListResult ChangdaoBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_CHANGDAO.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult ZhaoyuanBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_ZHAOYUAN.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult PenglaiBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_PENGLAI.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult LaizhouBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_LAIZHOU.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult LaiyangBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_LAIYANG.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult LongkouBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_LONGKOU.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult MupingBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_MUPING.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult LaishanBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_LAISHAN.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult QixiaBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_QIXIA.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult FushanBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_FUSHAN.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult HaiyangBusinessInfo(BusinessQueryRequest param)
        {
            var busi = cd.BUSINESS_HAIYANG.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))
                    // && (param.startTime == string.Empty || c.START_TIME.CompareTo(DateTime.Parse(param.startTime)) >= 0)
                    // && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                    //   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            //  BusinessCategory businessCategory;
            //  Log.InfoFormat("before try {0}", param.businessCategory.ToString(CultureInfo.InvariantCulture));
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
        private BusinessListResult ZhifuBusinessInfo(BusinessQueryRequest param)
        {
            Log.InfoFormat("countycode={0},{1}", param.CountyCode, param.CountyCode.ToString().ToLower());
            var busi = cd.ZHIFUBUSINESS.Where(c => (((int)param.CountyCode == -1 || c.COUNTYCODE == param.CountyCode.ToString().ToLower()))

                   && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.fileRecvUser == string.Empty || c.FILE_RECV_USER == param.fileRecvUser)
                            && (param.status == -1 || c.STATUS == param.status)

                );
            Log.InfoFormat("countycode={0},{1}", param.CountyCode, busi.Count());
            if (Enum.IsDefined(typeof(BusinessCategory), param.businessCategory))
            {
                // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                busi = busi.Where(c => c.QUEUE_NUM.StartsWith(ttt));
            }
            if (!string.IsNullOrEmpty(param.startTime))
            {
                var stime = DateTime.Parse(param.startTime);
                busi = busi.Where(c => c.START_TIME.CompareTo(stime) >= 0);
            }
            if (!string.IsNullOrEmpty(param.endTime))
            {
                var stime = DateTime.Parse(param.endTime);
                busi = busi.Where(c => c.END_TIME.CompareTo(stime) <= 0);
            }
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = busi.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (var hy in busi)
            {
                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)hy.ID,
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME.ToString(CultureInfo.InvariantCulture),
                    serialNum = hy.SERIAL_NUM,
                    endTime = (int)hy.STATUS == (int)BusinessStatus.Fee || (int)hy.STATUS == (int)BusinessStatus.Paid || (int)hy.STATUS == (int)BusinessStatus.License ? hy.END_TIME.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                    postAddr = hy.POSTADDR,
                    postPhone = hy.POSTPHONE,
                    texNum = hy.TAX_NUM,
                    texType = hy.TAX_TYPE,
                    originNum = hy.ORIGIN_NUM,
                    originType = hy.ORIGIN_TYPE,
                    carNum = hy.CAR_NUM,

                    uploader = hy.UPLOADER,
                    rejectReason = hy.REJECT_REASON,
                });
            }

            return ret;
        }
    }
}