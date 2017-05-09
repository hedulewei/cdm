using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using CDMservers.Models;
using Common;
using DataService;
using log4net;

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
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
              //  if (!PermissionCheck.Check(param))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                InputLog(param);
                var oo = new OracleOperation();
                var ret = oo.dueAndChangeCertification(param);

                return new ResultModel { StatusCode = ret == 1 ? "000000" : "000004", BussinessModel = new BusinessModel { queueNum = oo.GetOrdinal(param).ToString() } };
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
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
               // if (!PermissionCheck.Check(param))
                {
                    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                }
                InputLog(param);
                var oo = new OracleOperation();
                var ret = oo.dueAndChangeCertification(param);

                return new ResultModel { StatusCode = ret==1?"000000":"000004", BussinessModel = new BusinessModel { queueNum = oo.GetOrdinal(param).ToString() } };
            }
            catch (Exception ex)
            {
                Log.Error("dueAndChangeCertification", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
        
      
        private void InputLog(BusinessModel input)
        {
            Log.Info(string.Format("userName={0},counterNum={1},countyCode={2}", input.userName, input.counterNum,
                input.countyCode));
        }
    }
}
