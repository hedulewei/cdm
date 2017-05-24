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
    public class CorporationController : ApiController
    {
        private readonly Model1524 _db = new Model1524();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("PhoneQueryByCoInfor")]
        [HttpPost]
        public PhoneQueryResult PhoneQueryByCoInfor([FromBody] IndentityInforQuery param)
        {
            try
            {
                if (param == null)
                {
                    return new PhoneQueryResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("PhoneQueryByCoInfor input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(_db, param.UserName, "PhoneQueryByCoInfor", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new CommonResult { StatusCode = "000007", Result = "没有权限" };
                //}


                var busi = _db.CORPORATEINFO.FirstOrDefault(q => q.CODE == param.IdentityCardNumber);
                if (busi == null)
                {
                    return new PhoneQueryResult { StatusCode = "000020", Result = "没有找到相关公司信息 ！" };

                }

                return new PhoneQueryResult { StatusCode = "000000", Result = "", Address = busi.ADDRESS, Name = busi.NAME, PhoneNumber = busi.PHONENUMBER };

            }
            catch (Exception ex)
            {
                Log.Error("PhoneQueryByCoInfor", ex);
                return new PhoneQueryResult { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("CorporationInforQuery")]
        [HttpPost]
        public CommonResult CorporationInforQuery([FromBody] IndentityInforQuery param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("CorporationInforQuery input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(_db, param.UserName, "CorporationInforQuery", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new CommonResult { StatusCode = "000007", Result = "没有权限" };
                //}


                var busi = _db.CORPORATEINFO.FirstOrDefault(q => q.CODE == param.IdentityCardNumber);
                if (busi == null)
                {
                    _db.CORPORATEINFO.Add(new CORPORATEINFO
                    {
                        ADDRESS = param.Address,
                        PHONENUMBER = param.PhoneNumber,
                        NAME = param.Name,
                        CODE = param.IdentityCardNumber
                    });
                }
                else
                {
                    busi.ADDRESS = param.Address;
                    busi.PHONENUMBER = param.PhoneNumber;
                    busi.NAME = param.Name;
                }
                _db.SaveChanges();
                return new CommonResult { StatusCode = "000000", Result = "" };

            }
            catch (Exception ex)
            {
                Log.Error("CorporationInforQuery", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

        }
    }
}