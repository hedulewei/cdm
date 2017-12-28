
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace study.Controllers
{
    public class InspectController : Controller
    {

        private readonly studyContext _db1 = new studyContext();
        static List<Ptoken> tokens = new List<Ptoken>();
        class Ptoken
        {
            public string Identity { get; set; }
            public string Token { get; set; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
          [Route("LoginAndQuery")]
        [HttpPost]
        public LoginAndQueryResponse LoginAndQuery([FromBody] LoginAndQueryRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    return new LoginAndQueryResponse { StatusCode = "100002", Description = "请求错误，请检查请求参数！" };

                }
                var identity = inputRequest.Identity;
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    return new LoginAndQueryResponse { StatusCode = "100004", Description = "error identity" };
                }
                theuser.Name = inputRequest.Name;
                theuser.Licensetype = ((int)inputRequest.DrivingLicenseType).ToString();
                theuser.Phone = inputRequest.Phone;
                theuser.Wechat = inputRequest.Wechat;
                _db1.SaveChanges();

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
                var allow = theuser.Drugrelated == null ? true : false;
                var allstatus = string.Empty;
                if (allow)
                {
                    allstatus = theuser.Studylog;
                }
                else allstatus = "drugreleated";
                return new LoginAndQueryResponse
                {
                    Token = toke1n,
                    StatusCode = "100000",
                    Description = "ok",
                    AllowedToStudy = allow,
                    AllStatus = allstatus,
                    //   Photo = string.IsNullOrEmpty(theuser.Photo) ? string.Empty : theuser.Photo,
                };
            }
            catch (Exception ex)
            {
                return new LoginAndQueryResponse { StatusCode = "100003", Description = ex.Message };
            }

        }
        [Route("LogSignature")]
        [HttpPost]
        public CommonResponse LogSignature([FromBody] LogSignatureRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    return new LoginAndQueryResponse { StatusCode = "100002", Description = "请求错误，请检查请求参数！" };

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
                // var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                // if (theuser == null)
                // {
                //     return new CommonResponse { StatusCode = "100004", Description = "error identity" };
                // }
              
//System.IO.File.WriteAllBytes(inputRequest.SignatureFile);//todo 
              
                return new CommonResponse
                {
                   
                    StatusCode = "100000",
                    Description = "ok",
                  
                };
            }
            catch (Exception ex)
            {
                return new CommonResponse { StatusCode = "100003", Description = ex.Message };
            }

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
                    //   Photo = string.IsNullOrEmpty(theuser.Photo) ? string.Empty : theuser.Photo,
                };
            }
            catch (Exception ex)
            {
                // Log.Error("GetLearnerInfo", ex);
                return new GetTokenResponse { StatusCode = "100003", Description = ex.Message };
            }

        }
        [Route("InspectCompleteCourses")]
        [HttpPost]

        public CommonResponse InspectCompleteCourses([FromBody] CompleteCoursesRequest inputRequest)
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
                // theuser. = DateTime.Now;
                theuser.Studylog += inputRequest.AllRecords + inputRequest.AllStatus;

                _db1.History.Add(new History
                {
                    Identity = theuser.Identity,
                    Name = theuser.Name,
                    Phone = theuser.Phone,
                    Syncdate = theuser.Syncdate,
                    Startdate = theuser.Startdate,

                    Finishdate = DateTime.Now,
                    Stoplicense = theuser.Stoplicense,
                    Noticedate = theuser.Noticedate,
                    Wechat = theuser.Wechat,
                    Studylog = theuser.Studylog,

                    Drugrelated = theuser.Drugrelated,
                    // Photo = theuser.Photo,
                    Fullmark = theuser.Fullmark,
                    Inspect = theuser.Inspect,
                    Licensetype = theuser.Licensetype,
                    //  Timestamp = DateTime.Now
                });
                _db1.User.Remove(theuser);
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

                if (theuser.Startdate == null)
                {
                    theuser.Startdate = DateTime.Now;
                }
                //    Console.WriteLine("startdate,{0}", theuser.Startdate);
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
                var found = false;
                var identity = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
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
        private string GetToken()
        {
            var seed = Guid.NewGuid().ToString("N");
            return seed;
        }
    }
}