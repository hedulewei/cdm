using System.Data.Entity.Validation;
using System.Reflection;
using System.Web.UI.WebControls;
using CDMservers.Models;
using Common;
using DataService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

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
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
               // if (!PermissionCheck.Check(param))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                var oo = new OracleOperation();
                Log.Info("RetrieveCorporateInfo 111");
                var a = oo.RetrieveCorporateInfo(param);
                Log.Info("RetrieveCorporateInfo 222");
                return new ResultModel { StatusCode = a.name == string.Empty ? "000000" : "000013", BussinessModel = a };
            }
            catch (Exception ex)
            {
                Log.Error("RetrieveCorporateInfo", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        [Route("SendCorporateInfo")]
        [HttpPost]
        public ResultModel SendCorporateInfo([FromBody] BusinessModel param)
        {
            try
            {
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
               // if (!PermissionCheck.Check(param))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                var oo = new OracleOperation();
                Log.Info("SendCorporateInfo 111");
                var a = oo.SendCorporateInfo(param);
                Log.Info("SendCorporateInfo 222");
                return new ResultModel { StatusCode = a == 1 ? "000000" : "000012", BussinessModel = new BusinessModel() };
            }
            catch (Exception ex)
            {
                Log.Error("SendCorporateInfo", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        [Route("RetrieveCellPhoneNumber")]
        [HttpPost]
        public ResultModel RetrieveCellPhoneNumber([FromBody] BusinessModel param)
        {
            try
            {
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
              //  if (!PermissionCheck.Check(param))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                var oo = new OracleOperation();
                Log.Info("RetrieveCellPhoneNumber 111");
                var a = oo.RetrieveCellPhoneNumber(param);
                Log.Info("RetrieveCellPhoneNumber 222");
                return new ResultModel { StatusCode = a == string.Empty ? "000000" : "000011", BussinessModel = new BusinessModel{phoneNum=a} };
            }
            catch (Exception ex)
            {
                Log.Error("RetrieveCellPhoneNumber", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        [Route("SendIdentityCardInfo")]
        [HttpPost]
        public ResultModel SendIdentityCardInfo([FromBody] BusinessModel param)
        {
            try
            {
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
              //  if (!PermissionCheck.Check(param))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                var oo = new OracleOperation();
                Log.Info("SendIdentityCardInfo 111");
                var a = oo.SendIdentityCardInfo(param);
                Log.Info("SendIdentityCardInfo 222");
                return new ResultModel { StatusCode = a==1?"000000":"000010", BussinessModel = new BusinessModel () };
            }
            catch (Exception ex)
            {
                Log.Error("SendIdentityCardInfo", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        [Route("GET_QUEUE_NUM")]
        [HttpPost]
        public ResultModel GET_QUEUE_NUM([FromBody] BusinessModel param)
        {
            try
            {
               // if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
               //// if (!PermissionCheck.Check(param))
               // {
               //     return new ResultModel { StatusCode = "000007", Result = "没有权限" };
               // }
                InputLog(param);
                var oo = new OracleOperation();
                return new ResultModel { StatusCode = "000000", BussinessModel = new BusinessModel { queueNum = oo.GetOrdinal(param).ToString() } };
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
