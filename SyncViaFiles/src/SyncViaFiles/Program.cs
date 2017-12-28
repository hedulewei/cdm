using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SyncViaFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("haha");
            //   Process.Start("sh /home/fyang/release/syncftp.sh"); Console.WriteLine("hellow world");
            DirectoryInfo a = new DirectoryInfo("/home/fyang/release");
            foreach(var b in a.GetFiles())
            {
                //if (b.Name == "")
                //{
                   var content= File.ReadAllLines(b.FullName);
                    foreach(var line in content)
                    {
                    Console.WriteLine(b.Name+line);
                }
               // }
            }
            Console.ReadLine();
        }
    }
}
