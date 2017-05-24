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
    public class IdentityInformationController : ApiController
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
        [Route("PhoneQueryById")]
        [HttpPost]
        public PhoneQueryResult PhoneQueryById([FromBody] IndentityInforQuery param)
        {
            try
            {
                if (param == null)
                {
                    return new PhoneQueryResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("PhoneQueryById input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(_db, param.UserName, "PhoneQueryById", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new CommonResult { StatusCode = "000007", Result = "没有权限" };
                //}


                var busi = _db.POPULATION.FirstOrDefault(q => q.IDNUM == param.IdentityCardNumber);
                if (busi == null)
                {
                    return new PhoneQueryResult { StatusCode = "000019", Result = "没有找到相关人员信息 ！" };

                }

                return new PhoneQueryResult { StatusCode = "000000", Result = "",Address=busi.ADDRESS,Name=busi.NAME,PhoneNumber =busi.MOBILE};

            }
            catch (Exception ex)
            {
                Log.Error("PhoneQueryById", ex);
                return new PhoneQueryResult { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("IdentityInforQuery")]
        [HttpPost]
        public CommonResult IdentityInforQuery([FromBody] IndentityInforQuery param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("IdentityInforQuery input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(_db, param.UserName, "IdentityInforQuery", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new CommonResult { StatusCode = "000007", Result = "没有权限" };
                //}


                var busi = _db.POPULATION.FirstOrDefault(q => q.IDNUM == param.IdentityCardNumber);
                if (busi == null)
                {
                    _db.POPULATION.Add(new POPULATION
                    {
                        ADDRESS = param.Address,
                        BORN = param.Birthday,
                        IDNUM = param.IdentityCardNumber,
                        MOBILE = param.PhoneNumber,
                        NAME = param.Name,
                        NATION = param.Nationality,
                    });
                }
                else
                {
                    busi.ADDRESS = param.Address;
                    busi.BORN = param.Birthday;
                    busi.MOBILE = param.PhoneNumber;
                    busi.NAME = param.Name;
                    busi.NATION = param.Nationality;
                }
                _db.SaveChanges();
                return new CommonResult { StatusCode = "000000", Result = "" };

            }
            catch (Exception ex)
            {
                Log.Error("IdentityInforQuery", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

        }
    }
}