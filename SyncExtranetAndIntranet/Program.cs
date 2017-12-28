using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncExtranetAndIntranet
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "plan.txt";
            var fpath = args[0];
            var user = args[1];
            var pass = args[2];
            var fname = args[3];
            var ip = args[4];
           var ret= FtpHelper.DownloadFtp(fpath, fname, ip, user, pass);
            Console.WriteLine("{0},{1},{2},{3},{4},{5}",fpath,fname,user,pass,ip,ret);
        }
    }
}
