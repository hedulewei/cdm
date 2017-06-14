using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using log4net;
using Newtonsoft.Json;
using StudyModel;
using StudyServer.Models;

namespace StudyServer.Controllers
{
    public class LearnerController : ApiController
    {
        private readonly Model1 _db = new Model1();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public LearnerController()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("CompleteCourses")]
        [HttpPost]
        public CommonResponse CompleteCourses([FromBody] CompleteCoursesRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    return new CommonResponse { StatusCode = "100002", Description = "请求错误，请检查请求参数！" };
                }
                Log.Info("CompleteCourses input:" + JsonConvert.SerializeObject(inputRequest));

                return new CommonResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                };
            }
            catch (Exception ex)
            {
                Log.Error("CompleteCourses", ex);
                return new CommonResponse { StatusCode = "100003", Description = ex.Message };
            }

        }
        [Route("PostStudyStatus")]
        [HttpPost]
        public CommonResponse PostStudyStatus([FromBody] StudyStatusRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    return new CommonResponse { StatusCode = "100002", Description = "请求错误，请检查请求参数！" };
                }
                Log.Info("PostStudyStatus input:" + JsonConvert.SerializeObject(inputRequest));

                return new CommonResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                };
            }
            catch (Exception ex)
            {
                Log.Error("PostStudyStatus", ex);
                return new CommonResponse { StatusCode = "100003", Description = ex.Message };
            }

        }
        [Route("GetLearnerInfo")]
        [HttpGet]
        public GetLearnerInfoResponse GetLearnerInfo(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return new GetLearnerInfoResponse { StatusCode = "100001", Description = "error token" };
                }
                var toke1n = GetToken();
                Log.InfoFormat("input token is {0},random token is {1}.",token,toke1n);
                return new GetLearnerInfoResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                    DrivingLicenseType = DrivingLicenseType.A,
                    Identity = "id",
                    Name = "name",
                    Photo = new byte[1]
                };
            }
            catch (Exception ex)
            {
                Log.Error("GetLearnerInfo", ex);
                return new GetLearnerInfoResponse { StatusCode = "100003", Description = ex.Message };
            }

        }

        private string GetToken()
        {
            var seed = Guid.NewGuid().ToString("N");
            //var token = Convert.ToBase64String(Guid.ToByteArray()).TrimEnd('=');
            return seed;
        }
    }
}
