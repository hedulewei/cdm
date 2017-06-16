using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace study.Controllers
{

    // [Route("api/[controller]")]
    public class LearnerController : Controller
    {

        private readonly studyContext _db1 = new studyContext();
        static List<Ptoken> tokens = new List<Ptoken>();
        class Ptoken
        {
            public string Identity { get; set; }
            public string Token { get; set; }
        }
        // private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                _db1.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("GetToken")]
        [HttpGet]
        public GetTokenResponse GetToken(string identity)
        {
            try
            {
                if (string.IsNullOrEmpty(identity))
                {
                    return new GetTokenResponse { StatusCode = "100004", Description = "error identity" };
                }
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    return new GetTokenResponse { StatusCode = "100004", Description = "error identity" };
                }
                var toke1n = GetToken();
                var found = false;
                foreach (var a in tokens)
                {
                    if (a.Identity == identity)
                    {
                        a.Token = toke1n;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    tokens.Add(new Ptoken { Identity = identity, Token = toke1n });
                }

                return new GetTokenResponse
                {
                    Token = toke1n,
                    StatusCode = "100000",
                    Description = "ok",
                    DrivingLicenseType = string.IsNullOrEmpty(theuser.Licensetype) ? DrivingLicenseType.Unknown : (DrivingLicenseType)int.Parse(theuser.Licensetype),
                    Identity = theuser.Identity,
                    Name = theuser.Name,
                    Photo = string.IsNullOrEmpty(theuser.Photo) ? string.Empty : theuser.Photo,
                };
            }
            catch (Exception ex)
            {
                // Log.Error("GetLearnerInfo", ex);
                return new GetTokenResponse { StatusCode = "100003", Description = ex.Message };
            }

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
                var found = false;
                var identity = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Token == inputRequest.Token)
                    {
                        identity = a.Identity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return new CommonResponse { StatusCode = "100001", Description = "error token" };
                }
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    return new GetLearnerInfoResponse { StatusCode = "100004", Description = "error identity" };
                }
                theuser.Studycompletedate = DateTime.Now;
                theuser.Studylog += inputRequest.AllRecords + inputRequest.AllStatus;
                _db1.SaveChanges();
                return new CommonResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                };

            }
            catch (Exception ex)
            {
                //  Log.Error("CompleteCourses", ex);
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
                var found = false;
                var identity = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Token == inputRequest.Token)
                    {
                        identity = a.Identity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return new CommonResponse { StatusCode = "100001", Description = "error token" };
                }
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    return new GetLearnerInfoResponse { StatusCode = "100004", Description = "error identity" };
                }

                if (theuser.Studystartdate == null)
                {
                    theuser.Studystartdate = DateTime.Now;
                }
                Console.WriteLine("startdate,{0}", theuser.Studystartdate);
                theuser.Studylog += JsonConvert.SerializeObject(inputRequest);
                _db1.SaveChanges();
                return new CommonResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                };
            }
            catch (Exception ex)
            {
                //  Log.Error("PostStudyStatus", ex);
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
                var found = false;
                var identity = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Token ==token)
                    {
                        identity = a.Identity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return new GetLearnerInfoResponse { StatusCode = "100001", Description = "error token" };
                }
                 var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    return new GetLearnerInfoResponse { StatusCode = "100004", Description = "error identity" };
                }
                return new GetLearnerInfoResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                    DrivingLicenseType = string.IsNullOrEmpty(theuser.Licensetype) ? DrivingLicenseType.Unknown : (DrivingLicenseType)int.Parse(theuser.Licensetype),
                   
                    Identity = theuser.Identity,
                    Name = theuser.Name,
                    //  Photo = new byte[1]
                };
            }
            catch (Exception ex)
            {
                // Log.Error("GetLearnerInfo", ex);
                return new GetLearnerInfoResponse { StatusCode = "100003", Description = ex.Message };
            }

        }

        [Route("InspectCompleteCourses")]
        [HttpPost]
        public string InspectCompleteCourses([FromBody] CompleteCoursesRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    return JsonConvert.SerializeObject(new CommonResponse { StatusCode = "100002", Description = "请求错误，请检查请求参数！" });
                }

                return JsonConvert.SerializeObject(new CommonResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                });

            }
            catch (Exception ex)
            {
                //  Log.Error("CompleteCourses", ex);
                return JsonConvert.SerializeObject(new CommonResponse { StatusCode = "100003", Description = ex.Message }); ;
            }

        }
        [Route("InspectPostStudyStatus")]
        [HttpPost]
        public CommonResponse InspectPostStudyStatus([FromBody] StudyStatusRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    return new CommonResponse { StatusCode = "100002", Description = "请求错误，请检查请求参数！" };
                }
                //   Log.Info("PostStudyStatus input:" + JsonConvert.SerializeObject(inputRequest));

                return new CommonResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                };
            }
            catch (Exception ex)
            {
                //  Log.Error("PostStudyStatus", ex);
                return new CommonResponse { StatusCode = "100003", Description = ex.Message };
            }

        }
        [Route("InspectGetLearnerInfo")]
        [HttpGet]
        public GetLearnerInfoResponse InspectGetLearnerInfo(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return new GetLearnerInfoResponse { StatusCode = "100001", Description = "error token" };
                }
                var toke1n = GetToken();
                return new GetLearnerInfoResponse
                {
                    StatusCode = "100000",
                    Description = "ok",
                    DrivingLicenseType = DrivingLicenseType.A,
                    Identity = "id",
                    Name = "name",
                    ///  Photo = new byte[1]
                };
            }
            catch (Exception ex)
            {
                // Log.Error("GetLearnerInfo", ex);
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