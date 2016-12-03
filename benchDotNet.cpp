//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Perf("M1", () => M1(true,true,true), 1000000);            
            Perf("M2",  () => M2(new[]{true,true,true}), 1000000);
            Perf("M3",  () => M3(1|2|3), 1000000);
        }
        
        public static void M1(bool b1, bool b2, bool b3)
        {
        }
        
        public static void M2(bool [] bools)
        {
        }
        
        public static void M3(int Flag)
        {
        }
        
        public static void Perf(string id, Action a, int iters){
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for(var i=0;i<iters;i++)
                a();
            
            sw.Stop();
            Console.WriteLine("{1} took {0}ms", sw.ElapsedMilliseconds,id);
            
        }
    }
}
