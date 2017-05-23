using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class ArchiveController : ApiController
    {
        private Model1519 db = new Model1519();
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
                    case "zhifu":
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
                        break;
                    default:

                        return new CommonResult { StatusCode = "000000", Result = "" };
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("ArchivesAccept", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

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
                    case "zhifu":
                        foreach (int id in param.Ids)
                        {
                            var busi = db.ZHIFUBUSINESS.FirstOrDefault(q => q.ID == id);
                            if (busi == null)
                            {
                                return new CommonResult { StatusCode = "000015", Result = string.Format("撤销档案移交出错,没有找到id={0}的业务，{1}", id, param.CountyCode), };
                            }
                            busi.TRANSFER_STATUS = 0;
                        }
                        db.SaveChanges();
                        return new CommonResult { StatusCode = "000000", Result = "", };
                        break;
                    default:

                        return new CommonResult { StatusCode = "000000", Result = "" };
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("ArchivesWithdraw", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

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
                    case "zhifu":
                        foreach (int id in param.Ids)
                        {
                            var busi = db.ZHIFUBUSINESS.FirstOrDefault(q => q.ID == id);
                            if (busi == null)
                            {
                                return new CommonResult { StatusCode = "000015", Result = string.Format("档案移交出错,没有找到id={0}的业务，{1}",id,param.CountyCode), };
                            }
                            busi.TRANSFER_STATUS = 1;
                        }
                        db.SaveChanges();
                        return new CommonResult { StatusCode = "000000", Result = "", };
                        break;
                    default:

                        return new CommonResult { StatusCode = "000000", Result = "" };
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("ArchivesTransfer", ex);
                return new CommonResult { StatusCode = "000003", Result = ex.Message };
            }

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

                        var busi = db.USERS.Where(q => q.COUNTYCODE == param.countyCode);
                        var retlist = new List<DahcUser>();

                        foreach (USERS onebusi in busi)
                        {
                            var limit = JsonConvert.DeserializeObject<Dictionary<string,bool>>(onebusi.LIMIT);
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
                        break;
                    default:

                        return new UsersOfArchiveQueryResult { StatusCode = "000000", Result = "" };
                        break;
                }

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

                        var busi = db.ZHIFUBUSINESS.Where(q => q.TRANSFER_STATUS == 1 && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
                        if (!busi.Any())
                            return new BusinessListResult
                            {
                                StatusCode = "000009",
                                Result = "没有找到相关业务 ！"
                            };

                        //    var userlist = db.USERS.Where(q => q.COUNTYCODE == param.CountyCode);
                        var retlist = new List<BusinessModel>();

                        foreach (ZHIFUBUSINESS onebusi in busi)
                        {
                            retlist.Add(new BusinessModel
                            {
                                ID = (int)onebusi.ID,
                                processUser = onebusi.PROCESS_USER,
                                type = (int)onebusi.TYPE,
                                name = onebusi.NAME,
                                startTime = onebusi.START_TIME.ToString(),
                                endTime = onebusi.END_TIME.ToString(),
                                uploader = onebusi.UPLOADER,
                                status = (int)onebusi.STATUS,
                                queueNum = onebusi.QUEUE_NUM,
                                IDum = onebusi.ID_NUM,
                                address = onebusi.ADDRESS,
                                serialNum = onebusi.SERIAL_NUM,
                                fileRecvUser = onebusi.FILE_RECV_USER,
                            });
                        }
                        // return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "",Volumes = retlist};
                        return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
                        break;
                    default:

                        return new BusinessListResult { StatusCode = "000000", Result = "" };
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("ArchivedQuery", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }

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

                        var busi = db.ZHIFUBUSINESS.Where(q => q.TRANSFER_STATUS == 0 && q.START_TIME.CompareTo(startdate) >= 0 && q.END_TIME.CompareTo(endtime) <= 0);
                        if (!busi.Any())
                            return new BusinessListResult
                            {
                                StatusCode = "000009",
                                Result = "没有找到相关业务 ！"
                            };
                      
                     //    var userlist = db.USERS.Where(q => q.COUNTYCODE == param.CountyCode);
                        var retlist = new List<BusinessModel>();

                        foreach (ZHIFUBUSINESS onebusi in busi)
                        {
                            retlist.Add(new BusinessModel
                            {
                                ID=(int)onebusi.ID,
                                processUser = onebusi.PROCESS_USER,
                                type=(int)onebusi.TYPE,
                                name=onebusi.NAME,
                                startTime=onebusi.START_TIME.ToString(),
                                endTime = onebusi.END_TIME.ToString(),
                                uploader = onebusi.UPLOADER,
                                status=(int)onebusi.STATUS,
                                queueNum = onebusi.QUEUE_NUM,
                                IDum = onebusi.ID_NUM,
                                address = onebusi.ADDRESS,
                                serialNum =onebusi.SERIAL_NUM,
                                fileRecvUser = onebusi.FILE_RECV_USER,
                            });
                        }
                       // return new BusinessVolumeQueryResult { StatusCode = "000000", Result = "",Volumes = retlist};
                        return new BusinessListResult { StatusCode = "000000", Result = "", BussinessList = retlist };
                        break;
                    default:

                        return new BusinessListResult { StatusCode = "000000", Result = "" };
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("NotArchiveQuery", ex);
                return new BusinessListResult { StatusCode = "000003", Result = ex.Message };
            }

        }
     
    }
}
