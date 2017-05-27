using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
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
    public class AutoController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       
        private readonly Model1525 cd = new Model1525();
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cd.Dispose();
            }
            base.Dispose(disposing);
        }
       

        [Route("TabulationUpload")]
        [HttpPost]
        public  ResultModel TabulationUpload([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
              
                Log.Info("TabulationUpload input is:" + JsonConvert.SerializeObject(param));
              
                var id = InternalService.GetBusinessId(); //+param.checkFile;//test only
             
                switch (param.countyCode.ToLower())
                {
                    case "changdao": return ChangdaoTabulationUpload(param,id);
                    case "zhaoyuan": return ZhaoyuanTabulationUpload(param,id);
                    case "penglai": return PenglaiTabulationUpload(param,id);
                    case "laizhou": return LaizhouTabulationUpload(param,id);
                    case "laiyang": return LaiyangTabulationUpload(param,id);

                    case "longkou": return LongkouTabulationUpload(param,id);
                    case "muping": return MupingTabulationUpload(param,id);
                    case "laishan": return LaishanTabulationUpload(param,id);
                    case "qixia": return QixiaTabulationUpload(param,id);
                    case "fushan": return FushanTabulationUpload(param,id);

                    case "haiyang": return HaiyangTabulationUpload(param,id);
                    case "zhifu":
                    case "shisuo":
                    case "dacheng":
                        return ZhifuTabulationUpload(param,id);
                    default:

                        return new ResultModel { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
                }
                ;
             
            }
            catch (DbEntityValidationException e)
            {
                var err = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    var err1 =
                        string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    err += err1;
                    Log.InfoFormat(err1);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var err2 = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        Log.InfoFormat(err2);
                        err += err2;
                    }
                }
                return new ResultModel { StatusCode = "000003", Result = "DbEntityValidationException:" + err };
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("TabulationUpload", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        private ResultModel ChangdaoTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_CHANGDAO.Add(new BUSINESS_CHANGDAO
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel ZhaoyuanTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_ZHAOYUAN.Add(new BUSINESS_ZHAOYUAN
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel PenglaiTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_PENGLAI.Add(new BUSINESS_PENGLAI
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel LaizhouTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_LAIZHOU.Add(new BUSINESS_LAIZHOU
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel LaiyangTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_LAIYANG.Add(new BUSINESS_LAIYANG
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel LongkouTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_LONGKOU.Add(new BUSINESS_LONGKOU
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel MupingTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_MUPING.Add(new BUSINESS_MUPING
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel LaishanTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_LAISHAN.Add(new BUSINESS_LAISHAN
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel QixiaTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_QIXIA.Add(new BUSINESS_QIXIA
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel FushanTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_FUSHAN.Add(new BUSINESS_FUSHAN
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel HaiyangTabulationUpload(BusinessModel param, int id)
        {
            cd.BUSINESS_HAIYANG.Add(new BUSINESS_HAIYANG
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
        private ResultModel ZhifuTabulationUpload(BusinessModel param, int id)
        {
            cd.ZHIFUBUSINESS.Add(new ZHIFUBUSINESS
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = 9,
                TYPE = param.type,
                NAME = param.name,
                ID_NUM = param.IDum,
                QUEUE_NUM = param.queueNum,
                UPLOADER = param.userName,
                ADDRESS = param.address,
                PHONE_NUM = param.phoneNum,
                ATTENTION = param.attention,
                POSTADDR = param.postAddr,
                POSTPHONE = param.postPhone,
                CAR_NUM = param.carNum,
                TAX_NUM = param.texNum,
                TAX_TYPE = param.texType,
                ORIGIN_NUM = param.originNum,
                ORIGIN_TYPE = param.originType,
                TRANSFER_STATUS = 0,
            });
            cd.SaveChanges();
            return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { ID = id } };
        }
    }
}