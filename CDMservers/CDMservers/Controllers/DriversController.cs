using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Common;
using DataService;
using log4net;

namespace CDMservers.Controllers
{
    public class DriversController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        [System.Web.Http.Route("generalChangeCertification")]
        [System.Web.Http.HttpPost]
        public ResultModel ccc([FromBody] BusinessModel param)
        {
            try
            {
                InputLog(param);
                var oo = new OracleOperation();
                var ret = oo.dueAndChangeCertification(param);

                return new ResultModel { statusCode = ret == 1 ? "000000" : "000004", bussinessModel = new BusinessModel { queueNum = oo.GetOrdinal(param).ToString() } };
            }
            catch (Exception ex)
            {
                Log.Error("ordinaryChangeCertification", ex);
                return new ResultModel { statusCode = "000003", result = ex.Message };
            }
        }
        [Route("dueAndChangeCertification")]
        [HttpPost]
        public ResultModel dueAndChangeCertification([FromBody] BusinessModel param)
        {
            try
            {
                InputLog(param);
                var oo = new OracleOperation();
                var ret = oo.dueAndChangeCertification(param);

                return new ResultModel { statusCode = ret==1?"000000":"000004", bussinessModel = new BusinessModel { queueNum = oo.GetOrdinal(param).ToString() } };
            }
            catch (Exception ex)
            {
                Log.Error("dueAndChangeCertification", ex);
                return new ResultModel { statusCode = "000003", result = ex.Message };
            }
        }
        
      
        private void InputLog(BusinessModel input)
        {
            Log.Info(string.Format("userName={0},counterNum={1},countyCode={2}", input.userName, input.counterNum,
                input.countyCode));
        }
    }
}
