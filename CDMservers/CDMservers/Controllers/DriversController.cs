using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using CDMservers.Models;
using Common;
using log4net;
using Oracle.ManagedDataAccess.Client;

namespace CDMservers.Controllers
{
    public class DriversController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
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
        [System.Web.Http.Route("generalChangeCertification")]
        [System.Web.Http.HttpPost]
        public ResultModel GeneralChangeCertification([FromBody] BusinessModel param)
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
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                InputLog(param);
              
                var ret = dueAndChangeCertification(param);

                return new ResultModel { StatusCode = ret == 1 ? "000000" : "000004", BussinessModel = new BusinessModel { queueNum = InternalService.GetOrdinal(param).ToString() } };
            }
            catch (Exception ex)
            {
                Log.Error("ordinaryChangeCertification", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        [Route("dueAndChangeCertification")]
        [HttpPost]
        public ResultModel DueAndChangeCertification([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
               // if (!PermissionCheck.Check(param))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                InputLog(param);
               
                var ret = dueAndChangeCertification(param);

                return new ResultModel { StatusCode = ret == 1 ? "000000" : "000004", BussinessModel = new BusinessModel { queueNum = InternalService.GetOrdinal(param).ToString() } };
            }
            catch (Exception ex)
            {
                Log.Error("dueAndChangeCertification", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }

        private int dueAndChangeCertification(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(CdmConfiguration.DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                using (OracleCommand com = oracleConnectionconn.CreateCommand())
                {
                    com.CommandText = string.Format("SELECT businessSequence.nextval FROM dual");//写好想执行的Sql语句 
                    Log.Info("CommandText=" + com.CommandText);
                    OracleDataReader odr = com.ExecuteReader();
                    var ordinal = -1;
                    while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                    {
                        ordinal = odr.GetInt32(0);
                    }
                    odr.Close();//关闭reader.这是一定要写的  
                    //Log.Info("sequence.next="+ordinal);
                    var currentdate = DateTime.Now.Date;

                    var scurrentdate = string.Format("{0}/{1}/{2}", currentdate.Year, currentdate.Month, currentdate.Day);
                    com.CommandText = string.Format("insert into bussiness (id,type,start_time,status,queue_num,name,id_num,address,phone_num,attention) values({0},{1},'{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}')",
                        ordinal, input.type, scurrentdate, 3, input.queueNum, input.name, input.IDum, input.address, input.phoneNum, input.attention);//写好想执行的Sql语句   
                    Log.Info("insert CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
            }
        }
        private void InputLog(BusinessModel input)
        {
            Log.Info(string.Format("userName={0},counterNum={1},countyCode={2}", input.userName, input.counterNum,
                input.countyCode));
        }
    }
}
