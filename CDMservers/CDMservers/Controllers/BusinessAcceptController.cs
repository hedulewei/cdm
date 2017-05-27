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


                switch (param.countyCode.ToLower())
                {
                    case "changdao": return ChangdaoDestroy(param);
                    case "zhaoyuan": return ZhaoyuanDestroy(param);
                    case "penglai": return PenglaiDestroy(param);
                    case "laizhou": return LaizhouDestroy(param);
                    case "laiyang": return LaiyangDestroy(param);

                    case "longkou": return LongkouDestroy(param);
                    case "muping": return MupingDestroy(param);
                    case "laishan": return LaishanDestroy(param);
                    case "qixia": return QixiaDestroy(param);
                    case "fushan": return FushanDestroy(param);

                    case "haiyang": return HaiyangDestroy(param);
                    case "zhifu":
                    case "shisuo":
                    case "dacheng":
                        return ZhifuDestroy(param);
                    default:
                       
                        return new ResultModel { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
                       
                }

            }
            catch (Exception ex)
            {
                Log.Error("DestroyTask", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }

        private ResultModel ChangdaoDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_CHANGDAO.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel ZhaoyuanDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_ZHAOYUAN.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel PenglaiDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_PENGLAI.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel LaizhouDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_LAIZHOU.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel LaiyangDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_LAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel LongkouDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_LONGKOU.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel MupingDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_MUPING.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel LaishanDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_LAISHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel QixiaDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_QIXIA.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel FushanDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_FUSHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel ZhifuDestroy(BusinessModel param)
        {
            var busidestroy = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel HaiyangDestroy(BusinessModel param)
        {
            var busidestroy = cd.BUSINESS_HAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busidestroy == null)
            {
                return BusinessFinishNotFound();
            }
            busidestroy.STATUS = 6;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
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
                                ID = (int)rejectTask.ID,
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
                            BussinessList = blist,
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
        public async Task<ResultModel> RejectTask([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("RejectTask input:" + JsonConvert.SerializeObject(param));


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

                        await Task.Run(async () =>
                        {

                            await MessagePush.PushVoiceMessage(new CdmMessage
                            {
                                ClientType = ClientType.Voice,
                                Content = param.queueNum,
                                CountyCode = param.countyCode,
                                VoiceType = VoiceType.Reject
                            });
                            await MessagePush.PushLedMessage(new CdmMessage
                            {
                                ClientType = ClientType.Led,
                                Content = param.queueNum,
                                CountyCode = param.countyCode,
                                LedMsgType = LedMsgType.Reject
                            });


                        });
                        return new ResultModel
                        {
                            StatusCode = "000000",
                        };


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
        public async Task<ResultModel> FinishTask([FromBody] BusinessModel param)
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
                        await Task.Run(async () =>
                        {
                            await MessagePush.PushVoiceMessage(new CdmMessage
                            {
                                ClientType = ClientType.Voice,
                                Content = param.queueNum,
                                CountyCode = param.countyCode,
                                VoiceType = VoiceType.Fee
                            });
                            await MessagePush.PushLedMessage(new CdmMessage
                            {
                                ClientType = ClientType.Led,
                                Content = param.queueNum,
                                CountyCode = param.countyCode,
                                LedMsgType = LedMsgType.Done
                            });

                        });
                        return new ResultModel
                        {
                            StatusCode = "000000",
                        };

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
        public async Task<ResultModel> AcquireTask([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("AcquireTask input:" + JsonConvert.SerializeObject(param));

                string lockcode;
                switch (param.countyCode.ToLower())
                {
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        lockcode = "zhifu";
                        break;
                    default:
                        lockcode = param.countyCode;
                        break;
                }
                var lockvalue = queueLock[lockcode];

                switch (param.countyCode.ToLower())
                {
                    case "changdao": 
                    case "zhaoyuan": 
                    case "penglai": 
                    case "laizhou":
                    case "laiyang": 

                    case "longkou": 
                    case "muping":
                    case "laishan": 
                    case "qixia": 
                    case "fushan": 

                    case "haiyang": 
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        return await ZhifuAcquireTask(param,lockvalue);
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

        private async Task<ResultModel> ZhifuAcquireTask(BusinessModel param,Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.ZHIFUBUSINESS.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

                foreach (var busiacquire in currentTop100)
                {
                    if (pts.Contains((int)busiacquire.TYPE) && busiacquire.START_TIME.CompareTo(DateTime.Now.Date) >= 0)
                    {
                        busiacquire.STATUS = 3;
                        busiacquire.PROCESS_USER = param.userName;
                        cd.SaveChanges();
                        ret = new BusinessModel
                        {
                            type = int.Parse(busiacquire.TYPE.ToString(CultureInfo.InvariantCulture)),
                            countyCode = busiacquire.COUNTYCODE,
                            ID = (int)busiacquire.ID,
                            name = busiacquire.NAME,
                            IDum = busiacquire.ID_NUM,
                            serialNum = busiacquire.SERIAL_NUM,

                            queueNum = busiacquire.QUEUE_NUM,
                            address = busiacquire.ADDRESS,
                            phoneNum = busiacquire.PHONE_NUM,
                            carNum = busiacquire.CAR_NUM,
                            texType = busiacquire.TAX_TYPE,

                            texNum = busiacquire.TAX_NUM,
                            originType = busiacquire.ORIGIN_TYPE,
                            originNum = busiacquire.ORIGIN_NUM,
                            attention = busiacquire.ATTENTION,

                            postAddr = busiacquire.POSTADDR,
                            postPhone = busiacquire.POSTPHONE,
                        };
                        found = true;
                        break;
                    }
                }
            }
            if (!found)
                return BusinessNotFound();
            await Task.Run(async () =>
            {
                await MessagePush.PushLedMessage(new CdmMessage
                {
                    ClientType = ClientType.Led,
                    Content = param.queueNum,
                    CountyCode = param.countyCode,
                    LedMsgType = LedMsgType.Processing
                });
            });

            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = ret,
            };
        }
    }
}