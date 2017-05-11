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
        private readonly ModelAllDb _db = new ModelAllDb();
        private readonly UserDbc _dbuUserDbc = new UserDbc();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
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
                Log.Info("BusinessesPictureQuery input:" + JsonConvert.SerializeObject(param));
                if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                var zipfilePath = string.Empty;
                var zipfileContent = new byte[1];
                var tempfile = Path.GetTempFileName();
                switch (param.countyCode)
                {
                    case "changdao":
                        var busichangdao = _db.changdaobusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busichangdao == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busichangdao.START_TIME, busichangdao.ID);
                        //  Log.Info("Configuration fpath is:" + fpath);
                       
                        using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busichangdao.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busichangdao.ID,
                                name = busichangdao.NAME,
                                IDum = busichangdao.ID_NUM,
                                queueNum = busichangdao.QUEUE_NUM,
                                address = busichangdao.ADDRESS,
                                phoneNum = busichangdao.PHONE_NUM,
                                attention = busichangdao.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "zhaoyuan":
                        var busizhaoyuan = _db.zhaoyuanbusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busizhaoyuan == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busizhaoyuan.START_TIME, busizhaoyuan.ID);
                        using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busizhaoyuan.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busizhaoyuan.ID,
                                name = busizhaoyuan.NAME,
                                IDum = busizhaoyuan.ID_NUM,
                                queueNum = busizhaoyuan.QUEUE_NUM,
                                address = busizhaoyuan.ADDRESS,
                                phoneNum = busizhaoyuan.PHONE_NUM,
                                attention = busizhaoyuan.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "penglai":
                        var busipenglai = _db.penglaibusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busipenglai == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busipenglai.START_TIME, busipenglai.ID);
                         using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busipenglai.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busipenglai.ID,
                                name = busipenglai.NAME,
                                IDum = busipenglai.ID_NUM,
                                queueNum = busipenglai.QUEUE_NUM,
                                address = busipenglai.ADDRESS,
                                phoneNum = busipenglai.PHONE_NUM,
                                attention = busipenglai.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "laizhou":
                        var busilaizhou = _db.laizhoubusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busilaizhou == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaizhou.START_TIME, busilaizhou.ID);
                         using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busilaizhou.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busilaizhou.ID,
                                name = busilaizhou.NAME,
                                IDum = busilaizhou.ID_NUM,
                                queueNum = busilaizhou.QUEUE_NUM,
                                address = busilaizhou.ADDRESS,
                                phoneNum = busilaizhou.PHONE_NUM,
                                attention = busilaizhou.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "laiyang":
                        var busilaiyang = _db.laiyangbusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busilaiyang == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaiyang.START_TIME, busilaiyang.ID);
                         using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busilaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busilaiyang.ID,
                                name = busilaiyang.NAME,
                                IDum = busilaiyang.ID_NUM,
                                queueNum = busilaiyang.QUEUE_NUM,
                                address = busilaiyang.ADDRESS,
                                phoneNum = busilaiyang.PHONE_NUM,
                                attention = busilaiyang.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "longkou":
                        var busilongkou = _db.longkoubusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busilongkou == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilongkou.START_TIME, busilongkou.ID);
                          using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busilongkou.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busilongkou.ID,
                                name = busilongkou.NAME,
                                IDum = busilongkou.ID_NUM,
                                queueNum = busilongkou.QUEUE_NUM,
                                address = busilongkou.ADDRESS,
                                phoneNum = busilongkou.PHONE_NUM,
                                attention = busilongkou.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "muping":
                        var busimuping = _db.mupingbusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busimuping == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busimuping.START_TIME, busimuping.ID);
                        using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busimuping.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busimuping.ID,
                                name = busimuping.NAME,
                                IDum = busimuping.ID_NUM,
                                queueNum = busimuping.QUEUE_NUM,
                                address = busimuping.ADDRESS,
                                phoneNum = busimuping.PHONE_NUM,
                                attention = busimuping.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "laishan":
                        var busilaishan = _db.laishanbusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busilaishan == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaishan.START_TIME, busilaishan.ID);
                         using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busilaishan.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busilaishan.ID,
                                name = busilaishan.NAME,
                                IDum = busilaishan.ID_NUM,
                                queueNum = busilaishan.QUEUE_NUM,
                                address = busilaishan.ADDRESS,
                                phoneNum = busilaishan.PHONE_NUM,
                                attention = busilaishan.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "qixia":
                        var busiqixia = _db.qixiabusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busiqixia == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busiqixia.START_TIME, busiqixia.ID);
                          using (var newzip=new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busiqixia.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busiqixia.ID,
                                name = busiqixia.NAME,
                                IDum = busiqixia.ID_NUM,
                                queueNum = busiqixia.QUEUE_NUM,
                                address = busiqixia.ADDRESS,
                                phoneNum = busiqixia.PHONE_NUM,
                                attention = busiqixia.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "fushan":
                        var busifushan = _db.fushanbusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busifushan == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busifushan.START_TIME, busifushan.ID);
                        using (var newzip = new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busifushan.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busifushan.ID,
                                name = busifushan.NAME,
                                IDum = busifushan.ID_NUM,
                                queueNum = busifushan.QUEUE_NUM,
                                address = busifushan.ADDRESS,
                                phoneNum = busifushan.PHONE_NUM,
                                attention = busifushan.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "zhifu":
                        var busizhifu = _db.zhifubusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busizhifu == null)
                        {
                            return BusinessNotFound();
                        }

                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busizhifu.START_TIME, busizhifu.ID);
                        using (var newzip = new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                        return new ResultModel
                        {
                            StatusCode = "000000",
                            BussinessModel = new BusinessModel
                            {
                                type = int.Parse(busizhifu.TYPE.ToString(CultureInfo.InvariantCulture)),
                                ID = (int)busizhifu.ID,
                                name = busizhifu.NAME,
                                IDum = busizhifu.ID_NUM,
                                queueNum = busizhifu.QUEUE_NUM,
                                address = busizhifu.ADDRESS,
                                phoneNum = busizhifu.PHONE_NUM,
                                attention = busizhifu.ATTENTION,
                                zipFile = zipfileContent
                            }
                        };

                        break;
                    case "haiyang":
                        var busihaiyang = _db.haiyangbusiness.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busihaiyang == null)
                        {
                            return BusinessNotFound();
                        }


                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busihaiyang.START_TIME, busihaiyang.ID);
                         using (var newzip = new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
                            return new ResultModel
                            {
                                StatusCode = "000000",
                                BussinessModel = new BusinessModel
                                {
                                    type = int.Parse(busihaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                                    ID = (int)busihaiyang.ID,
                                    name = busihaiyang.NAME,
                                    IDum = busihaiyang.ID_NUM,
                                    queueNum = busihaiyang.QUEUE_NUM,
                                    address = busihaiyang.ADDRESS,
                                    phoneNum = busihaiyang.PHONE_NUM,
                                    attention = busihaiyang.ATTENTION,
                                    zipFile = zipfileContent
                                }
                            };
                        
                        break;
                    default:
                      //  if(db.Haiyangbusiness.Count(c=>c.SERIAL_NUM==param.serialNum)<1)
                          
                        var busi = _db.BUSSINESS.FirstOrDefault(c => c.SERIAL_NUM == param.serialNum);
                        if (busi == null)
                        return new ResultModel
                        {
                            StatusCode = "000009",
                            Result = "没有找到相关业务 ！"
                        };


                        zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busi.START_TIME, busi.ID);
                            using (var newzip = new ZipFile())
                        {
                            newzip.AddDirectory(zipfilePath);
                            newzip.Save(tempfile);
                        }
                        zipfileContent = File.ReadAllBytes(tempfile);
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
                                    zipFile = zipfileContent
                                }
                            };
                        
                        break;
                }
            
            }
            catch (Exception ex)
            {
                Log.Error("BusinessesQuery", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
            
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
                Log.Info("BusinessesQuery input:" + JsonConvert.SerializeObject(param));
                if (!PermissionCheck.CheckLevelPermission(param,_dbuUserDbc))
                {
                    return new BusinessListResult { StatusCode = "000007", Result = "没有权限" };
                }
                using (var cd = new ModelAllDb())
                {
                    switch (param.countyCode)
                    {
                        case "qixia":
                            return QixiaBusinessInfo(cd, param);
                        case "laishan":
                            return LaishanBusinessInfo(cd, param);
                        case "muping":
                            return MupingBusinessInfo(cd, param);
                        case "longkou":
                            return LongkouBusinessInfo(cd, param);
                        case "laiyang":
                            return LaiyangBusinessInfo(cd, param);

                        case "laizhou":
                            return LaizhouBusinessInfo(cd, param);
                        case "penglai":
                            return PenglaiBusinessInfo(cd, param);
                        case "zhaoyuan":
                            return ZhaoyuanBusinessInfo(cd, param);
                        case "changdao":
                            return ChangdaoBusinessInfo(cd, param);

                        case "zhifu":
                            return ZhifuBusinessInfo(cd, param);
                        case "haiyang":
                            return HaiyangBusinessInfo(cd, param);
                        case "fushan":
                            return FushanBusinessInfo(cd, param);
                        default:
                            return AllBusinessInfo(cd, param);
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

        private BusinessListResult HaiyangBusinessInfo(ModelAllDb cd, BusinessModel param)
        {

            IQueryable<haiyangbusiness> busi = cd.haiyangbusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.haiyangbusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

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
        private BusinessListResult ZhifuBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<zhifubusiness> busi = cd.zhifubusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.zhifubusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (zhifubusiness hy in busi)
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
        private BusinessListResult AllBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<BUSSINESS> busi = cd.BUSSINESS.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.BUSSINESS.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (BUSSINESS hy in busi)
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
        private BusinessListResult ChangdaoBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<changdaobusiness> busi = cd.changdaobusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.changdaobusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (changdaobusiness hy in busi)
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
        private BusinessListResult ZhaoyuanBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<zhaoyuanbusiness> busi = cd.zhaoyuanbusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.zhaoyuanbusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (zhaoyuanbusiness hy in busi)
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
        private BusinessListResult PenglaiBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<penglaibusiness> busi = cd.penglaibusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.penglaibusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (penglaibusiness hy in busi)
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
        private BusinessListResult LaizhouBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<laizhoubusiness> busi = cd.laizhoubusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.laizhoubusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (laizhoubusiness hy in busi)
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
        private BusinessListResult LaiyangBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<laiyangbusiness> busi = cd.laiyangbusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.laiyangbusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (laiyangbusiness hy in busi)
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
        private BusinessListResult LongkouBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<longkoubusiness> busi = cd.longkoubusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.longkoubusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (longkoubusiness hy in busi)
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
        private BusinessListResult MupingBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<mupingbusiness> busi = cd.mupingbusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.mupingbusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (mupingbusiness hy in busi)
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
        private BusinessListResult LaishanBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<laishanbusiness> busi = cd.laishanbusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.laishanbusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (laishanbusiness hy in busi)
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
        private BusinessListResult QixiaBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<qixiabusiness> busi = cd.qixiabusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.qixiabusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (qixiabusiness hy in busi)
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
        private BusinessListResult FushanBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            IQueryable<fushanbusiness> busi = cd.fushanbusiness.Where(c => c.COUNTYCODE == param.countyCode
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
                busi = cd.fushanbusiness.Where(c => c.TRANSFER_STATUS == aa);
            }
            busi = busi.Take(100);

            var ret = new BusinessListResult { StatusCode = "000000", Result = "ok" };
            foreach (fushanbusiness hy in busi)
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
     
    }
}