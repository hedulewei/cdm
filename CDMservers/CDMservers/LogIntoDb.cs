using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMservers.Models;

namespace CDMservers
{
    public static class LogIntoDb
    {
        public static void Log(NewDblog db, string username, DateTime timestamp, string keyword, string operation,string ip)
        {
          //  var ip ="?";
         //   var ip = GetIP();

           // var db = new DbLog();
            db.VITALLOG.Add(new Models.VITALLOG
            {
                KEYWORD = keyword,
                USERNAME = username,
                OPERATION = operation,
                IP =ip,
                TIME = timestamp
            });
         //   db.SaveChangesAsync();
            db.SaveChanges();
        }
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
     
    }
}