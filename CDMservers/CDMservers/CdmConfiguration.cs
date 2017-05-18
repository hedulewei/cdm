using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CDMservers
{
    public static class CdmConfiguration
    {
        public static readonly string FileRootPath = ConfigurationManager.AppSettings["FileRootPath"];
        public static readonly string DataSource = ConfigurationManager.ConnectionStrings["CityData"].ConnectionString;
    }
}