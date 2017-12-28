using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
namespace syncdata
{
    class Program
    {
        static string ftpPath = ".";
        static void Main(string[] args)
        {
            Console.WriteLine("haha");
            var t1 = new Thread(new ThreadStart(filetodbthread));
            t1.Start();
            var t2 = new Thread(new ThreadStart(dbtofilethread));
            t2.Start();
            Console.ReadLine();
        
        }

        private static void filetodbthread()
        {
            while (true)
            {
                var hour = DateTime.Now.Hour;
                if (hour > 22 && hour <= 23)
                {
                    FileToDb();
                    Console.WriteLine("??");
                }
                System.Threading.Thread.Sleep(1000 * 60 * 60);
            }
        }
        private static void dbtofilethread()
        {
            while (true)
            {
                var hour = DateTime.Now.Hour;
                if (hour > 22 && hour <= 23)
                {
                    DbToFileForExtranetToIntranet();
                    Console.WriteLine("??");
                }
                System.Threading.Thread.Sleep(1000 * 60 * 60);
            }
        }
        static void DbToFileForExtranetToIntranet()
        {
            using (var db = new studyContext())
            {
                var now = DateTime.Now;
                var yesterday = now.AddDays(-1);
                var theuser = db.History.Where(async => async.Timestamp.CompareTo(now) <= 0 && async.Timestamp.CompareTo(yesterday) > 0);
                var fname = ftpPath + "/extranetToIntranet.dat";
                foreach (var re in theuser)
                {
                    File.AppendAllText(fname, JsonConvert.SerializeObject(re));
                }
            }
        }
        static void FileToDb()
        {
            using (var db = new studyContext())
            {
                DirectoryInfo a = new DirectoryInfo(".");
                foreach (var b in a.GetFiles())
                {//b.Attributes.HasFlag()
                    if (b.Name == "users.dat")
                    {
                        var content = File.ReadAllLines(b.FullName);
                        foreach (var line in content)
                        {
                            Console.WriteLine(b.Name + line);
                            var fields = line.Split(',');
                            var identity = fields[0];
                            var name = fields[1];
                            // var syncdate=fields[2];
                            var theuser = db.User.FirstOrDefault(async => async.Identity == identity);
                            if (theuser == null)
                            {
                                db.User.Add(new User
                                {
                                    Identity = identity,
                                    Name = name,
                                    Syncdate = DateTime.Now
                                });
                            }
                            else
                            {
                                // theuser.
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }

}
