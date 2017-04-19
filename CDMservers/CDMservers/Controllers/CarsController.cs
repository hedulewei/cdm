using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
        private const string FileRoot = "d:\\";
        //private readonly DbContext _citydb ;
        //public CarsController(DbContext citydb)
        //{
        //    _citydb = citydb;
        //}
        [Route("GetBusinessInfoByOdc")]
        [HttpPost]
        public ResultModel GetBusinessInfoByOdc([FromBody] BusinessModel param)
        {
            try
            {
                //var param = JsonConvert.DeserializeObject<BusinessModel>(paramin);
                InputLog(param);
                var ret = new BUSSINESS();
                using (var cd = new CityData())
                {
                     ret = cd.BUSSINESS.FirstOrDefault(c => c.UNLOAD_TASK_NUM == param.unloadTaskNum);
                }
                var fpath = (FileRoot + param.countyCode + "\\" + ret.START_TIME + "\\" + ret.ID);
                Log.Info("fpath is:"+fpath);
                var fcontent = File.ReadAllBytes(@fpath);
                Log.Info("fcontent is:"+fcontent.Length);
                return new ResultModel { statusCode = "000000", bussinessModel = new BusinessModel{type=int.Parse(ret.TYPE.ToString()),name=ret.NAME,
                    IDum=ret.ID_NUM,queueNum=ret.QUEUE_NUM,address=ret.ADDRESS,phoneNum=ret.PHONE_NUM,attention=ret.ATTENTION,zipFile = fcontent} };
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
                return new ResultModel { statusCode = "000003", result = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("GetBusinessInfoByOdc", ex);
                return new ResultModel { statusCode = "000003", result = ex.Message };
            }
        }

        [Route("PostBusinessFormInfo")]
        [HttpPost]
        public ResultModel PostBusinessFormInfo([FromBody] BusinessModel param)
        {
            try
            {
                //Log.Info("input is:"+paramin);
                //var param = JsonConvert.DeserializeObject<BusinessModel>(paramin);
                InputLog(param);
                var id = new OracleOperation().GetBusinessId();
                var currentdate = DateTime.Now.Date;
                var scurrentdate = string.Format("{0}-{1}-{2}", currentdate.Year, currentdate.Month, currentdate.Day);

                var filepath = string.Format("{2}{0}\\{1}", param.countyCode,scurrentdate,FileRoot);
                Log.Info("path 11 =" + filepath);
                if (!Directory.Exists(@filepath))
                {
                    Log.Info("path=" + filepath);
                    Directory.CreateDirectory(@filepath);
                }
                var filename = string.Format("{0}\\{1}", filepath, id);
                Log.Info("file name=" + filename);
                File.WriteAllBytes(filename, param.zipFile);

                using (var cd=new CityData())
                {
                    cd.BUSSINESS.Add(new BUSSINESS{ID=id,UNLOAD_TASK_NUM = param.unloadTaskNum,START_TIME =scurrentdate, STATUS = param.status,TYPE=param.type,NAME=param.name,ID_NUM=param.IDum,QUEUE_NUM=param.queueNum,ADDRESS=param.address,PHONE_NUM=param.phoneNum,ATTENTION=param.attention});
                    cd.SaveChanges();
                   
                }
               // _citydb
                return new ResultModel { statusCode =  "000000", bussinessModel = new BusinessModel ()};
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
                return new ResultModel { statusCode = "000003", result = ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("PostBusinessFormInfo", ex);
                return new ResultModel { statusCode = "000003", result = ex.Message };
            }
        }


        private void InputLog(BusinessModel input)
        {
            Log.Info("input json string:"+JsonConvert.SerializeObject(input));
        }
    }
}
