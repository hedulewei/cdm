using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using CDMservers.Models;
using Common;
using log4net;
using Newtonsoft.Json;

namespace CDMservers.Controllers
{
    public class FilesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserDbc _dbUserDbc = new UserDbc();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbUserDbc.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("UploadPicture")]
        [HttpPost]
        public UploadPictureResult UploadPicture([FromBody] UploadPicture param)
        {
            try
            {
                if (param == null)
                {
                    return new UploadPictureResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                
                if (!PermissionCheck.CheckLevelPermission(param, _dbUserDbc))
                {
                    return new UploadPictureResult { StatusCode = "000007", Result = "没有权限" };
                }
               
                var currentdate = DateTime.Now.Date;
                var scurrentdate = string.Format("{0}-{1}-{2}", currentdate.Year, currentdate.Month, currentdate.Day);
               
                    var filepath = string.Format("{2}{0}\\{1}\\{3}", param.CountyCode, scurrentdate, CdmConfiguration.FileRootPath, param.Id);

                    if (!Directory.Exists(@filepath))
                    {
                      //  Log.Info("path=" + filepath);
                        Directory.CreateDirectory(@filepath);
                    }
                    var filename = string.Format("{0}\\{1}", filepath, param.FileName);
                 //   Log.Info("file name=" + filename);
                //base64
                    File.WriteAllBytes(filename, param.FileContent);
                 //   File.WriteAllBytes(filename, Convert.FromBase64String(param.FileContent));
                    return new UploadPictureResult { StatusCode = "000000", Result = "ok",Id = param.Id, FileName = param.FileName  };
               
              
            }
            catch (Exception ex)
            {
                Log.InfoFormat("UploadPicture :{0}.", JsonConvert.SerializeObject(param));
                Log.Error("UploadPicture", ex);
                return new UploadPictureResult { StatusCode = "000003", Result = ex.Message };
            }
        }
        [Route("SoftwareUpdate")]
        [HttpPost]
        public SoftwareUpdateResult SoftwareUpdate([FromBody] SoftwareUpdateRequest param)
        {
            try
            {
                if (param == null)
                {
                    return new SoftwareUpdateResult { StatusCode = "000003", Result = "请求错误，请检查输入参数！" };
                }
                var version =long.Parse( param.Version.Replace(".", ""));
                var updpath =new DirectoryInfo(CdmConfiguration.SoftwareUpdatePath).GetFiles("*.zip");
                var ret = new SoftwareUpdateResult
                {
                    NewVersionFileName = string.Empty,
                    StatusCode = "000000"
                };
                foreach (FileInfo fileInfo in updpath)
                {
                        var tmp = fileInfo.Name.Replace(".", "");
                        var reg = new Regex(@"\d+");
                        var m = reg.Match(tmp).ToString();
                        if (long.Parse(m) > version)
                        {
                            ret.NewVersionFileName = fileInfo.Name;
                            ret.FileContent = File.ReadAllBytes(fileInfo.FullName);
                            break;
                        }
                    
                }
               
                return ret;


            }
            catch (Exception ex)
            {
                Log.InfoFormat("SoftwareUpdate :{0}.", JsonConvert.SerializeObject(param));
                Log.Error("SoftwareUpdate", ex);
                return new SoftwareUpdateResult { StatusCode = "000003", Result = ex.Message };
            }
        }
    }
}
