using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.UI;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class ArchiveController : ApiController
    {
        private Model1525 db = new Model1525();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("ArchivesAccept")]
        [HttpPost]
        public CommonResult ArchivesAccept([FromBody] ArchivesTransferRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("ArchivesAccept input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(db, param.UserName, "ArchivesAccept", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}

                switch (param.CountyCode.ToLower())
                {
                    case "changdao": return ChangdaoArchiveAccept(param);
                    case "zhaoyuan": return ZhaoyuanArchiveAccept(param);
                    case "penglai": return PenglaiArchiveAccept(param);
                    case "laizhou": return LaizhouArchiveAccept(param);
                    case "laiyang": return LaiyangArchiveAccept(param);

                    case "longkou": return LongkouArchiveAccept(param);
                    case "muping": return MupingArchiveAccept(param);
                    case "laishan": return LaishanArchiveAccept(param);
                    case "qixia": return QixiaArchiveAccept(param);
                    case "fushan": return FushanArchiveAccept(param);

                    case "haiyang": return HaiyangArchiveAccept(param);
                    case "zhifu":
                    case "shisuo":
                    case "dacheng":
                        return ZhifuArchiveAccept(param);
                      
                    default:
                        return new CommonResult { StatusCode = "000016", Result = "没有该县区标识" + param.CountyCode };
                }

            }
            catch (Exception ex)
            {
                Log.Error("ArchivesAccept", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

        }

        private CommonResult ChangdaoArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_CHANGDAO.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult ZhaoyuanArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_ZHAOYUAN.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult PenglaiArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_PENGLAI.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaizhouArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAIZHOU.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaiyangArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAIYANG.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LongkouArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LONGKOU.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult MupingArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_MUPING.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaishanArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAISHAN.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult QixiaArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_QIXIA.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult FushanArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_FUSHAN.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult HaiyangArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_HAIYANG.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult ZhifuArchiveAccept(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.ZHIFUBUSINESS.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {

                    return new CommonResult { StatusCode = "000015", Result = string.Format("接受档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 2;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        [Route("ArchivesWithdraw")]
        [HttpPost]
        public CommonResult ArchivesWithdraw([FromBody] ArchivesTransferRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("ArchivesWithdraw input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(db, param.UserName, "ArchivesWithdraw", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}

                switch (param.CountyCode.ToLower())
                {
                    case "changdao": return ChangdaoArchiveWithDraw(param);
                    case "zhaoyuan": return ZhaoyuanArchiveWithDraw(param);
                    case "penglai": return PenglaiArchiveWithDraw(param);
                    case "laizhou": return LaizhouArchiveWithDraw(param);
                    case "laiyang": return LaiyangArchiveWithDraw(param);

                    case "longkou": return LongkouArchiveWithDraw(param);
                    case "muping": return MupingArchiveWithDraw(param);
                    case "laishan": return LaishanArchiveWithDraw(param);
                    case "qixia": return QixiaArchiveWithDraw(param);
                    case "fushan": return FushanArchiveWithDraw(param);

                    case "haiyang": return HaiyangArchiveWithDraw(param);
                    case "zhifu":
                    case "shisuo":
                    case "dacheng":
                       return ZhifuArchiveWithDraw(param);
                       
                    default:

                        return new CommonResult { StatusCode = "000016", Result = "没有该县区标识"+param.CountyCode };
                }

            }
            catch (Exception ex)
            {
                Log.Error("ArchivesWithdraw", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

        }
        private CommonResult ChangdaoArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_CHANGDAO.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult ZhaoyuanArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_ZHAOYUAN.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult PenglaiArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_PENGLAI.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaizhouArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAIZHOU.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaiyangArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAIYANG.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }

        private CommonResult LongkouArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LONGKOU.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult MupingArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_MUPING.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaishanArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAISHAN.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult QixiaArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_QIXIA.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult FushanArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_FUSHAN.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }

        private CommonResult HaiyangArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_HAIYANG.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
      
        private CommonResult ZhifuArchiveWithDraw(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.ZHIFUBUSINESS.FirstOrDefault(q => q.ID == id && q.TRANSFER_STATUS == 1);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务或该业务档案标识不为1，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 0;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        [Route("ArchivesTransfer")]
        [HttpPost]
        public CommonResult ArchivesTransfer([FromBody] ArchivesTransferRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("ArchivesTransfer input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(db, param.UserName, "ArchivesTransfer", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}

                switch (param.CountyCode.ToLower())
                {
                    case "changdao": return ChangdaoArchiveTransfer(param);
                    case "zhaoyuan": return ZhaoyuanArchiveTransfer(param);
                    case "penglai": return PenglaiArchiveTransfer(param);
                    case "laizhou": return LaizhouArchiveTransfer(param);
                    case "laiyang": return LaiyangArchiveTransfer(param);

                    case "longkou": return LongkouArchiveTransfer(param);
                    case "muping": return MupingArchiveTransfer(param);
                    case "laishan": return LaishanArchiveTransfer(param);
                    case "qixia": return QixiaArchiveTransfer(param);
                    case "fushan": return FushanArchiveTransfer(param);

                    case "haiyang": return HaiyangArchiveTransfer(param);
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        return ZhifuArchiveTransfer(param);
                    default:

                        return new CommonResult { StatusCode = "000016", Result = "没有该县区标识" + param.CountyCode };
                }

            }
            catch (Exception ex)
            {
                Log.Error("ArchivesTransfer", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

        }

        private CommonResult ZhaoyuanArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_ZHAOYUAN.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult ChangdaoArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_CHANGDAO.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult PenglaiArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_PENGLAI.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaizhouArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAIZHOU.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaiyangArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAIYANG.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LongkouArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LONGKOU.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult MupingArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_MUPING.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult LaishanArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_LAISHAN.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult QixiaArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_QIXIA.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult FushanArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_FUSHAN.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult HaiyangArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.BUSINESS_HAIYANG.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        private CommonResult ZhifuArchiveTransfer(ArchivesTransferRequest param)
        {
            foreach (int id in param.Ids)
            {
                var busi = db.ZHIFUBUSINESS.FirstOrDefault(q => q.ID == id);
                if (busi == null)
                {
                    return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                }
                busi.TRANSFER_STATUS = 1;
                busi.FILE_RECV_USER = param.UserName;
            }
            db.SaveChanges();
            return new CommonResult { StatusCode = "000000", Result = "", };
        }
        [Route("UsersOfArchiveQuery")]
        [HttpPost]
        public UsersOfArchiveQueryResult UsersOfArchiveQuery([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new UsersOfArchiveQueryResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("UsersOfArchiveQuery input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(db, param.userName, "UsersOfArchiveQuery", JsonConvert.SerializeObject(param));
               
                        var busi = db.USERS.Where(q => q.COUNTYCODE == param.countyCode);
                        var retlist = new List<DahcUser>();

                        foreach (USERS onebusi in busi)
                        {
                            var limit = JsonConvert.DeserializeObject<Dictionary<string, bool>>(onebusi.LIMIT);
                            if (limit.ContainsKey("dahc") && limit["dahc"])
                            {
                                retlist.Add(new DahcUser
                                {
                                    UserName = onebusi.USERNAME,
                                    Id = (int)onebusi.ID,
                                });
                            }

                        }
                        // return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "",Volumes = retlist};
                        return new UsersOfArchiveQueryResult { StatusCode = "000000", Result = "", DahcUsers = retlist };
               

            }
            catch (Exception ex)
            {
                Log.Error("UsersOfArchiveQuery", ex);
                return new UsersOfArchiveQueryResult { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("ArchivedQuery")]
        [HttpPost]
        public BusinessListResult ArchivedQuery([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new BusinessListResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("ArchivedQuery input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(db, param.userName, "ArchivedQuery", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}
                var startdate = DateTime.Parse(param.startTime);
                var endtime = DateTime.Parse(param.endTime);
                var businesscategory = param.businessCategory.ToString(CultureInfo.InvariantCulture);

                switch (param.countyCode.ToLower())
                {
                    case "changdao": return ChangdaoArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "zhaoyuan": return ZhaoyuanArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "penglai": return PenglaiArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "laizhou": return LaizhouArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "laiyang": return LaiyangArchiveTransfer(param, businesscategory, startdate, endtime);

                    case "longkou": return LongkouArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "muping": return MupingArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "laishan": return LaishanArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "qixia": return QixiaArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "fushan": return FushanArchiveTransfer(param, businesscategory, startdate, endtime);

                    case "haiyang": return HaiyangArchiveTransfer(param, businesscategory, startdate, endtime);
                    case "shisuo":
                    case "dacheng":
                    case "zhifu":
                        return ZhifuArchivedQuery(param, businesscategory, startdate, endtime);
                      
                    default:
                        return new BusinessListResult { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
                }

            }
            catch (Exception ex)
            {
                Log.Error("ArchivedQuery", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }

        }

        private BusinessListResult ChangdaoArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_CHANGDAO.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };

           
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }
           
            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult ZhaoyuanArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_ZHAOYUAN.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult PenglaiArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_PENGLAI.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult LaizhouArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_LAIZHOU.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult LaiyangArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_LAIYANG.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult LongkouArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_LONGKOU.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult MupingArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_MUPING.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult LaishanArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_LAISHAN.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult QixiaArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_QIXIA.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult FushanArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_FUSHAN.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult HaiyangArchiveTransfer(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_HAIYANG.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult ZhifuArchivedQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.ZHIFUBUSINESS.Where(q => q.TRANSFER_STATUS == 1 &&
                             q.UPLOADER == param.userName &&
                           q.QUEUE_NUM.StartsWith(businesscategory) &&
                           q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };


            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    countyCode = onebusi.COUNTYCODE,
                    address = onebusi.ADDRESS,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        [Route("NotArchiveQuery")]
        [HttpPost]
        public BusinessListResult NotArchiveQuery([FromBody] BusinessModel param)
        {
            try
            {
                if (param == null)
                {
                    return new BusinessListResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }

                Log.Info("NotArchiveQuery input:" + JsonConvert.SerializeObject(param));

                LogIntoDb.Log(db, param.userName, "NotArchiveQuery", JsonConvert.SerializeObject(param));

                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}

                var startdate = DateTime.Parse(param.startTime);
                var endtime = DateTime.Parse(param.endTime);
                var businesscategory = param.businessCategory.ToString(CultureInfo.InvariantCulture);
                switch (param.countyCode.ToLower())
                {
                    case "changdao": return ChangdaoNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "zhaoyuan": return ZhaoyuanNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "penglai": return PenglaiNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "laizhou": return LaizhouNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "laiyang": return LaiyangNotArchiveQuery(param, businesscategory, startdate, endtime);

                    case "longkou": return LongkouNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "muping": return MupingNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "laishan": return LaishanNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "qixia": return QixiaNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "fushan": return FushanNotArchiveQuery(param, businesscategory, startdate, endtime);

                    case "haiyang": return HaiyangNotArchiveQuery(param, businesscategory, startdate, endtime);
                    case "shisuo":
                    case "dacheng":
                    case "zhifu": return ZhifuNotArchiveQuery(param, businesscategory, startdate, endtime);
                      
                    default:
                        return new BusinessListResult { StatusCode = "000016", Result = "没有该县区标识" + param.countyCode };
                }
            }
            catch (Exception ex)
            {
                Log.Error("NotArchiveQuery", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }
        }

        private BusinessListResult ChangdaoNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_CHANGDAO.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }
           

            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult ZhaoyuanNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_ZHAOYUAN.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult PenglaiNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_PENGLAI.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult LaizhouNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_LAIZHOU.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult LaiyangNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_LAIYANG.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult LongkouNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_LONGKOU.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult MupingNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_MUPING.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult LaishanNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_LAISHAN.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult QixiaNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_QIXIA.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult FushanNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_FUSHAN.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult HaiyangNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.BUSINESS_HAIYANG.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }
        private BusinessListResult ZhifuNotArchiveQuery(BusinessModel param, string businesscategory, DateTime startdate, DateTime endtime)
        {
            var busi = db.ZHIFUBUSINESS.Where(q => q.TRANSFER_STATUS == 0 &&
                          q.UPLOADER == param.userName &&
                          (q.STATUS == 5 || q.STATUS == 7 || q.STATUS == 8) &&
                          q.QUEUE_NUM.StartsWith(businesscategory) &&
                          q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
            if (!busi.Any())
                return new BusinessListResult
                {
                    StatusCode = "000009",
                    Result = "没有找到相关业务 ！"
                };
            var retlist = new List<BusinessModel>();

            foreach (var onebusi in busi)
            {
                retlist.Add(new BusinessModel
                {
                    ID = (int)onebusi.ID,
                    processUser = onebusi.PROCESS_USER,
                    type = (int)onebusi.TYPE,
                    name = onebusi.NAME,
                    startTime = onebusi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = onebusi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = onebusi.UPLOADER,
                    status = (int)onebusi.STATUS,
                    queueNum = onebusi.QUEUE_NUM,
                    IDum = onebusi.ID_NUM,
                    address = onebusi.ADDRESS,
                    countyCode = onebusi.COUNTYCODE,
                    serialNum = onebusi.SERIAL_NUM,
                    fileRecvUser = onebusi.FILE_RECV_USER,
                });
            }


            return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
        }

    }
}
