using IniUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole
{
    class Program
    {
        static string iniPath1 = @"C:\Users\USER\Documents\TEST1.ini";
        static string iniPath2 = @"C:\Users\USER\Documents\TEST2.ini";
        static string iniPathResult = @"C:\Users\USER\Documents\Result.ini";


        static void Main(string[] args)
        {
            IniFileUtility.IniFileDirectory = @"C:\Users\USER\Documents\";
            IniFileUtility.UseIniFileParser = true;
            ConfigData conf = new ConfigData();
            //Console.WriteLine("終了");
            //Console.ReadKey();
        }
    }
}
