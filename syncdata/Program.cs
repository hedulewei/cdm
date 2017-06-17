using System;
using System.IO;
using System.Linq;

namespace syncdata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("haha");
            //   Process.Start("sh /home/fyang/release/syncftp.sh"); Console.WriteLine("hellow world");
            while (true)
            {
                var hour = DateTime.Now.Hour;
                if (hour > 22 && hour <= 23)
                {
                    bbb();
                    Console.WriteLine("??");
                }
                System.Threading.Thread.Sleep(1000 * 60 * 60);
            }

            // Console.ReadLine();
        }

        static void bbb()
        {
            using (var db = new studyContext())
            {
                DirectoryInfo a = new DirectoryInfo(".");
                foreach (var b in a.GetFiles())
                {
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
