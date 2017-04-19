using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Common;
using DataService;
using log4net;

namespace CDMservers.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
      
        private void InputLog(BusinessModel input)
        {
            Log.Info(string.Format("userName={0},counterNum={1},countyCode={2}", input.userName, input.counterNum,
                input.countyCode));
        }
    }
}
