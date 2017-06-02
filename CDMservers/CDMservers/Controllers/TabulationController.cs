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
              
                switch (param.CountyCode)
                {
                    case CountyCode.LaiShan: return Laishan_TabulationQuery(param);
                    case CountyCode.LaiYang: return LaiyangTabulationQuery(param);
                    case CountyCode.LaiZhou: return LaizhouTabulationQuery(param);
                    case CountyCode.LongKou: return LongkouTabulationQuery(param);
                    case CountyCode.PengLai: return PenglaiTabulationQuery(param);

                    case CountyCode.FuShan: return FushanTabulationQuery(param);
                    case CountyCode.QiXia: return QixiaTabulationQuery(param);
                    case CountyCode.HaiYang: return HaiyangTabulationQuery(param);
                    case CountyCode.MuPing: return MupingTabulationQuery(param);
                    case CountyCode.ChangDao: return ChangdaoTabulationQuery(param);

                    case CountyCode.ZhaoYuan: return ZhaoyuanTabulationQuery(param);

                    case CountyCode.ZhiFu:
                    case CountyCode.DaCheng: 
                    case CountyCode.ShiSuo:
                        return ZhifuTabulationQuery( param);
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

      
        private ResultModel Laishan_TabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_LAISHAN.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel LaiyangTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_LAIYANG.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel LaizhouTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_LAIZHOU.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel LongkouTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_LONGKOU.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel PenglaiTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_PENGLAI.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel FushanTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_FUSHAN.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel QixiaTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_QIXIA.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel HaiyangTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_HAIYANG.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel MupingTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_MUPING.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel ChangdaoTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_CHANGDAO.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel ZhaoyuanTabulationQuery(TabulationQuery param)
        {
            var busi = db.BUSINESS_ZHAOYUAN.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }

            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
        }
        private ResultModel ZhifuTabulationQuery( TabulationQuery param)
        {
            var busi = db.ZHIFUBUSINESS.FirstOrDefault(q => q.UNLOAD_TASK_NUM == param.TabulationOrdinal && q.STATUS == 9);
            if (busi == null)
            {
                return new ResultModel { StatusCode = "000009", Result = string.Format("没有找到相关业务 ！{0}", param.TabulationOrdinal), };
            }
            
            return new ResultModel
            {
                StatusCode = "000000",
                Result = "",
                BussinessModel = new BusinessModel
                {
                    ID = (int)busi.ID,
                    processUser = busi.PROCESS_USER,
                    type = (int)busi.TYPE,
                    name = busi.NAME,
                    startTime = busi.START_TIME.ToString(CultureInfo.InvariantCulture),
                    endTime = busi.END_TIME.ToString(CultureInfo.InvariantCulture),
                    uploader = busi.UPLOADER,
                    status = (int)busi.STATUS,
                    queueNum = busi.QUEUE_NUM,
                    IDum = busi.ID_NUM,
                    address = busi.ADDRESS,
                    serialNum = busi.SERIAL_NUM,
                    fileRecvUser = busi.FILE_RECV_USER,
                    texNum = busi.TAX_NUM,
                    texType = busi.TAX_TYPE,
                    originNum = busi.ORIGIN_NUM,
                    originType = busi.ORIGIN_TYPE,
                    postAddr = busi.POSTADDR,
                    postPhone = busi.POSTPHONE,
                    carNum = busi.CAR_NUM,
                    phoneNum = busi.PHONE_NUM,
                }
            };
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
                return new CommonResult
                {
                    StatusCode = "000000",
                    Result = string.Format("{0}{1}{2}{3}{4}", ((int)param.CountyCode).ToString("D2"),
                        today.Year, today.Month.ToString("D2"), today.Day.ToString("D2"),
                        InternalService.GetTabulationOrdinal(param).ToString("D4"))
                };
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