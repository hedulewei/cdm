﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using CDMservers.Models;
using Common;
using DataService;
using log4net;

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
                    File.WriteAllBytes(filename, param.FileContent);
                    return new UploadPictureResult { StatusCode = "000000", Result = "ok",Id = param.Id, FileName = param.FileName  };
               
              
            }
            catch (Exception ex)
            {
                Log.Error("PostBusinessFormInfo", ex);
                return new UploadPictureResult { StatusCode = "000003", Result = ex.Message };
            }
        }
     
    }
}
