using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class PaymentController : ApiController
    {
        private readonly Model15242 _db = new Model15242();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("Payment")]
        [HttpPost]
        public ResultModel Payment([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("Payment input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(_db, param.userName, "Payment", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}

                switch (param.countyCode)
                {
                    //case "changdao":


                    //    break;
                    //case "zhaoyuan":


                    //    break;
                    //case "penglai":


                    //    break;
                    //case "laizhou":


                    //    break;
                    //case "laiyang":


                    //    break;
                    //case "longkou":


                    //    break;
                    //case "muping":


                    //    break;
                    //case "laishan":


                    //    break;
                    //case "qixia":


                    //    break;
                    //case "fushan":


                    //    break;

                    //case "haiyang":

                    //    break;
                    case "zhifu":
                        var busi = _db.ZHIFUBUSINESS.FirstOrDefault(q => q.ID == param.ID);
                        if (busi == null)
                            return new ResultModel
                            {
                                StatusCode = "000009",
                                Result = "没有找到相关业务 ！"
                            };
                        busi.STATUS = (int)BusinessStatus.Paid;
                        busi.COMPLETE_PAY_USER = param.userName;
                        _db.SaveChanges();
                        return new ResultModel { StatusCode = "000000", Result = "" };
                        break;
                    default:

                        return new ResultModel { StatusCode = "000000", Result = "" };
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("Payment", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }
     
    }
}