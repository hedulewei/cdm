using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
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
    public class TabulationController : ApiController
    {
        private Model15242 db = new Model15242();

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("TabulationQuery")]
        [HttpPost]
        public ResultModel TabulationQuery([FromBody] TabulationQuery param)
        {
            try
            {
                if (param == null)
                {
                    return new ResultModel { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                Log.Info("TabulationQuery input:" + JsonConvert.SerializeObject(param));
                LogIntoDb.Log(db, param.UserName, "TabulationQuery", JsonConvert.SerializeObject(param));
                //if (!PermissionCheck.CheckLevelPermission(param, _dbuUserDbc))
                //{
                //    return new ResultModel { StatusCode = "000007", Result = "没有权限" };
                //}

                switch (param.CountyCode)
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
                    case CountyCode.ZhiFu:
                        var busi = db.ZHIFUBUSINESS.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal );
                            if (busi == null)
                            {
                                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}",  param.TabulationOrdinal), };
                            }
                            busi.TRANSFER_STATUS = 0;
                      
                        return new ResultModel { StatusCode = "000000", Result = "",BussinessModel = new BusinessModel
                        {
                            ID = (int)busi.ID,
                            processUser = busi.PROCESS_USER,
                            type = (int)busi.TYPE,
                            name = busi.NAME,
                            startTime = busi.START_TIME.ToString(),
                            endTime = busi.END_TIME.ToString(),
                            uploader = busi.UPLOADER,
                            status = (int)busi.STATUS,
                            queueNum = busi.QUEUE_NUM,
                            IDum = busi.ID_NUM,
                            address = busi.ADDRESS,
                            serialNum = busi.SERIAL_NUM,
                            fileRecvUser = busi.FILE_RECV_USER,
                        }};
                        break;
                    default:

                        return new ResultModel { StatusCode = "000016", Result = "没有该县区标识" + param.CountyCode };
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("TabulationQuery", ex);
                return new ResultModel { StatusCode = "000003", Result = ex.Message };
            }

        }
        [Route("GetTabulationOrdinal")]
        [HttpPost]
        public CommonResult GetTabulationOrdinal([FromBody] BasisRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                var today = DateTime.Now;
                return new CommonResult { StatusCode = "000000", Result = string.Format("{0}{1}{2}{3}{4}",((int)param.CountyCode).ToString("D2"),
                    today.Year, today.Month.ToString("D2"), today.Day.ToString("D2"),
                    InternalService.GetTabulationOrdinal(param).ToString("D4")) };
            }
            catch (Exception ex)
            {
                Log.Error("GetTabulationOrdinal", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }
        }
        [Route("VoiceCall")]
        [HttpPost]
        public async Task<CommonResult> VoiceCall([FromBody] VoiceCallRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new CommonResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                await Task.Run(async () =>
                {
                     await MessagePush.PushVoiceMessage(new CdmMessage
                        {
                            ClientType = ClientType.Voice,
                            Content = param.VoiceContent,
                            CountyCode = param.CountyCode,
                            VoiceType = VoiceType.PlayOver
                        });
                      
                    
                  
                  
                });
                return new CommonResult
                {
                    StatusCode = "000000",
                };
            }
            catch (Exception ex)
            {
                Log.Error("VoiceCall", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }
        }
    }
}