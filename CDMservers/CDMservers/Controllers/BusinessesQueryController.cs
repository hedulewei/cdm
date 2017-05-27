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
using Ionic.Zip;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class BusinessesQueryController : ApiController
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
                _dbLog.Dispose();
                _dbuUserDbc.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("BusinessesPictureQuery")]
        [HttpPost]
        public ResultModel BusinessesPictureQuery([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
              //  Log.Info("BusinessesPictureQuery input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(_dbLog, param.userName, param.type.ToString(), JsonConvert.SerializeObject(param));
               
                var tempfile = Path.GetTempFileName()+".zip";
                switch (param.countyCode.ToLower())
                {
                    case "changdao": return ChangdaoPicture(param, tempfile);
                    case "zhaoyuan": return ZhaoyuanPicture(param, tempfile);
                    case "penglai": return PenglaiPicture(param, tempfile);
                    case "laizhou": return LaizhouPicture(param, tempfile);
                    case "laiyang": return LaiyangPicture(param, tempfile);

                    case "longkou": return LongkouPicture(param, tempfile);
                    case "muping": return MupingPicture(param, tempfile);
                    case "laishan": return LaishanPicture(param, tempfile);
                    case "qixia": return QixiaPicture(param, tempfile);
                    case "fushan": return FushanPicture(param, tempfile);

                    case "haiyang": return HaiyangPicture(param, tempfile);
                    case "zhifu":
                    case "shisuo":
                    case "dacheng":
                        return ZhifuPicture(param,tempfile);
                      
                    default:
                        return new ResultModel { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
                }
            
            }
            catch (Exception ex)
            {
                Log.Error("BusinessesPictureQuery", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
            
        }
        private ResultModel ChangdaoPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_CHANGDAO.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel ZhaoyuanPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_ZHAOYUAN.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel PenglaiPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_PENGLAI.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel LaizhouPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_LAIZHOU.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel LaiyangPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_LAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel LongkouPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_LONGKOU.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel MupingPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_MUPING.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel LaishanPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_LAISHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel QixiaPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_QIXIA.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel FushanPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_FUSHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel HaiyangPicture(BusinessModel param, string tempfile)
        {
            var busiforpic = cd.BUSINESS_HAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

            var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
            var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }
        private ResultModel ZhifuPicture(BusinessModel param,string tempfile)
        {
            var busiforpic = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
            if (busiforpic == null)
            {
                return BusinessNotFound();
            }

           var zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busiforpic.START_TIME.Year, busiforpic.START_TIME.Month, busiforpic.START_TIME.Day, busiforpic.ID);
            Log.InfoFormat("zipfilePath--{0}", zipfilePath);
            using (var newzip = new ZipFile(System.Text.Encoding.UTF8))
            {
                newzip.AddDirectory(zipfilePath);
                newzip.Save(tempfile);
            }
           var zipfileContent = File.ReadAllBytes(tempfile);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busiforpic.TYPE.ToString(CultureInfo.InvariantCulture)),
                    ID = (int)busiforpic.ID,
                    name = busiforpic.NAME,
                    IDum = busiforpic.ID_NUM,
                    queueNum = busiforpic.QUEUE_NUM,
                    address = busiforpic.ADDRESS,
                    phoneNum = busiforpic.PHONE_NUM,
                    attention = busiforpic.ATTENTION,
                    zipFile = zipfileContent
                }
            };

        }

        private ResultModel BusinessNotFound()
        {

            return new ResultModel
            {
                StatusCode = "000009",
                Result = "没有找到相关业务 ！"
            };
        }

        [Route("BusinessesQuery")]
        [HttpPost]
        public BusinessListResult BusinessesQuery([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new BusinessListResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("BusinessesQuery input:" + JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param,_dbuUserDbc))
                //{
                //    return new BusinessListResult { StatusCode = "000007", Result = "没有权限" };
                //}

                switch (param.countyCode.ToLower())
                {
                    case "changdao": return ChangdaoBusinessInfo(param);
                    case "zhaoyuan": return ZhaoyuanBusinessInfo(param);
                    case "penglai": return PenglaiBusinessInfo(param);
                    case "laizhou": return LaizhouBusinessInfo(param);
                    case "laiyang": return LaiyangBusinessInfo(param);

                    case "longkou": return LongkouBusinessInfo(param);
                    case "muping": return MupingBusinessInfo(param);
                    case "laishan": return LaishanBusinessInfo(param);
                    case "qixia": return QixiaBusinessInfo(param);
                    case "fushan": return FushanBusinessInfo(param);

                    case "haiyang": return HaiyangBusinessInfo(param);
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        return ZhifuBusinessInfo(param);
                        default:
                        return new BusinessListResult { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
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
                Log.Error("BusinessesQuery", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }
        }

        private BusinessListResult ChangdaoBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_CHANGDAO.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult ZhaoyuanBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_ZHAOYUAN.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult PenglaiBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_PENGLAI.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult LaizhouBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_LAIZHOU.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult LaiyangBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_LAIYANG.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult LongkouBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_LONGKOU.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult MupingBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_MUPING.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult LaishanBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_LAISHAN.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult QixiaBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_QIXIA.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult FushanBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_FUSHAN.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult HaiyangBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_HAIYANG.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
        private BusinessListResult ZhifuBusinessInfo( BusinessModel param)
        {
            var busi = cd.ZHIFUBUSINESS.Where(c => (param.countyCode == string.Empty || c.COUNTYCODE == param.countyCode)
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
            if (Enum.IsDefined(typeof(BusinessCategory),param.businessCategory))
            {
               // Log.InfoFormat(" IsDefined ok? {0}", "in ok");
                var ttt =param.businessCategory.ToString(CultureInfo.InvariantCulture);
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