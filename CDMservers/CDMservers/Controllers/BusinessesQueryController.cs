using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
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

namespace CDMservers.Controllers
{
    public class BusinessesQueryController : ApiController
    {
        private Business db = new Business();

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string FileRootPath = ConfigurationManager.AppSettings["FileRootPath"];
        [Route("BusinessesPictureQuery")]
        [HttpPost]
        public ResultModel BusinessesPictureQuery([FromBody] BusinessModel param)
        {
            try
            {
                Log.Info("BusinessesPictureQuery input:" + JsonConvert.SerializeObject(param));
                if (!PermissionCheck.CheckLevelPermission(param))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                switch (param.countyCode)
                {
                    case "haiyang":
                    default:
                        if(db.Haiyangbusiness.Count(c=>c.SERIAL_NUM==param.serialNum)<1)
                            return new ResultModel
                            {
                                StatusCode = "000009",
                                Result = "没有找到相关业务 ！"
                            };
                        var busi = db.Haiyangbusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        
                        var fpath = (@FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
                        Log.Info("fpath is:" + fpath);
                        var fcontent = File.ReadAllBytes(@fpath);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID =(int) busi.ID,
                                name = busi.NAME,
                                IDum = busi.ID_NUM,
                                queueNum = busi.QUEUE_NUM,
                                address = busi.ADDRESS,
                                phoneNum = busi.PHONE_NUM,
                                attention = busi.ATTENTION,
                                zipFile = fcontent
                            }
                        };
                        break;
                }
                //return new ResultModel
                //{
                //    StatusCode = "000000",
                //    BussinessModel = new BusinessModel
                //    {
                //        type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                //        name = busi.NAME,
                //        IDum = busi.ID_NUM,
                //        queueNum = busi.QUEUE_NUM,
                //        address = busi.ADDRESS,
                //        phoneNum = busi.PHONE_NUM,
                //        attention = busi.ATTENTION,
                //        zipFile = fcontent
                //    }
                //};
            }
            catch (Exception ex)
            {
                Log.Error("BusinessesQuery", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
            
        }

        [Route("BusinessesQuery")]
        [HttpPost]
        public BusinessListResult BusinessesQuery([FromBody] BusinessModel param)
        {
            try
            {
                Log.Info("BusinessesQuery input:" + JsonConvert.SerializeObject(param));
                if (!PermissionCheck.CheckLevelPermission(param))
                {
                    return new BusinessListResult { StatusCode = "000007", Result = "没有权限" };
                }
                using (var cd = new Business())
                {
                    switch (param.countyCode)
                    {
                        case "haiyang":
                            return HaiyangBusinessInfo(cd, param);
                        case "fushan":
                            return HaiyangBusinessInfo(cd, param);
                        default:
                            return HaiyangBusinessInfo(cd, param);
                    }
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
                throw;
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("BusinessesQuery", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }
        }

        private BusinessListResult HaiyangBusinessInfo(Business cd, BusinessModel param)
        {

            IQueryable<haiyangbusiness> busi = cd.Haiyangbusiness.Where(c => c.COUNTYCODE == param.countyCode
                && (param.startTime == string.Empty || c.START_TIME.CompareTo(param.startTime) >= 0)
                 && (param.endTime == string.Empty || c.END_TIME.CompareTo(param.endTime) <= 0)
                   && (param.type == -1 || c.TYPE == param.type)
                    && (param.queueNum == string.Empty || c.QUEUE_NUM == param.queueNum)
                     && (param.serialNum == string.Empty || c.SERIAL_NUM == param.serialNum)
                       && (param.IDum == string.Empty || c.ID_NUM == param.IDum)
                         && (param.BusinessUser == string.Empty || c.NAME == param.BusinessUser)
                           && (param.processUser == string.Empty || c.PROCESS_USER == param.processUser)
                            && (param.status == -1 || c.STATUS == param.status)
                //    && (param.transferStatus ==string.Empty || c.TRANSFER_STATUS == decimal.Parse(param.transferStatus))
                );
            if (param.transferStatus != string.Empty)
            {
                var aa = decimal.Parse(param.transferStatus);
                busi = cd.Haiyangbusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(10);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (haiyangbusiness hy in busi)
            {

                ret.BussinessList.Add(new BusinessModel
                {
                    type = int.Parse(hy.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = hy.NAME,
                    IDum = hy.ID_NUM,
                    queueNum = hy.QUEUE_NUM,
                    startTime = hy.START_TIME,
                    serialNum = hy.SERIAL_NUM,
                    endTime = hy.END_TIME,
                    userName = hy.NAME,
                    address = hy.ADDRESS,
                    phoneNum = hy.PHONE_NUM,
                    attention = hy.ATTENTION,
                    processUser = hy.PROCESS_USER,
                    status = (int)hy.STATUS,
                    transferStatus = hy.TRANSFER_STATUS.ToString(),
                    countyCode = hy.COUNTYCODE,
                });
            }

            return ret;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}