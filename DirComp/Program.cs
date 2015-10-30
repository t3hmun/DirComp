using System;

namespace DirComp
{
    using System.IO;
    class Program
    {
        static void Main(string[] args)
        {
            string pathA = @"W:\OneDrive\music";
            string pathB = @"G:\music";

            var start = DateTime.Now;
            var dirA = new Dir(new System.IO.DirectoryInfo(pathA));
            var time = (DateTime.Now - start).TotalMilliseconds;

            Console.WriteLine("mainlisting: " + time + "ms");

            start = DateTime.Now;
            var dirB = new Dir(new System.IO.DirectoryInfo(pathB));
            time = (DateTime.Now - start).TotalMilliseconds;

            Console.WriteLine("backlisting: " + time + "ms");

            start = DateTime.Now;
            var diff = dirA.Missing(dirB);
            time = (DateTime.Now - start).TotalMilliseconds;

            Console.WriteLine("diff calc  : " + time + "ms");

            File.WriteAllLines("diff.txt", diff);
        }
    }
}
