using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMservers.Models;
using Common;
using Newtonsoft.Json;

namespace CDMservers
{
    public static class LogIntoDb
    {
        public static void Log(Model1519 db, string username, string keyword, string operation)
        {
            db.VITALLOG.Add(new VITALLOG
            {
                KEYWORD = keyword,
                USERNAME = username,
                OPERATION = operation,
                IP = HttpContext.Current.Request.UserHostAddress,
                TIME = DateTime.Now
            });
            db.SaveChangesAsync();
        }
        public static void Log(NewDblog db, string username, string keyword, UserTransaction operation)
        {
            db.VITALLOG.Add(new Models.VITALLOG
            {
                KEYWORD = keyword,
                USERNAME = username,
               // OPERATION =operation.ToString(),
                OPERATION = JsonConvert.SerializeObject(operation),
                IP = HttpContext.Current.Request.UserHostAddress,
                TIME = DateTime.Now
            });
            db.SaveChangesAsync();
            // db.SaveChanges();
        }
        public static void Log(NewDblog db, string username,  string keyword, string operation)
        {
          
            db.VITALLOG.Add(new Models.VITALLOG
            {
                KEYWORD = keyword,
                USERNAME = username,
                OPERATION = operation,
                IP =HttpContext.Current.Request.UserHostAddress,
                TIME = DateTime.Now
            });
            db.SaveChangesAsync();
           // db.SaveChanges();
        }
    }
}