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
        private readonly Model15242 cd = new Model15242();
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
                OracleCommand com = OracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT carinfoid.nextval FROM dual");//写好想执行的Sql语句 
              //  Log.Info("CommandText=" + com.CommandText);
                OracleDataReader odr = com.ExecuteReader();
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
               
                    switch (param.countyCode)
                    {
                        //case "qixia":
                        //    return QixiauBusinessInfo( param);
                        //case "laishan":
                        //    return LaishanBusinessInfo( param);
                        
                        //case "muping":
                        //    return MupingBusinessInfo( param);
                        //case "longkou":
                        //    return LongkouBusinessInfo( param);

                        //case "laiyang":
                        //    return LaiyangBusinessInfo( param);
                        //case "laizhou":
                        //    return LaizhouBusinessInfo( param);
                        //case "penglai":
                        //    return PenglaiBusinessInfo( param);
                        //case "zhaoyuan":
                        //    return ZhaoyuanBusinessInfo( param);
                        //case "changdao":
                        //    return ChangdaoBusinessInfo( param);

                        //case "haiyang":
                        //    return HaiyangBusinessInfo(param);
                        //case "fushan":
                        //    return FushanBusinessInfo( param);
                        case "zhifu":
                        default:
                            return ZhifuBusinessInfo(param);
                        
                        //    return AllBusinessInfo( param);
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
        //private ResultModel AllBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.BUSSINESS.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        //    //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        //    //Log.Info("fpath is:" + fpath);
        //    //var fcontent = File.ReadAllBytes(@fpath);
        //    //Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //          //  zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel FushanBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.fushanbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        //    var fcontent = new byte[1];
        //    try
        //    {
        //        var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        //        Log.Info("fpath is:" + fpath);
        //         fcontent = File.ReadAllBytes(@fpath);
        //        Log.Info("fcontent is:" + fcontent.Length);
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //            zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel HaiyangBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.haiyangbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        //    //var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        //    //Log.Info("fpath is:" + fpath);
        //    //var fcontent = File.ReadAllBytes(@fpath);
        //    //Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //          //  zipFile = fcontent
        //        }
        //    };
        //}
        private ResultModel ZhifuBusinessInfo( BusinessModel param)
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
        //private ResultModel QixiauBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.qixiabusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        // //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        // ////   Log.Info("fpath is:" + fpath);
        // //   var fcontent = File.ReadAllBytes(@fpath);
        // // //  Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //         //   zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel LaishanBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.laishanbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        //  //  var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        //  ////  Log.Info("fpath is:" + fpath);
        //  //  var fcontent = File.ReadAllBytes(@fpath);
        //  ////  Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //           // zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel MupingBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.mupingbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        // //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        // ////   Log.Info("fpath is:" + fpath);
        // //   var fcontent = File.ReadAllBytes(@fpath);
        // // //  Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //           // zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel LongkouBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.longkoubusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        // //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        // ////   Log.Info("fpath is:" + fpath);
        // //   var fcontent = File.ReadAllBytes(@fpath);
        // ////   Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //          //  zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel LaiyangBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.laiyangbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        //  //  var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        //  ////  Log.Info("fpath is:" + fpath);
        //  //  var fcontent = File.ReadAllBytes(@fpath);
        //  ////  Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //           // zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel LaizhouBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.laizhoubusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        // //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        // ////   Log.Info("fpath is:" + fpath);
        // //   var fcontent = File.ReadAllBytes(@fpath);
        // ////   Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //         //   zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel PenglaiBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.penglaibusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        // //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        // ////   Log.Info("fpath is:" + fpath);
        // //   var fcontent = File.ReadAllBytes(@fpath);
        // ////   Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //         //   zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel ZhaoyuanBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.zhaoyuanbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        // //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        // ////   Log.Info("fpath is:" + fpath);
        // //   var fcontent = File.ReadAllBytes(@fpath);
        // // //  Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //          //  zipFile = fcontent
        //        }
        //    };
        //}
        //private ResultModel ChangdaoBusinessInfo( BusinessModel param)
        //{
        //    var busi = cd.changdaobusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

        //    if (busi == null)
        //        return new ResultModel
        //        {
        //            StatusCode = "000006",
        //            Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
        //        };
        //  //  var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
        //  ////  Log.Info("fpath is:" + fpath);
        //  //  var fcontent = File.ReadAllBytes(@fpath);
        //  ////  Log.Info("fcontent is:" + fcontent.Length);
        //    return new ResultModel
        //    {
        //        StatusCode = "000000",
        //        BussinessModel = new BusinessModel
        //        {
        //            type = int.Parse(busi.TYPE.ToString(CultureInfo.InvariantCulture)),
        //            name = busi.NAME,
        //            IDum = busi.ID_NUM,
        //            queueNum = busi.QUEUE_NUM,
        //            address = busi.ADDRESS,
        //            phoneNum = busi.PHONE_NUM,
        //            attention = busi.ATTENTION,
        //          //  zipFile = fcontent
        //        }
        //    };
        //}


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
               
                switch (param.countyCode)
                {
                    case "zhifu":
                      default:
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
                       
                        break;
                    //case "qixia":
                       
                    //        cd.qixiabusiness.Add(new qixiabusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            NAME = param.name,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            UPLOADER = param.userName,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "laishan":
                       
                    //        cd.laishanbusiness.Add(new laishanbusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            NAME = param.name,
                    //            ID_NUM = param.IDum,
                    //            UPLOADER = param.userName,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "muping":
                       
                    //        cd.mupingbusiness.Add(new mupingbusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            NAME = param.name,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            UPLOADER = param.userName,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "longkou":
                       
                    //        cd.longkoubusiness.Add(new longkoubusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            NAME = param.name,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            UPLOADER = param.userName,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "laiyang":
                      
                    //        cd.laiyangbusiness.Add(new laiyangbusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            NAME = param.name,
                    //            UPLOADER = param.userName,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "laizhou":
                       
                    //        cd.laizhoubusiness.Add(new laizhoubusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            NAME = param.name,
                    //            UPLOADER = param.userName,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "penglai":
                       
                    //        cd.penglaibusiness.Add(new penglaibusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            NAME = param.name,
                    //            UPLOADER = param.userName,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "zhaoyuan":
                       
                    //        cd.zhaoyuanbusiness.Add(new zhaoyuanbusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            NAME = param.name,
                    //            UPLOADER = param.userName,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            PHONE_NUM = param.phoneNum,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "changdao":
                        
                    //        cd.changdaobusiness.Add(new changdaobusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            UPLOADER = param.userName,
                    //            NAME = param.name,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "haiyang":
                       
                    //        cd.haiyangbusiness.Add(new haiyangbusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            UPLOADER = param.userName,
                    //            NAME = param.name,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //case "fushan":
                        
                    //        cd.fushanbusiness.Add(new fushanbusiness
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            UPLOADER = param.userName,
                    //            NAME = param.name,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
                    //default:
                        
                    //        cd.BUSSINESS.Add(new BUSSINESS
                    //        {
                    //            ID = id,
                    //            COUNTYCODE = param.countyCode,
                    //            UNLOAD_TASK_NUM = param.unloadTaskNum,
                    //            START_TIME = DateTime.Now,
                    //            END_TIME = DateTime.Now,
                    //            STATUS = param.status,
                    //            TYPE = param.type,
                    //            UPLOADER = param.userName,
                    //            NAME = param.name,
                    //            ID_NUM = param.IDum,
                    //            QUEUE_NUM = param.queueNum,
                    //            ADDRESS = param.address,
                    //            PHONE_NUM = param.phoneNum,
                    //            POSTADDR = param.postAddr,
                    //            POSTPHONE = param.postPhone,
                    //            ATTENTION = param.attention
                    //        });
                    //        cd.SaveChanges();
                        
                    //    break;
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

     
        private void InputLog(BusinessModel input)
        {
            Log.Info("input json string:" + JsonConvert.SerializeObject(input));
        }
    }
}
