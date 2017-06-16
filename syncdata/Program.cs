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


            Console.ReadLine();
        }

        void bbb()
        {
            using (var db = new studyContext())
            {
                DirectoryInfo a = new DirectoryInfo("/home/fyang/release");
                foreach (var b in a.GetFiles())
                {
                    //if (b.Name == "")
                    //{
                    var content = File.ReadAllLines(b.FullName);
                    foreach (var line in content)
                    {
                        Console.WriteLine(b.Name + line);
                        var theuser = db.User.FirstOrDefault(async => async.Identity == "identity");
                        if (theuser == null)
                        {
                            ;
                        }
                    }
                    // }
                }
            }
        }
    }

}
