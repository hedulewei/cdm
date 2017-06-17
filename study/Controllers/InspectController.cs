
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
                theuser.Studycompletedate = DateTime.Now;
                theuser.Studylog += inputRequest.AllRecords + inputRequest.AllStatus;

                _db1.History.Add(new History
                {
                    Identity = theuser.Identity,
                    Name = theuser.Name,
                    Phone = theuser.Phone,
                    Syncdate = theuser.Syncdate,
                    Studystartdate = theuser.Studystartdate,

                    Studycompletedate = theuser.Studycompletedate,
                    Stoplicense = theuser.Stoplicense,
                    Noticedate = theuser.Noticedate,
                    Wechat = theuser.Wechat,
                    Studylog = theuser.Studylog,

                    Drugrelated = theuser.Drugrelated,
                    Photo = theuser.Photo,
                    Fullmark = theuser.Fullmark,
                    Inspect = theuser.Inspect,
                    Licensetype = theuser.Licensetype,
                    Timestamp = DateTime.Now
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