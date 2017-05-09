using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Microsoft.Owin;
using Owin;
using CDMservers;

namespace CDMservers
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}