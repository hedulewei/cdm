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
    public class BusinessAcceptController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Model1525 cd = new Model1525();
        private readonly UserDbc _dbuUserDbc = new UserDbc();
         private static Dictionary<string, Dictionary<string, string>> queueLock =
            new Dictionary<string, Dictionary<string, string>>();

         static BusinessAcceptController()
        {
            queueLock.Add("haiyang", new Dictionary<string, string> { { "cars", "cars1" }, { "drivers", "drivers1" }, { "archives", "archives1" } });
            queueLock.Add("laizhou", new Dictionary<string, string> { { "cars", "cars2" }, { "drivers", "drivers2" }, { "archives", "archives2" } });
            queueLock.Add("zhifu", new Dictionary<string, string> { { "cars", "cars3" }, { "drivers", "drivers3" }, { "archives", "archives3" } });
            queueLock.Add("laishan", new Dictionary<string, string> { { "cars", "cars4" }, { "drivers", "drivers4" }, { "archives", "archives4" } });
            queueLock.Add("fushan", new Dictionary<string, string> { { "cars", "cars5" }, { "drivers", "drivers5" }, { "archives", "archives5" } });

            queueLock.Add("longkou", new Dictionary<string, string> { { "cars", "cars6" }, { "drivers", "drivers6" }, { "archives", "archives6" } });
            queueLock.Add("penglai", new Dictionary<string, string> { { "cars", "cars7" }, { "drivers", "drivers7" }, { "archives", "archives7" } });
            queueLock.Add("muping", new Dictionary<string, string> { { "cars", "cars8" }, { "drivers", "drivers8" }, { "archives", "archives8" } });
            queueLock.Add("zhaoyuan", new Dictionary<string, string> { { "cars", "cars9" }, { "drivers", "drivers9" }, { "archives", "archives9" } });
            queueLock.Add("qixia", new Dictionary<string, string> { { "cars", "cars10" }, { "drivers", "drivers10" }, { "archives", "archives10" } });

            queueLock.Add("laiyang", new Dictionary<string, string> { { "cars", "cars11" }, { "drivers", "drivers11" }, { "archives", "archives11" } });
            queueLock.Add("changdao", new Dictionary<string, string> { { "cars", "cars12" }, { "drivers", "drivers12" }, { "archives", "archives12" } });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cd.Dispose();
                _dbuUserDbc.Dispose();
            }
            base.Dispose(disposing);
        }
        private ResultModel BusinessFinishNotFound()
        {
            return new ResultModel
            {
                StatusCode = "000009",
                Result = "没有找到相关业务 ！"
            };
        }
        private ResultModel BusinessNotFound()
        {
            return new ResultModel
            {
                StatusCode = "000000",
                Result = "没有找到相关业务 ！"
            };
        }
        [Route("DestroyTask")]
        [HttpPost]
        public ResultModel DestroyTask([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("DestroyTask input:" + JsonConvert.SerializeObject(param));
                // LogIntoDb.Log(_dbLog, param.userName, param.type.ToString(), JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}


                switch (param.countyCode)
                {
                    //case "changdao":
                    //    var busichangdao = cd.changdaobusiness.FirstOrDefault(c => c.STATUS==param.status);
                    //    if (busichangdao == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }


                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busichangdao.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busichangdao.ID,
                    //            name = busichangdao.NAME,
                    //            IDum = busichangdao.ID_NUM,
                    //            serialNum = busichangdao.SERIAL_NUM,
                    //            queueNum = busichangdao.QUEUE_NUM,
                    //            address = busichangdao.ADDRESS,
                    //            phoneNum = busichangdao.PHONE_NUM,
                    //            attention = busichangdao.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "zhaoyuan":
                    //    var busizhaoyuan = cd.zhaoyuanbusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busizhaoyuan == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busizhaoyuan.START_TIME, busizhaoyuan.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busizhaoyuan.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busizhaoyuan.ID,
                    //            name = busizhaoyuan.NAME,
                    //            IDum = busizhaoyuan.ID_NUM,
                    //            queueNum = busizhaoyuan.QUEUE_NUM,
                    //            address = busizhaoyuan.ADDRESS,
                    //            phoneNum = busizhaoyuan.PHONE_NUM,
                    //            attention = busizhaoyuan.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "penglai":
                    //    var busipenglai = cd.penglaibusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busipenglai == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busipenglai.START_TIME, busipenglai.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busipenglai.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busipenglai.ID,
                    //            name = busipenglai.NAME,
                    //            IDum = busipenglai.ID_NUM,
                    //            queueNum = busipenglai.QUEUE_NUM,
                    //            address = busipenglai.ADDRESS,
                    //            phoneNum = busipenglai.PHONE_NUM,
                    //            attention = busipenglai.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "laizhou":
                    //    var busilaizhou = cd.laizhoubusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busilaizhou == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaizhou.START_TIME, busilaizhou.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busilaizhou.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busilaizhou.ID,
                    //            name = busilaizhou.NAME,
                    //            IDum = busilaizhou.ID_NUM,
                    //            queueNum = busilaizhou.QUEUE_NUM,
                    //            address = busilaizhou.ADDRESS,
                    //            phoneNum = busilaizhou.PHONE_NUM,
                    //            attention = busilaizhou.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "laiyang":
                    //    var busilaiyang = cd.laiyangbusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busilaiyang == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaiyang.START_TIME, busilaiyang.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busilaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busilaiyang.ID,
                    //            name = busilaiyang.NAME,
                    //            IDum = busilaiyang.ID_NUM,
                    //            queueNum = busilaiyang.QUEUE_NUM,
                    //            address = busilaiyang.ADDRESS,
                    //            phoneNum = busilaiyang.PHONE_NUM,
                    //            attention = busilaiyang.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "longkou":
                    //    var busilongkou = cd.longkoubusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busilongkou == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilongkou.START_TIME, busilongkou.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busilongkou.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busilongkou.ID,
                    //            name = busilongkou.NAME,
                    //            IDum = busilongkou.ID_NUM,
                    //            queueNum = busilongkou.QUEUE_NUM,
                    //            address = busilongkou.ADDRESS,
                    //            phoneNum = busilongkou.PHONE_NUM,
                    //            attention = busilongkou.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "muping":
                    //    var busimuping = cd.mupingbusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busimuping == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busimuping.START_TIME, busimuping.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busimuping.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busimuping.ID,
                    //            name = busimuping.NAME,
                    //            IDum = busimuping.ID_NUM,
                    //            queueNum = busimuping.QUEUE_NUM,
                    //            address = busimuping.ADDRESS,
                    //            phoneNum = busimuping.PHONE_NUM,
                    //            attention = busimuping.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "laishan":
                    //    var busilaishan = cd.laishanbusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busilaishan == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaishan.START_TIME, busilaishan.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busilaishan.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busilaishan.ID,
                    //            name = busilaishan.NAME,
                    //            IDum = busilaishan.ID_NUM,
                    //            queueNum = busilaishan.QUEUE_NUM,
                    //            address = busilaishan.ADDRESS,
                    //            phoneNum = busilaishan.PHONE_NUM,
                    //            attention = busilaishan.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "qixia":
                    //    var busiqixia = cd.qixiabusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busiqixia == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busiqixia.START_TIME, busiqixia.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busiqixia.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busiqixia.ID,
                    //            name = busiqixia.NAME,
                    //            IDum = busiqixia.ID_NUM,
                    //            queueNum = busiqixia.QUEUE_NUM,
                    //            address = busiqixia.ADDRESS,
                    //            phoneNum = busiqixia.PHONE_NUM,
                    //            attention = busiqixia.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    //case "fushan":
                    //    var busifushan = cd.fushanbusiness.FirstOrDefault(c => c.ID == param.ID);
                    //    if (busifushan == null)
                    //    {
                    //        return BusinessNotFound();
                    //    }

                    //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busifushan.START_TIME, busifushan.ID);
                    //    using (var newzip = new ZipFile())
                    //    {
                    //        newzip.AddDirectory(zipfilePath);
                    //        newzip.Save(tempfile);
                    //    }
                    //    zipfileContent = File.ReadAllBytes(tempfile);
                    //    return new ResultModel
                    //    {
                    //        StatusCode = "000000",
                    //        BussinessModel = new BusinessModel
                    //        {
                    //            type = int.Parse(busifushan.TYPE.ToString(CultureInfo.InvariantCulture)),
                    //            ID = (int)busifushan.ID,
                    //            name = busifushan.NAME,
                    //            IDum = busifushan.ID_NUM,
                    //            queueNum = busifushan.QUEUE_NUM,
                    //            address = busifushan.ADDRESS,
                    //            phoneNum = busifushan.PHONE_NUM,
                    //            attention = busifushan.ATTENTION,
                    //            zipFile = zipfileContent
                    //        }
                    //    };

                    //    break;
                    case "zhifu":
                    default:
                        var busizhifu = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
                        if (busizhifu == null)
                        {
                            return BusinessFinishNotFound();
                        }
                        busizhifu.STATUS = 6;
                        cd.SaveChanges();
                        return new ResultModel
                        {
                            StatusCode = "000000",
                        };

                        break;
                        //case "haiyang":
                        //    var busihaiyang = cd.haiyangbusiness.FirstOrDefault(c => c.ID == param.ID);
                        //    if (busihaiyang == null)
                        //    {
                        //        return BusinessNotFound();
                        //    }
                        //    zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busihaiyang.START_TIME.Year, busihaiyang.START_TIME.Month, busihaiyang.START_TIME.Day, busihaiyang.ID);
                        //    using (var newzip = new ZipFile())
                        //    {
                        //        newzip.AddDirectory(zipfilePath);
                        //        newzip.Save(tempfile);
                        //    }
                        //    zipfileContent = File.ReadAllBytes(tempfile);
                        //    return new ResultModel
                        //    {
                        //        StatusCode = "000000",
                        //        BussinessModel = new BusinessModel
                        //        {
                        //            type = int.Parse(busihaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                        //            ID = (int)busihaiyang.ID,
                        //            name = busihaiyang.NAME,
                        //            IDum = busihaiyang.ID_NUM,
                        //            queueNum = busihaiyang.QUEUE_NUM,
                        //            address = busihaiyang.ADDRESS,
                        //            phoneNum = busihaiyang.PHONE_NUM,
                        //            attention = busihaiyang.ATTENTION,
                        //            zipFile = zipfileContent
                        //        }
                        //    };

                        //    break;
                        // default:
                        //  if(db.Haiyangbusiness.Count(c=>c.SERIAL_NUM==param.serialNum)<1)

                        //var busi = cd.BUSSINESS.FirstOrDefault(c => c.ID == param.ID);
                        //if (busi == null)
                        //    return new ResultModel
                        //    {
                        //        StatusCode = "000009",
                        //        Result = "没有找到相关业务 ！"
                        //    };

                        //zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busi.START_TIME.Year, busi.START_TIME.Month, busi.START_TIME.Day, busi.ID);
                        //using (var newzip = new ZipFile())
                        //{
                        //    newzip.AddDirectory(zipfilePath);
                        //    newzip.Save(tempfile);
                        //}
                        //zipfileContent = File.ReadAllBytes(tempfile);
                        //return new ResultModel
                        //{
                        //    StatusCode = "000000",
                        //    BussinessModel = new BusinessModel
                        //    {
                        //        type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                        //        ID = (int)busi.ID,
                        //        name = busi.NAME,
                        //        IDum = busi.ID_NUM,
                        //        queueNum = busi.QUEUE_NUM,
                        //        address = busi.ADDRESS,
                        //        phoneNum = busi.PHONE_NUM,
                        //        attention = busi.ATTENTION,
                        //        zipFile = zipfileContent
                        //    }
                        //};

                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("DestroyTask", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("UpdateTask")]
        [HttpPost]
        public ResultModel UpdateTask([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("UpdateTask input:" + JsonConvert.SerializeObject(param));
               // LogIntoDb.Log(_dbLog, param.userName, param.type.ToString(), JsonConvert.SerializeObject(param));
                if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }


                switch (param.countyCode.ToLower())
                {
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                    default:
                        var busizhifu = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
                        if (busizhifu == null)
                        {
                            return BusinessFinishNotFound();
                        }
                        busizhifu.STATUS = 1;
                        busizhifu.REJECT_REASON = string.Empty;
                        busizhifu.START_TIME = DateTime.Now;
                        busizhifu.END_TIME = DateTime.Now;
                        busizhifu.QUEUE_NUM = param.queueNum;

                        busizhifu.ID_NUM = param.IDum;
                        busizhifu.ADDRESS = param.address;
                        busizhifu.SERIAL_NUM = param.serialNum;
                        busizhifu.NAME = param.name;
                         busizhifu.PHONE_NUM = param.phoneNum;

                        busizhifu.PROCESS_USER = param.userName;
                         busizhifu.FILE_RECV_USER = param.fileRecvUser;
                      //  busizhifu.TRANSFER_STATUS = param.transferStatus;
                      //   busizhifu.UPLOADER = param.uploader;
                        busizhifu.COMPLETE_PAY_USER = param.completePayUser;

                         busizhifu.ATTENTION = param.attention;
                        busizhifu.UNLOAD_TASK_NUM = param.unloadTaskNum;
                        busizhifu.POSTPHONE = param.postPhone;
                        busizhifu.POSTADDR = param.postAddr;
                         busizhifu.CHECK_FILE = param.checkFile;

                        busizhifu.CAR_NUM = param.carNum;
                         busizhifu.TAX_NUM = param.texNum;
                        busizhifu.TAX_TYPE = param.texType;
                         busizhifu.ORIGIN_NUM = param.originNum;
                        busizhifu.ORIGIN_TYPE = param.originType;
                        cd.SaveChanges();
                        return new ResultModel
                        {
                            StatusCode = "000000",
                        };

                        break;
                        //case "haiyang":
                        //    var busihaiyang = cd.haiyangbusiness.FirstOrDefault(c => c.ID == param.ID);
                        //    if (busihaiyang == null)
                        //    {
                        //        return BusinessNotFound();
                        //    }
                        //    zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busihaiyang.START_TIME.Year, busihaiyang.START_TIME.Month, busihaiyang.START_TIME.Day, busihaiyang.ID);
                        //    using (var newzip = new ZipFile())
                        //    {
                        //        newzip.AddDirectory(zipfilePath);
                        //        newzip.Save(tempfile);
                        //    }
                        //    zipfileContent = File.ReadAllBytes(tempfile);
                        //    return new ResultModel
                        //    {
                        //        StatusCode = "000000",
                        //        BussinessModel = new BusinessModel
                        //        {
                        //            type = int.Parse(busihaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                        //            ID = (int)busihaiyang.ID,
                        //            name = busihaiyang.NAME,
                        //            IDum = busihaiyang.ID_NUM,
                        //            queueNum = busihaiyang.QUEUE_NUM,
                        //            address = busihaiyang.ADDRESS,
                        //            phoneNum = busihaiyang.PHONE_NUM,
                        //            attention = busihaiyang.ATTENTION,
                        //            zipFile = zipfileContent
                        //        }
                        //    };

                        //    break;
                        // default:
                        //  if(db.Haiyangbusiness.Count(c=>c.SERIAL_NUM==param.serialNum)<1)

                        //var busi = cd.BUSSINESS.FirstOrDefault(c => c.ID == param.ID);
                        //if (busi == null)
                        //    return new ResultModel
                        //    {
                        //        StatusCode = "000009",
                        //        Result = "没有找到相关业务 ！"
                        //    };

                        //zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busi.START_TIME.Year, busi.START_TIME.Month, busi.START_TIME.Day, busi.ID);
                        //using (var newzip = new ZipFile())
                        //{
                        //    newzip.AddDirectory(zipfilePath);
                        //    newzip.Save(tempfile);
                        //}
                        //zipfileContent = File.ReadAllBytes(tempfile);
                        //return new ResultModel
                        //{
                        //    StatusCode = "000000",
                        //    BussinessModel = new BusinessModel
                        //    {
                        //        type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                        //        ID = (int)busi.ID,
                        //        name = busi.NAME,
                        //        IDum = busi.ID_NUM,
                        //        queueNum = busi.QUEUE_NUM,
                        //        address = busi.ADDRESS,
                        //        phoneNum = busi.PHONE_NUM,
                        //        attention = busi.ATTENTION,
                        //        zipFile = zipfileContent
                        //    }
                        //};

                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("BusinessesPictureQuery", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("RejectTaskList")]
        [HttpPost]
        public BusinessListResult RejectTaskList([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new BusinessListResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                  Log.Info("RejectTaskList input:" + JsonConvert.SerializeObject(param));
                // LogIntoDb.Log(_dbLog, param.userName, param.type.ToString(), JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new BusinessListResult { StatusCode = "000007", Result = "没有权限" };
                //}


                switch (param.countyCode.ToLower())
                {
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                    default:
                        var busizhifu = cd.ZHIFUBUSINESS.Where(c => c.STATUS == 4);

                        var blist = new List<BusinessModel>();
                        foreach (ZHIFUBUSINESS rejectTask in busizhifu)
                        {
                            blist.Add(new BusinessModel
                            {
                                ID=(int)rejectTask.ID,
                                type = (int)rejectTask.TYPE,
                                startTime = rejectTask.START_TIME.ToString(),
                                endTime = rejectTask.END_TIME.ToString(),
                                status = (int)rejectTask.STATUS,

                                queueNum = rejectTask.QUEUE_NUM,
                                IDum = rejectTask.ID_NUM,
                                address = rejectTask.ADDRESS,
                                serialNum = rejectTask.SERIAL_NUM,
                                rejectReason = rejectTask.REJECT_REASON,

                                name = rejectTask.NAME,
                                phoneNum = rejectTask.PHONE_NUM,
                                processUser = rejectTask.PROCESS_USER,
                                fileRecvUser = rejectTask.FILE_RECV_USER,
                             //   transferStatus = rejectTask.TRANSFER_STATUS,

                                uploader = rejectTask.UPLOADER,
                                completePayUser = rejectTask.COMPLETE_PAY_USER,
                                attention = rejectTask.ATTENTION,
                                unloadTaskNum = rejectTask.UNLOAD_TASK_NUM,
                                postPhone = rejectTask.POSTPHONE,
                                countyCode = rejectTask.COUNTYCODE,
                                postAddr = rejectTask.POSTADDR,
                             //   checkFile = rejectTask.CHECK_FILE,
                                carNum = rejectTask.CAR_NUM,
                                texNum = rejectTask.TAX_NUM,
                                texType = rejectTask.TAX_TYPE,

                                originNum = rejectTask.ORIGIN_NUM,
                                originType = rejectTask.ORIGIN_TYPE,
                            });
                        }

                        return new BusinessListResult
                        {
                            StatusCode = "000000",
                            BussinessList=blist,
                        };

                        break;
                        //case "haiyang":
                        //    var busihaiyang = cd.haiyangbusiness.FirstOrDefault(c => c.ID == param.ID);
                        //    if (busihaiyang == null)
                        //    {
                        //        return BusinessNotFound();
                        //    }
                        //    zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busihaiyang.START_TIME.Year, busihaiyang.START_TIME.Month, busihaiyang.START_TIME.Day, busihaiyang.ID);
                        //    using (var newzip = new ZipFile())
                        //    {
                        //        newzip.AddDirectory(zipfilePath);
                        //        newzip.Save(tempfile);
                        //    }
                        //    zipfileContent = File.ReadAllBytes(tempfile);
                        //    return new ResultModel
                        //    {
                        //        StatusCode = "000000",
                        //        BussinessModel = new BusinessModel
                        //        {
                        //            type = int.Parse(busihaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                        //            ID = (int)busihaiyang.ID,
                        //            name = busihaiyang.NAME,
                        //            IDum = busihaiyang.ID_NUM,
                        //            queueNum = busihaiyang.QUEUE_NUM,
                        //            address = busihaiyang.ADDRESS,
                        //            phoneNum = busihaiyang.PHONE_NUM,
                        //            attention = busihaiyang.ATTENTION,
                        //            zipFile = zipfileContent
                        //        }
                        //    };

                        //    break;
                        // default:
                        //  if(db.Haiyangbusiness.Count(c=>c.SERIAL_NUM==param.serialNum)<1)

                        //var busi = cd.BUSSINESS.FirstOrDefault(c => c.ID == param.ID);
                        //if (busi == null)
                        //    return new ResultModel
                        //    {
                        //        StatusCode = "000009",
                        //        Result = "没有找到相关业务 ！"
                        //    };

                        //zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busi.START_TIME.Year, busi.START_TIME.Month, busi.START_TIME.Day, busi.ID);
                        //using (var newzip = new ZipFile())
                        //{
                        //    newzip.AddDirectory(zipfilePath);
                        //    newzip.Save(tempfile);
                        //}
                        //zipfileContent = File.ReadAllBytes(tempfile);
                        //return new ResultModel
                        //{
                        //    StatusCode = "000000",
                        //    BussinessModel = new BusinessModel
                        //    {
                        //        type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                        //        ID = (int)busi.ID,
                        //        name = busi.NAME,
                        //        IDum = busi.ID_NUM,
                        //        queueNum = busi.QUEUE_NUM,
                        //        address = busi.ADDRESS,
                        //        phoneNum = busi.PHONE_NUM,
                        //        attention = busi.ATTENTION,
                        //        zipFile = zipfileContent
                        //    }
                        //};

                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("BusinessesPictureQuery", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("RejectTask")]
        [HttpPost]
        public ResultModel RejectTask([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("RejectTask input:" + JsonConvert.SerializeObject(param));
                // LogIntoDb.Log(_dbLog, param.userName, param.type.ToString(), JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}


                switch (param.countyCode.ToLower())
                {
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                    default:
                        var busizhifu = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
                        if (busizhifu == null)
                        {
                            return BusinessFinishNotFound();
                        }
                        busizhifu.STATUS = 4;
                        busizhifu.REJECT_REASON = param.rejectReason;
                       
                        cd.SaveChanges();
                        return new ResultModel
                        {
                            StatusCode = "000000",
                        };

                        break;
                        //case "haiyang":
                        //    var busihaiyang = cd.haiyangbusiness.FirstOrDefault(c => c.ID == param.ID);
                        //    if (busihaiyang == null)
                        //    {
                        //        return BusinessNotFound();
                        //    }
                        //    zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busihaiyang.START_TIME.Year, busihaiyang.START_TIME.Month, busihaiyang.START_TIME.Day, busihaiyang.ID);
                        //    using (var newzip = new ZipFile())
                        //    {
                        //        newzip.AddDirectory(zipfilePath);
                        //        newzip.Save(tempfile);
                        //    }
                        //    zipfileContent = File.ReadAllBytes(tempfile);
                        //    return new ResultModel
                        //    {
                        //        StatusCode = "000000",
                        //        BussinessModel = new BusinessModel
                        //        {
                        //            type = int.Parse(busihaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                        //            ID = (int)busihaiyang.ID,
                        //            name = busihaiyang.NAME,
                        //            IDum = busihaiyang.ID_NUM,
                        //            queueNum = busihaiyang.QUEUE_NUM,
                        //            address = busihaiyang.ADDRESS,
                        //            phoneNum = busihaiyang.PHONE_NUM,
                        //            attention = busihaiyang.ATTENTION,
                        //            zipFile = zipfileContent
                        //        }
                        //    };

                        //    break;
                        // default:
                        //  if(db.Haiyangbusiness.Count(c=>c.SERIAL_NUM==param.serialNum)<1)

                        //var busi = cd.BUSSINESS.FirstOrDefault(c => c.ID == param.ID);
                        //if (busi == null)
                        //    return new ResultModel
                        //    {
                        //        StatusCode = "000009",
                        //        Result = "没有找到相关业务 ！"
                        //    };

                        //zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busi.START_TIME.Year, busi.START_TIME.Month, busi.START_TIME.Day, busi.ID);
                        //using (var newzip = new ZipFile())
                        //{
                        //    newzip.AddDirectory(zipfilePath);
                        //    newzip.Save(tempfile);
                        //}
                        //zipfileContent = File.ReadAllBytes(tempfile);
                        //return new ResultModel
                        //{
                        //    StatusCode = "000000",
                        //    BussinessModel = new BusinessModel
                        //    {
                        //        type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                        //        ID = (int)busi.ID,
                        //        name = busi.NAME,
                        //        IDum = busi.ID_NUM,
                        //        queueNum = busi.QUEUE_NUM,
                        //        address = busi.ADDRESS,
                        //        phoneNum = busi.PHONE_NUM,
                        //        attention = busi.ATTENTION,
                        //        zipFile = zipfileContent
                        //    }
                        //};

                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("BusinessesPictureQuery", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("FinishTask")]
        [HttpPost]
        public ResultModel FinishTask([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("FinishTask input:" + JsonConvert.SerializeObject(param));
                // LogIntoDb.Log(_dbLog, param.userName, param.type.ToString(), JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}
               

                    switch (param.countyCode.ToLower())
                    {
                        case "shisuo":
                        case "dacheng":
                        case "zhifu":
                        default:
                            var busizhifu = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
                            if (busizhifu == null)
                            {
                                return BusinessFinishNotFound();
                            }
                            busizhifu.STATUS = 5;
                            busizhifu.SERIAL_NUM = param.serialNum;
                            busizhifu.PROCESS_USER = param.userName;
                            cd.SaveChanges();
                            return new ResultModel
                            {
                                StatusCode = "000000",
                            };

                            break;
                            //case "haiyang":
                            //    var busihaiyang = cd.haiyangbusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busihaiyang == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }
                            //    zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busihaiyang.START_TIME.Year, busihaiyang.START_TIME.Month, busihaiyang.START_TIME.Day, busihaiyang.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busihaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busihaiyang.ID,
                            //            name = busihaiyang.NAME,
                            //            IDum = busihaiyang.ID_NUM,
                            //            queueNum = busihaiyang.QUEUE_NUM,
                            //            address = busihaiyang.ADDRESS,
                            //            phoneNum = busihaiyang.PHONE_NUM,
                            //            attention = busihaiyang.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            // default:
                            //  if(db.Haiyangbusiness.Count(c=>c.SERIAL_NUM==param.serialNum)<1)

                            //var busi = cd.BUSSINESS.FirstOrDefault(c => c.ID == param.ID);
                            //if (busi == null)
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000009",
                            //        Result = "没有找到相关业务 ！"
                            //    };

                            //zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busi.START_TIME.Year, busi.START_TIME.Month, busi.START_TIME.Day, busi.ID);
                            //using (var newzip = new ZipFile())
                            //{
                            //    newzip.AddDirectory(zipfilePath);
                            //    newzip.Save(tempfile);
                            //}
                            //zipfileContent = File.ReadAllBytes(tempfile);
                            //return new ResultModel
                            //{
                            //    StatusCode = "000000",
                            //    BussinessModel = new BusinessModel
                            //    {
                            //        type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //        ID = (int)busi.ID,
                            //        name = busi.NAME,
                            //        IDum = busi.ID_NUM,
                            //        queueNum = busi.QUEUE_NUM,
                            //        address = busi.ADDRESS,
                            //        phoneNum = busi.PHONE_NUM,
                            //        attention = busi.ATTENTION,
                            //        zipFile = zipfileContent
                            //    }
                            //};

                            break;
                    }
                
            }
            catch (Exception ex)
            {
                Log.Error("BusinessesPictureQuery", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }

        [Route("AcquireTask")]
        [HttpPost]
        public ResultModel AcquireTask([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("AcquireTask input:" + JsonConvert.SerializeObject(param));
               // LogIntoDb.Log(_dbLog, param.userName, param.type.ToString(), JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}
                //var zipfilePath = string.Empty;
                //var zipfileContent = new byte[1];
                //var tempfile = Path.GetTempFileName() + ".zip";
                 var lockvalue= queueLock[param.countyCode];
              //  Log.Error("GetOrdinal lockvalue:" + lockvalue);
                lock (lockvalue)
                {

                    switch (param.countyCode.ToLower())
                    {
                            //case "changdao":
                            //    var busichangdao = cd.changdaobusiness.FirstOrDefault(c => c.STATUS==param.status);
                            //    if (busichangdao == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }


                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busichangdao.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busichangdao.ID,
                            //            name = busichangdao.NAME,
                            //            IDum = busichangdao.ID_NUM,
                            //            serialNum = busichangdao.SERIAL_NUM,
                            //            queueNum = busichangdao.QUEUE_NUM,
                            //            address = busichangdao.ADDRESS,
                            //            phoneNum = busichangdao.PHONE_NUM,
                            //            attention = busichangdao.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "zhaoyuan":
                            //    var busizhaoyuan = cd.zhaoyuanbusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busizhaoyuan == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busizhaoyuan.START_TIME, busizhaoyuan.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busizhaoyuan.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busizhaoyuan.ID,
                            //            name = busizhaoyuan.NAME,
                            //            IDum = busizhaoyuan.ID_NUM,
                            //            queueNum = busizhaoyuan.QUEUE_NUM,
                            //            address = busizhaoyuan.ADDRESS,
                            //            phoneNum = busizhaoyuan.PHONE_NUM,
                            //            attention = busizhaoyuan.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "penglai":
                            //    var busipenglai = cd.penglaibusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busipenglai == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busipenglai.START_TIME, busipenglai.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busipenglai.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busipenglai.ID,
                            //            name = busipenglai.NAME,
                            //            IDum = busipenglai.ID_NUM,
                            //            queueNum = busipenglai.QUEUE_NUM,
                            //            address = busipenglai.ADDRESS,
                            //            phoneNum = busipenglai.PHONE_NUM,
                            //            attention = busipenglai.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "laizhou":
                            //    var busilaizhou = cd.laizhoubusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busilaizhou == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaizhou.START_TIME, busilaizhou.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busilaizhou.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busilaizhou.ID,
                            //            name = busilaizhou.NAME,
                            //            IDum = busilaizhou.ID_NUM,
                            //            queueNum = busilaizhou.QUEUE_NUM,
                            //            address = busilaizhou.ADDRESS,
                            //            phoneNum = busilaizhou.PHONE_NUM,
                            //            attention = busilaizhou.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "laiyang":
                            //    var busilaiyang = cd.laiyangbusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busilaiyang == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaiyang.START_TIME, busilaiyang.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busilaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busilaiyang.ID,
                            //            name = busilaiyang.NAME,
                            //            IDum = busilaiyang.ID_NUM,
                            //            queueNum = busilaiyang.QUEUE_NUM,
                            //            address = busilaiyang.ADDRESS,
                            //            phoneNum = busilaiyang.PHONE_NUM,
                            //            attention = busilaiyang.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "longkou":
                            //    var busilongkou = cd.longkoubusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busilongkou == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilongkou.START_TIME, busilongkou.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busilongkou.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busilongkou.ID,
                            //            name = busilongkou.NAME,
                            //            IDum = busilongkou.ID_NUM,
                            //            queueNum = busilongkou.QUEUE_NUM,
                            //            address = busilongkou.ADDRESS,
                            //            phoneNum = busilongkou.PHONE_NUM,
                            //            attention = busilongkou.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "muping":
                            //    var busimuping = cd.mupingbusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busimuping == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busimuping.START_TIME, busimuping.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busimuping.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busimuping.ID,
                            //            name = busimuping.NAME,
                            //            IDum = busimuping.ID_NUM,
                            //            queueNum = busimuping.QUEUE_NUM,
                            //            address = busimuping.ADDRESS,
                            //            phoneNum = busimuping.PHONE_NUM,
                            //            attention = busimuping.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "laishan":
                            //    var busilaishan = cd.laishanbusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busilaishan == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busilaishan.START_TIME, busilaishan.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busilaishan.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busilaishan.ID,
                            //            name = busilaishan.NAME,
                            //            IDum = busilaishan.ID_NUM,
                            //            queueNum = busilaishan.QUEUE_NUM,
                            //            address = busilaishan.ADDRESS,
                            //            phoneNum = busilaishan.PHONE_NUM,
                            //            attention = busilaishan.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "qixia":
                            //    var busiqixia = cd.qixiabusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busiqixia == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busiqixia.START_TIME, busiqixia.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busiqixia.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busiqixia.ID,
                            //            name = busiqixia.NAME,
                            //            IDum = busiqixia.ID_NUM,
                            //            queueNum = busiqixia.QUEUE_NUM,
                            //            address = busiqixia.ADDRESS,
                            //            phoneNum = busiqixia.PHONE_NUM,
                            //            attention = busiqixia.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                            //case "fushan":
                            //    var busifushan = cd.fushanbusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busifushan == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }

                            //    zipfilePath = string.Format("{0}{1}\\{2}\\{3}", CdmConfiguration.FileRootPath, param.countyCode, busifushan.START_TIME, busifushan.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busifushan.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busifushan.ID,
                            //            name = busifushan.NAME,
                            //            IDum = busifushan.ID_NUM,
                            //            queueNum = busifushan.QUEUE_NUM,
                            //            address = busifushan.ADDRESS,
                            //            phoneNum = busifushan.PHONE_NUM,
                            //            attention = busifushan.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                        case "shisuo":
                        case "dacheng":
                        case "zhifu":
                        default:
                            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
                            var currentTop100 = cd.ZHIFUBUSINESS.Where(c=> c.STATUS == 1&&c.QUEUE_NUM!=string.Empty).OrderBy(c=>c.ID).Take(100);
                            foreach (ZHIFUBUSINESS busizhifu in currentTop100)
                            {
                                if (pts.Contains((int)busizhifu.TYPE)&&busizhifu.START_TIME.CompareTo(DateTime.Now.Date)>=0)
                                {
                                    busizhifu.STATUS = 3;
                                    busizhifu.PROCESS_USER = param.userName;
                                    cd.SaveChanges();
                                    return new ResultModel
                                    {
                                        StatusCode = "000000",
                                        BussinessModel = new BusinessModel
                                        {
                                            type = int.Parse(busizhifu.TYPE.ToString(CultureInfo.InvariantCulture)),
                                            countyCode = busizhifu.COUNTYCODE,
                                            ID = (int)busizhifu.ID,
                                            name = busizhifu.NAME,
                                            IDum = busizhifu.ID_NUM,
                                            serialNum = busizhifu.SERIAL_NUM,

                                            queueNum = busizhifu.QUEUE_NUM,
                                            address = busizhifu.ADDRESS,
                                            phoneNum = busizhifu.PHONE_NUM,
                                            carNum = busizhifu.CAR_NUM,
                                            texType = busizhifu.TAX_TYPE,

                                            texNum = busizhifu.TAX_NUM,
                                            originType = busizhifu.ORIGIN_TYPE,
                                            originNum = busizhifu.ORIGIN_NUM,
                                            attention = busizhifu.ATTENTION,

                                            postAddr = busizhifu.POSTADDR,
                                            postPhone = busizhifu.POSTPHONE,
                                        }
                                    };
                                }
                            }
                            //foreach (int pt in pts)
                            //{
                            //    var what = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.STATUS == 1&&c.TYPE==pt);
                            //    if (what != null)
                            //    {
                            //        var busizhifu = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.STATUS == 1);
                            //        busizhifu.STATUS = 3;
                            //        busizhifu.PROCESS_USER = param.userName;
                            //        cd.SaveChanges();
                            //        return new ResultModel
                            //        {
                            //            StatusCode = "000000",
                            //            BussinessModel = new BusinessModel
                            //            {
                            //                type = int.Parse(busizhifu.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //                ID = (int)busizhifu.ID,
                            //                name = busizhifu.NAME,
                            //                IDum = busizhifu.ID_NUM,
                            //                serialNum = busizhifu.SERIAL_NUM,
                            //                queueNum = busizhifu.QUEUE_NUM,
                            //                address = busizhifu.ADDRESS,
                            //                phoneNum = busizhifu.PHONE_NUM,
                            //                carNum = busizhifu.CAR_NUM,
                            //                texType = busizhifu.TAX_TYPE,
                            //                texNum = busizhifu.TAX_NUM,
                            //                originType = busizhifu.ORIGIN_TYPE,
                            //                originNum = busizhifu.ORIGIN_NUM,
                            //                attention = busizhifu.ATTENTION,
                            //            }
                            //        };
                            //       break;
                            //    }
                            //}
                            return BusinessNotFound();

                            break;
                            //case "haiyang":
                            //    var busihaiyang = cd.haiyangbusiness.FirstOrDefault(c => c.ID == param.ID);
                            //    if (busihaiyang == null)
                            //    {
                            //        return BusinessNotFound();
                            //    }
                            //    zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busihaiyang.START_TIME.Year, busihaiyang.START_TIME.Month, busihaiyang.START_TIME.Day, busihaiyang.ID);
                            //    using (var newzip = new ZipFile())
                            //    {
                            //        newzip.AddDirectory(zipfilePath);
                            //        newzip.Save(tempfile);
                            //    }
                            //    zipfileContent = File.ReadAllBytes(tempfile);
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000000",
                            //        BussinessModel = new BusinessModel
                            //        {
                            //            type = int.Parse(busihaiyang.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //            ID = (int)busihaiyang.ID,
                            //            name = busihaiyang.NAME,
                            //            IDum = busihaiyang.ID_NUM,
                            //            queueNum = busihaiyang.QUEUE_NUM,
                            //            address = busihaiyang.ADDRESS,
                            //            phoneNum = busihaiyang.PHONE_NUM,
                            //            attention = busihaiyang.ATTENTION,
                            //            zipFile = zipfileContent
                            //        }
                            //    };

                            //    break;
                       // default:
                            //  if(db.Haiyangbusiness.Count(c=>c.SERIAL_NUM==param.serialNum)<1)

                            //var busi = cd.BUSSINESS.FirstOrDefault(c => c.ID == param.ID);
                            //if (busi == null)
                            //    return new ResultModel
                            //    {
                            //        StatusCode = "000009",
                            //        Result = "没有找到相关业务 ！"
                            //    };

                            //zipfilePath = string.Format("{0}{1}\\{2}-{3}-{4}\\{5}", CdmConfiguration.FileRootPath, param.countyCode, busi.START_TIME.Year, busi.START_TIME.Month, busi.START_TIME.Day, busi.ID);
                            //using (var newzip = new ZipFile())
                            //{
                            //    newzip.AddDirectory(zipfilePath);
                            //    newzip.Save(tempfile);
                            //}
                            //zipfileContent = File.ReadAllBytes(tempfile);
                            //return new ResultModel
                            //{
                            //    StatusCode = "000000",
                            //    BussinessModel = new BusinessModel
                            //    {
                            //        type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                            //        ID = (int)busi.ID,
                            //        name = busi.NAME,
                            //        IDum = busi.ID_NUM,
                            //        queueNum = busi.QUEUE_NUM,
                            //        address = busi.ADDRESS,
                            //        phoneNum = busi.PHONE_NUM,
                            //        attention = busi.ATTENTION,
                            //        zipFile = zipfileContent
                            //    }
                            //};

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("BusinessesPictureQuery", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }



    }
}