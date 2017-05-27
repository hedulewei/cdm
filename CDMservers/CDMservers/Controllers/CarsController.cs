using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace CDMservers.Controllers
{
    public class CarsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserDbc _dbUserDbc = new UserDbc();
        private readonly NewDblog _dbLog = new NewDblog();
        private readonly Model1525 cd = new Model1525();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbUserDbc.Dispose();
                  _dbLog.Dispose();
                  cd.Dispose();
            }
            base.Dispose(disposing);
        }
        private int GetCarInfoId()
        {
            using (var OracleConnectionconn = new OracleConnection(CdmConfiguration.DataSource))
            {
                OracleConnectionconn.Open();//打开指定的连接  
                OracleDataReader odr;
                using (var com = OracleConnectionconn.CreateCommand())
                {
                    com.CommandText = string.Format("SELECT carinfoid.nextval FROM dual");//写好想执行的Sql语句 
                    //  Log.Info("CommandText=" + com.CommandText);
                    odr = com.ExecuteReader();
                }
                var ordinal = -1;
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    ordinal = odr.GetInt32(0);
                }
                odr.Close();//关闭reader.这是一定要写的  
               OracleConnectionconn.Close();
                return ordinal;
            }
        }
//        客户端发送：上传车辆信息指令，内容车牌，车辆品牌，车辆类型，大驾号，号牌种类，车主姓名，车主身份证号码，表单编号，地区编号。
//服务器返回：是否成功。
        [Route("CarInfoUpload")]
        [HttpPost]
        public CommonResult CarInfoUpload([FromBody] CarInfoUploadRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                var today = DateTime.Now;
                var id = GetCarInfoId();
                Log.InfoFormat("{0},{1},{2},{3}",id,111,param.CAR_HEIGHT, param.CAR_WIDTH);
                cd.CARINFOR.Add(new CARINFOR
                {
                    ID = id,
                    TIME=today,
                    CAR_NUM = param.CAR_NUM,

                    BRAND = param.BRAND,
                    MODEL_TYPE = param.MODEL_TYPE,
                    VIN = param.VIN,
                    PLATE_TYPE = param.PLATE_TYPE,
                    OWNER = param.OWNER,

                    OWNER_ID = param.OWNER_ID,
                    CAR_HEIGHT = param.CAR_HEIGHT,
                    CAR_WIDTH = param.CAR_WIDTH,
                    CAR_LENGTH = param.CAR_LENGTH,
                    SERIAL_NUM = param.SERIAL_NUM,

                    STANDARD_HEIGHT = param.STANDARD_HEIGHT,
                    STANDARD_LENGTH = param.STANDARD_LENGTH,
                    STANDARD_WIDTH = param.STANDARD_WIDTH,
                    QUEUE_NUM = param.QUEUE_NUM,
                    FINISH = param.FINISH,

                    TASK_TYPE = param.TASK_TYPE,
                    INSPECTOR = param.INSPECTOR,
                    RECHECKER = param.RECHECKER,
                    UNLOAD_TASK_NUM = param.UNLOAD_TASK_NUM,
                    INVALID_TASK = param.INVALID_TASK,
                });
                Log.InfoFormat("{0},{1}", id, 222);
                cd.SaveChanges();
                Log.InfoFormat("{0},{1}", id, 333);
                return new CommonResult
                {
                    StatusCode = "000000",
                    Result = ""
                };
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
                return new CommonResult { StatusCode = "000003", Result = "DbEntityValidationException:" + err };
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("CarInfoUpload", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }
        }
        [Route("GetBusinessInfoByOdc")]
        [HttpPost]
        public ResultModel GetBusinessInfoByOdc([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                InputLog(param);
               
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
                        case "zhifu":
                        case "shisuo":
                        case "dacheng": return ZhifuBusinessInfo(param);
                        default:
                          

                            return new ResultModel { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
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
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("GetBusinessInfoByOdc", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }

        private ResultModel ChangdaoBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_CHANGDAO.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                  //  zipFile = fcontent
                }
            };
        }

        private ResultModel ZhaoyuanBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_ZHAOYUAN.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel PenglaiBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_PENGLAI.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel LaizhouBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_LAIZHOU.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel LaiyangBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_LAIYANG.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel LongkouBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_LONGKOU.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel MupingBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_MUPING.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel LaishanBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_LAISHAN.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel QixiaBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_QIXIA.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel FushanBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_FUSHAN.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel HaiyangBusinessInfo(BusinessModel param)
        {
            var busi = cd.BUSINESS_HAIYANG.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }
        private ResultModel ZhifuBusinessInfo(BusinessModel param)
        {
            var busi = cd.ZHIFUBUSINESS.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
            //Log.Info("fpath is:" + fpath);
            //var fcontent = File.ReadAllBytes(@fpath);
            //Log.Info("fcontent is:" + fcontent.Length);
            return new ResultModel
            {
                StatusCode = "000000",
                BussinessModel = new BusinessModel
                {
                    type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
                    name = busi.NAME,
                    IDum = busi.ID_NUM,
                    queueNum = busi.QUEUE_NUM,
                    address = busi.ADDRESS,
                    phoneNum = busi.PHONE_NUM,
                    attention = busi.ATTENTION,
                    //  zipFile = fcontent
                }
            };
        }

        [Route("PostBusinessFormInfo")]
        [HttpPost]
        public async Task<ResultModel> PostBusinessFormInfo([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
              //  if (!PermissionCheck.Check(param))
                {
                    return new ResultModel {StatusCode = "000007", Result = "没有权限"};
                }
                LogIntoDb.Log(_dbLog, param.userName, param.type.ToString(CultureInfo.InvariantCulture), JsonConvert.SerializeObject(param));
                  Log.Info("PostBusinessFormInfo input is:" + JsonConvert.SerializeObject(param));
                var currentdate = DateTime.Now.Date;
                var scurrentdate = string.Format("{0}-{1}-{2}", currentdate.Year, currentdate.Month, currentdate.Day);
                var id = InternalService.GetBusinessId(); //+param.checkFile;//test only
                //Log.Info("path 11 =" + id);
                //if (!string.IsNullOrEmpty(param.fileName))
                //{
                //    var filepath = string.Format("{2}{0}\\{1}\\{3}", param.countyCode, scurrentdate, CdmConfiguration.FileRootPath, param.ID);

                //    if (!Directory.Exists(@filepath))
                //    {
                //        Log.Info("path=" + filepath);
                //        Directory.CreateDirectory(@filepath);
                //    }
                //    var filename = string.Format("{0}\\{1}", filepath, param.fileName);
                //    Log.Info("file name=" + filename);
                //    File.WriteAllBytes(filename, param.zipFile);
                //    return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel{ID = param.ID,fileName=param.fileName} };
                //}
                param.status = (int)BusinessStatus.Upload;
               
                switch (param.countyCode.ToLower())
                {
                    case "changdao":  ChangdaoPostFormInfo(param, id); break;
                    case "zhaoyuan":  ZhaoyuanPostFormInfo(param, id); break;
                    case "penglai":  PenglaiPostFormInfo(param, id); break;
                    case "laizhou":  LaizhouPostFormInfo(param, id); break;
                    case "laiyang":  LaiyangPostFormInfo(param, id); break;

                    case "longkou":  LongkouPostFormInfo(param, id); break;
                    case "muping":  MupingPostFormInfo(param, id); break;
                    case "laishan":  LaishanPostFormInfo(param, id); break;
                    case "qixia":  QixiaPostFormInfo(param, id); break;
                    case "fushan":  FushanPostFormInfo(param, id); break;

                    case "haiyang":  HaiyangPostFormInfo(param, id); break;
                    case "zhifu":
                    case "shisuo":
                    case "dacheng":
                         ZhifuPostFormInfo(param,id);
                        break;
                      default:

                        return new ResultModel { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
                
                }

                await Task.Run(async () =>
                {
                    if (param.status == (int)BusinessStatus.Reject)
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
                    }
                    if (param.status == (int)BusinessStatus.Fee)
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
                    }
                    if (param.status == (int)BusinessStatus.Processing)
                    {
                        await MessagePush.PushLedMessage(new CdmMessage
                        {
                            ClientType = ClientType.Led,
                            Content = param.queueNum,
                            CountyCode = param.countyCode,
                            LedMsgType = LedMsgType.Processing
                        });
                    }
                });
            ;
                return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel{ID=id} };
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
                Log.Error("PostBusinessFormInfo", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }

        private void ChangdaoPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_CHANGDAO.Add(new BUSINESS_CHANGDAO
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void ZhaoyuanPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_ZHAOYUAN.Add(new BUSINESS_ZHAOYUAN
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void PenglaiPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_PENGLAI.Add(new BUSINESS_PENGLAI
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void LaizhouPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_LAIZHOU.Add(new BUSINESS_LAIZHOU
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void LaiyangPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_LAIYANG.Add(new BUSINESS_LAIYANG
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void LongkouPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_LONGKOU.Add(new BUSINESS_LONGKOU
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void MupingPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_MUPING.Add(new BUSINESS_MUPING
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void LaishanPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_LAISHAN.Add(new BUSINESS_LAISHAN
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void QixiaPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_QIXIA.Add(new BUSINESS_QIXIA
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void FushanPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_FUSHAN.Add(new BUSINESS_FUSHAN
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void HaiyangPostFormInfo(BusinessModel param, int id)
        {
            cd.BUSINESS_HAIYANG.Add(new BUSINESS_HAIYANG
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void ZhifuPostFormInfo(BusinessModel param, int id)
        {
            cd.ZHIFUBUSINESS.Add(new ZHIFUBUSINESS
            {
                ID = id,
                COUNTYCODE = param.countyCode,
                UNLOAD_TASK_NUM = param.unloadTaskNum,
                START_TIME = DateTime.Now,
                END_TIME = DateTime.Now,
                STATUS = param.status,
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
        }

        private void InputLog(BusinessModel input)
        {
            Log.Info("input json string:" + JsonConvert.SerializeObject(input));
        }
    }
}
