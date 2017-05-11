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
using DataService;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class CarsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
     //   private static readonly string FileRootPath = ConfigurationManager.AppSettings["FileRootPath"];
        private readonly UserDbc _dbUserDbc = new UserDbc();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbUserDbc.Dispose();
                //  _dbLog.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("GetBusinessInfoByOdc")]
        [HttpPost]
        public ResultModel GetBusinessInfoByOdc([FromBody] BusinessModel param)
        {
            try
            {
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                InputLog(param);
                using (var cd = new ModelAllDb())
                {
                    switch (param.countyCode)
                    {
                        case "qixia":
                            return QixiauBusinessInfo(cd, param);
                        case "laishan":
                            return LaishanBusinessInfo(cd, param);
                        case "zhifu":
                            return ZhifuBusinessInfo(cd, param);
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

                        case "haiyang":
                            return HaiyangBusinessInfo(cd,param);
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
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("GetBusinessInfoByOdc", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        private ResultModel AllBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.BUSSINESS.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

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
        private ResultModel FushanBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.fushanbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
            var fcontent = new byte[1];
            try
            {
                var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
                Log.Info("fpath is:" + fpath);
                 fcontent = File.ReadAllBytes(@fpath);
                Log.Info("fcontent is:" + fcontent.Length);
            }
            catch (Exception ex)
            {
                
            }
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
                    zipFile = fcontent
                }
            };
        }
        private ResultModel HaiyangBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.haiyangbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

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
        private ResultModel ZhifuBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.zhifubusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

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
        private ResultModel QixiauBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.qixiabusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
         //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
         ////   Log.Info("fpath is:" + fpath);
         //   var fcontent = File.ReadAllBytes(@fpath);
         // //  Log.Info("fcontent is:" + fcontent.Length);
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
                 //   zipFile = fcontent
                }
            };
        }
        private ResultModel LaishanBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.laishanbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
          //  var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
          ////  Log.Info("fpath is:" + fpath);
          //  var fcontent = File.ReadAllBytes(@fpath);
          ////  Log.Info("fcontent is:" + fcontent.Length);
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
                   // zipFile = fcontent
                }
            };
        }
        private ResultModel MupingBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.mupingbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
         //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
         ////   Log.Info("fpath is:" + fpath);
         //   var fcontent = File.ReadAllBytes(@fpath);
         // //  Log.Info("fcontent is:" + fcontent.Length);
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
                   // zipFile = fcontent
                }
            };
        }
        private ResultModel LongkouBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.longkoubusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
         //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
         ////   Log.Info("fpath is:" + fpath);
         //   var fcontent = File.ReadAllBytes(@fpath);
         ////   Log.Info("fcontent is:" + fcontent.Length);
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
        private ResultModel LaiyangBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.laiyangbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
          //  var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
          ////  Log.Info("fpath is:" + fpath);
          //  var fcontent = File.ReadAllBytes(@fpath);
          ////  Log.Info("fcontent is:" + fcontent.Length);
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
                   // zipFile = fcontent
                }
            };
        }
        private ResultModel LaizhouBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.laizhoubusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
         //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
         ////   Log.Info("fpath is:" + fpath);
         //   var fcontent = File.ReadAllBytes(@fpath);
         ////   Log.Info("fcontent is:" + fcontent.Length);
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
                 //   zipFile = fcontent
                }
            };
        }
        private ResultModel PenglaiBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.penglaibusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
         //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
         ////   Log.Info("fpath is:" + fpath);
         //   var fcontent = File.ReadAllBytes(@fpath);
         ////   Log.Info("fcontent is:" + fcontent.Length);
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
                 //   zipFile = fcontent
                }
            };
        }
        private ResultModel ZhaoyuanBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.zhaoyuanbusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
         //   var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
         ////   Log.Info("fpath is:" + fpath);
         //   var fcontent = File.ReadAllBytes(@fpath);
         // //  Log.Info("fcontent is:" + fcontent.Length);
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
        private ResultModel ChangdaoBusinessInfo(ModelAllDb cd, BusinessModel param)
        {
            var busi = cd.changdaobusiness.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);

            if (busi == null)
                return new ResultModel
                {
                    StatusCode = "000006",
                    Result = "没有相关业务信息，请检查 上传任务代码：" + param.unloadTaskNum
                };
          //  var fpath = (CdmConfiguration.FileRootPath + param.countyCode + "\\" + busi.START_TIME + "\\" + busi.ID);
          ////  Log.Info("fpath is:" + fpath);
          //  var fcontent = File.ReadAllBytes(@fpath);
          ////  Log.Info("fcontent is:" + fcontent.Length);
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
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
              //  if (!PermissionCheck.Check(param))
                {
                    return new ResultModel {StatusCode = "000007", Result = "没有权限"};
                }
                //  Log.Info("PostBusinessFormInfo input is:" + JsonConvert.SerializeObject(param));
                var currentdate = DateTime.Now.Date;
                var scurrentdate = string.Format("{0}-{1}-{2}", currentdate.Year, currentdate.Month, currentdate.Day);
                var id = new OracleOperation().GetBusinessId(); //+param.checkFile;//test only
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
              
               
                switch (param.countyCode)
                {
                    case "zhifu":
                        using (var cd = new ModelAllDb())
                        {
                            cd.zhifubusiness.Add(new zhifubusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "qixia":
                        using (var cd = new ModelAllDb())
                        {
                            cd.qixiabusiness.Add(new qixiabusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "laishan":
                        using (var cd = new ModelAllDb())
                        {
                            cd.laishanbusiness.Add(new laishanbusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "muping":
                        using (var cd = new ModelAllDb())
                        {
                            cd.mupingbusiness.Add(new mupingbusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "longkou":
                        using (var cd = new ModelAllDb())
                        {
                            cd.longkoubusiness.Add(new longkoubusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "laiyang":
                        using (var cd = new ModelAllDb())
                        {
                            cd.laiyangbusiness.Add(new laiyangbusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "laizhou":
                        using (var cd = new ModelAllDb())
                        {
                            cd.laizhoubusiness.Add(new laizhoubusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "penglai":
                        using (var cd = new ModelAllDb())
                        {
                            cd.penglaibusiness.Add(new penglaibusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "zhaoyuan":
                        using (var cd = new ModelAllDb())
                        {
                            cd.zhaoyuanbusiness.Add(new zhaoyuanbusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "changdao":
                        using (var cd = new ModelAllDb())
                        {
                            cd.changdaobusiness.Add(new changdaobusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "haiyang":
                        using (var cd = new Business())
                        {
                            cd.Haiyangbusiness.Add(new haiyangbusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    case "fushan":
                        using (var cd = new Business())
                        {
                            cd.Fushanbusiness.Add(new fushanbusiness
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                    default:
                        using (var cd = new Business())
                        {
                            cd.Bussiness.Add(new BUSSINESS
                            {
                                ID = id,
                                COUNTYCODE = param.countyCode,
                                UNLOAD_TASK_NUM = param.unloadTaskNum,
                                START_TIME = scurrentdate,
                                STATUS = param.status,
                                TYPE = param.type,
                                NAME = param.name,
                                ID_NUM = param.IDum,
                                QUEUE_NUM = param.queueNum,
                                ADDRESS = param.address,
                                PHONE_NUM = param.phoneNum,
                                ATTENTION = param.attention
                            });
                            cd.SaveChanges();
                        }
                        break;
                }

                await Task.Run(async () =>
                {
                    await MessagePush.PushVoiceMessage(new CdmMessage
                    {
                        ClientType = ClientType.Voice,
                        Content = param.queueNum,
                        CountyCode = param.countyCode,
                        VoiceType = VoiceType.Fee
                    });
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
