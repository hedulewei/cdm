using System.Data.Entity.Validation;
using System.Reflection;
using System.Web.UI.WebControls;
using CDMservers.Models;
using Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace CDMservers.Controllers
{
    public class ManageController : ApiController
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
        [Route("RetrieveCorporateInfo")]
        [HttpPost]
        public ResultModel RetrieveCorporateInfo([FromBody] BusinessModel param)
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
               
                var a = RetrieveCorporateInfoFromdb(param);
                Log.Info("RetrieveCorporateInfo 222");
                return new ResultModel { StatusCode = a.name == string.Empty ? "000000" : "000013", BussinessModel = a };
            }
            catch (Exception ex)
            {
                Log.Error("RetrieveCorporateInfo", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        private BusinessModel RetrieveCorporateInfoFromdb(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(CdmConfiguration.DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT name,address from corporateinfo where code='{0}'", input.IDum);
                OracleDataReader odr = com.ExecuteReader();
                var count = new BusinessModel();
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    count.name = odr.GetString(0);
                    count.address = odr.GetString(1);
                }
                odr.Close();
                return count;
            }
        }
        [Route("SendCorporateInfo")]
        [HttpPost]
        public ResultModel SendCorporateInfo([FromBody] BusinessModel param)
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
               
                var a = SendCorporateInfoFromdb(param);
                Log.Info("SendCorporateInfo 222");
                return new ResultModel { StatusCode = a == 1 ? "000000" : "000012", BussinessModel = new BusinessModel() };
            }
            catch (Exception ex)
            {
                Log.Error("SendCorporateInfo", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        private int SendCorporateInfoFromdb(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(CdmConfiguration.DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT count(code) from corporateinfo where code='{0}'", input.IDum);
                OracleDataReader odr = com.ExecuteReader();
                var count = 0;
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    count = odr.GetInt32(0);
                }
                odr.Close();
                if (count < 1)
                {
                    Log.Info("insert aaa=");
                    com.CommandText = string.Format("insert into corporateinfo (code,name,address) values('{0}','{1}','{2}')",
                        input.IDum, input.name, input.address);//写好想执行的Sql语句   
                    Log.Info("insert bbb=");
                    Log.Info("insert CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
                else
                {
                    Log.Info("update aaa=");
                    com.CommandText = string.Format("update corporateinfo set name='{0}',address='{1}' where code='{2}'",
                        input.name, input.address, input.IDum);//写好想执行的Sql语句   
                    Log.Info("update bbb=");
                    Log.Info("update CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
            }
        }
        [Route("RetrieveCellPhoneNumber")]
        [HttpPost]
        public ResultModel RetrieveCellPhoneNumber([FromBody] BusinessModel param)
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
              
                var a = RetrieveCellPhoneNumberFromdb(param);
                Log.Info("RetrieveCellPhoneNumber 222");
                return new ResultModel { StatusCode = a == string.Empty ? "000000" : "000011", BussinessModel = new BusinessModel{phoneNum=a} };
            }
            catch (Exception ex)
            {
                Log.Error("RetrieveCellPhoneNumber", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        private string RetrieveCellPhoneNumberFromdb(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(CdmConfiguration.DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT mobile from population where idnum='{0}'", input.IDum);
                OracleDataReader odr = com.ExecuteReader();
                var count = string.Empty;
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    count = odr.GetString(0);
                }
                odr.Close();
                return count;
            }
        }
        [Route("SendIdentityCardInfo")]
        [HttpPost]
        public ResultModel SendIdentityCardInfo([FromBody] BusinessModel param)
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
               
                var a = SendIdentityCardInfoFromdb(param);
                Log.Info("SendIdentityCardInfo 222");
                return new ResultModel { StatusCode = a==1?"000000":"000010", BussinessModel = new BusinessModel () };
            }
            catch (Exception ex)
            {
                Log.Error("SendIdentityCardInfo", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        private int SendIdentityCardInfoFromdb(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(CdmConfiguration.DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT count(idnum) from population where idnum='{0}'", input.IDum);
                OracleDataReader odr = com.ExecuteReader();
                var count = 0;
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    count = odr.GetInt32(0);
                }
                odr.Close();
                if (count < 1)
                {
                    Log.Info("insert aaa=");
                    com.CommandText = string.Format("insert into population (name,sex,nation,born,address,postcode,idnum,mobile) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                        input.name, input.Gender, input.Nationality, input.Birthday, input.address, input.ZipCode, input.IDum, input.phoneNum);//写好想执行的Sql语句   
                    Log.Info("insert bbb=");
                    Log.Info("insert CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
                else
                {
                    Log.Info("update aaa=");
                    com.CommandText = string.Format("update population set name='{0}',sex='{1}',nation='{2}',born='{3}',address='{4}',postcode='{5}',mobile='{7}' where idnum='{6}'",
                        input.name, input.Gender, input.Nationality, input.Birthday, input.address, input.ZipCode, input.IDum, input.phoneNum);//写好想执行的Sql语句   
                    Log.Info("update bbb=");
                    Log.Info("update CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
            }
        }
        [Route("GET_QUEUE_NUM")]
        [HttpPost]
        public ResultModel GET_QUEUE_NUM([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
               // if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
               //// if (!PermissionCheck.Check(param))
               // {
               //     return new ResultModel { StatusCode = "000007", Result = "没有权限" };
               // }
                InputLog(param);
               // var oo = new OracleOperation();
                return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { queueNum = InternalService.GetOrdinal(param).ToString() } };
            }
            catch (Exception ex)
            {
                Log.Error("GET_QUEUE_NUM", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
      
        private void InputLog(BusinessModel input)
        {
            Log.Info(string.Format("userName={0},counterNum={1},countyCode={2}", input.userName, input.counterNum,
                input.countyCode));
        }
        [Route("GET_VERSION")]
        [HttpPost]
        public ResultModel GET_VERSION([FromBody] BusinessModel param)
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
                Log.Info("GET_VERSION input param:" + param);
                //   var input = JsonConvert.DeserializeObject<BusinessModel>(param);
                //  var errDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(param);
                //  Log.Info("GET_VERSION input errDict:" + errDict);
               // /ar haha = string.Empty;
                //foreach(var a in Request.Properties)
                //{
                //    haha += a.Value+";";
                //}
                return new ResultModel { StatusCode = "000000", Result = "hehe",BussinessModel=param };
            }
            catch (Exception ex)
            {
                Log.Error("GET_VERSION", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
    }
}
