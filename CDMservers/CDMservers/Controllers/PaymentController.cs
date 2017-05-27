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
        private readonly Model1525 _db = new Model1525();
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

                switch (param.countyCode.ToLower())
                {
                    case "changdao": return ChangdaoPayment(param);
                    case "zhaoyuan": return ZhaoyuanPayment(param);
                    case "penglai": return PenglaiPayment(param);
                    case "laizhou": return LaizhouPayment(param);
                    case "laiyang": return LaiyangPayment(param);

                    case "longkou": return LongkouPayment(param);
                    case "muping": return MupingPayment(param);
                    case "laishan": return LaishanPayment(param);
                    case "qixia": return QixiaPayment(param);
                    case "fushan": return FushanPayment(param);

                    case "haiyang": return HaiyangPayment(param);
                    case "zhifu":
                    case "shisuo":
                    case "dacheng":
                        return ZhifuPayment(param);
                       
                    default:
                        return new ResultModel { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
                }

            }
            catch (Exception ex)
            {
                Log.Error("Payment", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }
        private ResultModel ChangdaoPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_CHANGDAO.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel ZhaoyuanPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_ZHAOYUAN.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel PenglaiPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_PENGLAI.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel LaizhouPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_LAIZHOU.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel LaiyangPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_LAIYANG.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel LongkouPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_LONGKOU.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel MupingPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_MUPING.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel LaishanPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_LAISHAN.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel QixiaPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_QIXIA.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel FushanPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_FUSHAN.FirstOrDefault(q => q.ID == param.ID);
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
        }

        private ResultModel HaiyangPayment(BusinessModel param)
        {
            var busi = _db.BUSINESS_HAIYANG.FirstOrDefault(q => q.ID == param.ID);
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
        }
     
        private ResultModel ZhifuPayment(BusinessModel param)
        {
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
        }
     
    }
}