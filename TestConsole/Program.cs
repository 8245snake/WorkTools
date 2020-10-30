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
            IniFile ini1 = IniFileParser.ParseIniFile(iniPath1);
            IniFile ini2 = IniFileParser.ParseIniFile(iniPath2);
            (ini1 - ini2).OutputIniFile(iniPath2, true);
            //Console.WriteLine("終了");
            //Console.ReadKey();
        }
    }
}
