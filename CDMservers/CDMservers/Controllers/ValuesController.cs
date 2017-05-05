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
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        [Route("businessprocess/{param}")]
        [HttpGet]
        public ResultModel BusinessProcess(string param)
        {
            var input = JsonConvert.DeserializeObject<BusinessModel>(param);
            return new ResultModel { StatusCode = "000000", Result = "ok" };
        }
        [Route("getordinal/{param}")]
        [HttpGet]
        public ResultModel GetOrdinal(string param)
        {
            try
            {
                Log.Info("GetOrdinal input param" + param);
                var input = JsonConvert.DeserializeObject<BusinessModel>(param);
                Log.Info("aaa");
                var oo = new OracleOperation();
                Log.Info("bbb");
                var ret = oo.GetOrdinal(input);
                Log.Info("ccc");
                return new ResultModel { StatusCode = "000000", Result = ret.ToString() };
            }
            catch (Exception ex)
            {
                Log.Error("GetOrdinal", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("getordinal2")]
        [HttpGet]
        public ResultModel GetOrdinal2(string code, string category)
        {
            try
            {
                Log.Info(string.Format("GetOrdinal input code={0},category={1}", code, category));
                //  var input = JsonConvert.DeserializeObject<OrdinalInput>(param);
                var input = new BusinessModel { countyCode = code, businessCategory = category };
                var oo = new OracleOperation();
                return new ResultModel { StatusCode = "000000", Result = oo.GetOrdinal(input).ToString() };
            }
            catch (Exception ex)
            {
                Log.Error("GetOrdinal", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }
        }
    }
}
