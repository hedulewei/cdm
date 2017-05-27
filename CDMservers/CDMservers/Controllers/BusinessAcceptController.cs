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
                    case "changdao": return ChangdaoUpdateTask(param);
                    case "zhaoyuan": return ZhaoyuanUpdateTask(param);
                    case "penglai": return PenglaiUpdateTask(param);
                    case "laizhou": return LaizhouUpdateTask(param);
                    case "laiyang": return LaiyangUpdateTask(param);

                    case "longkou": return LongkouUpdateTask(param);
                    case "muping": return MupingUpdateTask(param);
                    case "laishan": return LaishanUpdateTask(param);
                    case "qixia": return QixiaUpdateTask(param);
                    case "fushan": return FushanUpdateTask(param);

                    case "haiyang": return HaiyangUpdateTask(param);
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        return ZhifuUpdateTask(param);
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

        private ResultModel ChangdaoUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_CHANGDAO.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel ZhaoyuanUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_ZHAOYUAN.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel PenglaiUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_PENGLAI.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel LaizhouUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_LAIZHOU.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel LaiyangUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_LAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel LongkouUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_LONGKOU.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel MupingUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_MUPING.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel LaishanUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_LAISHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel QixiaUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_QIXIA.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel FushanUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_FUSHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel HaiyangUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.BUSINESS_HAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
        }
        private ResultModel ZhifuUpdateTask(BusinessModel param)
        {
            var busiupdatetask = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
            if (busiupdatetask == null)
            {
                return BusinessFinishNotFound();
            }
            busiupdatetask.STATUS = 1;
            busiupdatetask.REJECT_REASON = string.Empty;
            busiupdatetask.START_TIME = DateTime.Now;
            busiupdatetask.END_TIME = DateTime.Now;
            busiupdatetask.QUEUE_NUM = param.queueNum;

            busiupdatetask.ID_NUM = param.IDum;
            busiupdatetask.ADDRESS = param.address;
            busiupdatetask.SERIAL_NUM = param.serialNum;
            busiupdatetask.NAME = param.name;
            busiupdatetask.PHONE_NUM = param.phoneNum;

            busiupdatetask.PROCESS_USER = param.userName;
            busiupdatetask.FILE_RECV_USER = param.fileRecvUser;
            //  busizhifu.TRANSFER_STATUS = param.transferStatus;
            //   busizhifu.UPLOADER = param.uploader;
            busiupdatetask.COMPLETE_PAY_USER = param.completePayUser;

            busiupdatetask.ATTENTION = param.attention;
            busiupdatetask.UNLOAD_TASK_NUM = param.unloadTaskNum;
            busiupdatetask.POSTPHONE = param.postPhone;
            busiupdatetask.POSTADDR = param.postAddr;
            busiupdatetask.CHECK_FILE = param.checkFile;

            busiupdatetask.CAR_NUM = param.carNum;
            busiupdatetask.TAX_NUM = param.texNum;
            busiupdatetask.TAX_TYPE = param.texType;
            busiupdatetask.ORIGIN_NUM = param.originNum;
            busiupdatetask.ORIGIN_TYPE = param.originType;
            cd.SaveChanges();
            return new ResultModel
            {
                StatusCode = "000000",
            };
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
                    case "changdao": return ChangdaoRejectList(param);
                    case "zhaoyuan": return ZhaoyuanRejectList(param);
                    case "penglai": return PenglaiRejectList(param);
                    case "laizhou": return LaizhouRejectList(param);
                    case "laiyang": return LaiyangRejectList(param);

                    case "longkou": return LongkouRejectList(param);
                    case "muping": return MupingRejectList(param);
                    case "laishan": return LaishanRejectList(param);
                    case "qixia": return QixiaRejectList(param);
                    case "fushan": return FushanRejectList(param);

                    case "haiyang": return HaiyangRejectList(param);
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        return ZhifuRejectList(param);
                    default:
                      

                        return new BusinessListResult { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
                }

            }
            catch (Exception ex)
            {
                Log.Error("BusinessesPictureQuery", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }

        }

        private BusinessListResult ChangdaoRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_CHANGDAO.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult ZhaoyuanRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_ZHAOYUAN.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult PenglaiRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_PENGLAI.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult LaizhouRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_LAIZHOU.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult LaiyangRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_LAIYANG.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult LongkouRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_LONGKOU.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult MupingRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_MUPING.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult LaishanRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_LAISHAN.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult QixiaRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_QIXIA.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult FushanRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_FUSHAN.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult HaiyangRejectList(BusinessModel param)
        {
            var busirejectlist = cd.BUSINESS_HAIYANG.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
        }
        private BusinessListResult ZhifuRejectList(BusinessModel param)
        {
            var busirejectlist = cd.ZHIFUBUSINESS.Where(c => c.STATUS == 4);

            var blist = new List<BusinessModel>();
            foreach (var rejectTask in busirejectlist)
            {
                blist.Add(new BusinessModel
                {
                    ID = (int)rejectTask.ID,
                    type = (int)rejectTask.TYPE,
                    startTime = rejectTask.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = rejectTask.END_TIME.ToString(CultureInfo.InvariantCulture),
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
                    case "changdao": return await ChangdaoRejectTask(param);
                    case "zhaoyuan": return await ZhaoyuanRejectTask(param);
                    case "penglai": return await PenglaiRejectTask(param);
                    case "laizhou": return await LaizhouRejectTask(param);
                    case "laiyang": return await LaiyangRejectTask(param);

                    case "longkou": return await LongkouRejectTask(param);
                    case "muping": return await MupingRejectTask(param);
                    case "laishan": return await LaishanRejectTask(param);
                    case "qixia": return await QixiaRejectTask(param);
                    case "fushan": return await FushanRejectTask(param);

                    case "haiyang": return await HaiyangRejectTask(param);
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        return await ZhifuRejectTask(param);
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

        private async Task<ResultModel> ChangdaoRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_CHANGDAO.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> ZhaoyuanRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_ZHAOYUAN.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> PenglaiRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_PENGLAI.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> LaizhouRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_LAIZHOU.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> LaiyangRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_LAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> LongkouRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_LONGKOU.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> MupingRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_MUPING.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> LaishanRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_LAISHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> QixiaRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_QIXIA.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> FushanRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_FUSHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> HaiyangRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.BUSINESS_HAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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

        }
        private async Task<ResultModel> ZhifuRejectTask(BusinessModel param)
        {
            var busirejecttask = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
            if (busirejecttask == null)
            {
                return BusinessFinishNotFound();
            }
            busirejecttask.STATUS = 4;
            busirejecttask.REJECT_REASON = param.rejectReason;

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
                    case "changdao": return await ChangdaoFinishTask(param);
                    case "zhaoyuan": return await ZhaoyuanFinishTask(param);
                    case "penglai": return await PenglaiFinishTask(param);
                    case "laizhou": return await LaizhouFinishTask(param);
                    case "laiyang": return await LaiyangFinishTask(param);

                    case "longkou": return await LongkouFinishTask(param);
                    case "muping": return await MupingFinishTask(param);
                    case "laishan": return await LaishanFinishTask(param);
                    case "qixia": return await QixiaFinishTask(param);
                    case "fushan": return await FushanFinishTask(param);

                    case "haiyang": return await HaiyangFinishTask(param);
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        return await ZhifuFinishTask(param);
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

        private async Task<ResultModel> ChangdaoFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_CHANGDAO.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return  BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> ZhaoyuanFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_ZHAOYUAN.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> PenglaiFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_PENGLAI.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> LaizhouFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_LAIZHOU.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> LaiyangFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_LAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> LongkouFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_LONGKOU.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> MupingFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_MUPING.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> LaishanFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_LAISHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> QixiaFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_QIXIA.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> FushanFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_FUSHAN.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> HaiyangFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.BUSINESS_HAIYANG.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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

        }
        private async Task<ResultModel> ZhifuFinishTask(BusinessModel param)
        {
            var busifinishtask = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.ID == param.ID);
            if (busifinishtask == null)
            {
                return BusinessFinishNotFound();
            }
            busifinishtask.STATUS = 5;
            busifinishtask.SERIAL_NUM = param.serialNum;
            busifinishtask.PROCESS_USER = param.userName;
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
                    case "changdao": return await ChangdaoAcquireTask(param,lockvalue);
                    case "zhaoyuan": return await ZhaoyuanAcquireTask(param,lockvalue);
                    case "penglai": return await PenglaiAcquireTask(param,lockvalue);
                    case "laizhou": return await LaizhouAcquireTask(param,lockvalue);
                    case "laiyang": return await LaiyangAcquireTask(param,lockvalue);

                    case "longkou": return await LongkouAcquireTask(param,lockvalue);
                    case "muping": return await MupingAcquireTask(param,lockvalue);
                    case "laishan": return await LaishanAcquireTask(param,lockvalue);
                    case "qixia": return await QixiaAcquireTask(param,lockvalue);
                    case "fushan": return await FushanAcquireTask(param,lockvalue);

                    case "haiyang": return await HaiyangAcquireTask(param,lockvalue);
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

        private async Task<ResultModel> ChangdaoAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_CHANGDAO.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> ZhaoyuanAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_ZHAOYUAN.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> PenglaiAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_PENGLAI.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> LaizhouAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_LAIZHOU.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> LaiyangAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_LAIYANG.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> LongkouAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_LONGKOU.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> MupingAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_MUPING.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> LaishanAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_LAISHAN.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> QixiaAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_QIXIA.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> FushanAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_FUSHAN.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> HaiyangAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
        {
            var pts = UserService.GetPermissionType(cd.USERS, param.userName);
            var found = false;

            var ret = new BusinessModel();
            lock (lockvalue)
            {
                var currentTop100 = cd.BUSINESS_HAIYANG.Where(c => c.STATUS == 1 && c.QUEUE_NUM != string.Empty).OrderBy(c => c.ID).Take(100);

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
        private async Task<ResultModel> ZhifuAcquireTask(BusinessModel param, Dictionary<string, string> lockvalue)
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