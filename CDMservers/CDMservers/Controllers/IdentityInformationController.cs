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
        [Route("AcquireIdentityInfor")]
        [HttpPost]
        public AcquireIdentityInforResult AcquireIdentityInfor([FromBody] AcquireIdentityInforRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new AcquireIdentityInforResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("AcquireIdentityInfor input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(_db, param.UserName, "AcquireIdentityInfor", JsonConvert.SerializeObject(param));

                var busi = _db.POPULATION.FirstOrDefault(q => q.IDNUM == param.IdentityCardNumber);
                if (busi == null)
                {
                    return new AcquireIdentityInforResult { StatusCode = "000019", Result = "没有找到相关人员信息 ！" };

                }
                var gender = Gender.Unknown;
                Enum.TryParse(busi.SEX, out gender);
                return new AcquireIdentityInforResult
                {
                    StatusCode = "000000",
                    Result = "",
                    Address = busi.ADDRESS, Name = busi.NAME,
                    Gender = gender,
                    Nationality = busi.NATION,
                    Birthday = busi.BORN,
                    Postcode = busi.POSTCODE,
                    PostAddress = busi.POSTADDRESS,
                    Mobile = busi.MOBILE,
                    Telephone = busi.TELEPHONE,
                    Email = busi.EMAIL,
                    IdentityCardNumber = busi.IDNUM,
                    FingerprintOne = busi.FIRSTFINGER,
                    FingerprintTwo = busi.SECONDFINGER,
                    IrisRight = busi.RIGHTEYE,
                    IrisLeft = busi.LEFTEYE,
                };

            }
            catch (Exception ex)
            {
                Log.Error("AcquireIdentityInfor", ex);
                return new AcquireIdentityInforResult { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("IdentityInforUpload")]
        [HttpPost]
        public CommonResult IdentityInforUpload([FromBody] IdentityInforUploadRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("IdentityInforUpload input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(_db, param.UserName, "IdentityInforUpload", JsonConvert.SerializeObject(param));
               
                var busi = _db.POPULATION.FirstOrDefault(q => q.IDNUM == param.IdentityCardNumber);
                if (busi == null)
                {
                    _db.POPULATION.Add(new POPULATION
                    {
                      
                        FIRSTFINGER = param.FingerprintOne,
                        IDNUM = param.IdentityCardNumber,
                        SECONDFINGER = param.FingerprintTwo,
                        LEFTEYE = param.IrisLeft,
                        RIGHTEYE = param.IrisRight,
                    });
                }
                else
                {
                   
                    busi.RIGHTEYE = param.IrisRight;
                    busi.LEFTEYE = param.IrisLeft;
                    busi.FIRSTFINGER = param.FingerprintOne;
                    busi.SECONDFINGER = param.FingerprintTwo;
                }
                _db.SaveChanges();
                return new CommonResult { StatusCode = "000000", Result = "" };

            }
            catch (Exception ex)
            {
                Log.Error("IdentityInforUpload", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

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