using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IniUtils;

namespace UnitTestForIniUtils
{
    [TestClass]
    public class UnitTest1
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        string iniPath = @"C:\Users\USER\Documents\TEST.ini";

        [TestMethod]
        public void TestMethod1()
        {
            IniFile ini = IniFileParser.ParseIniFile(iniPath);
            Assert.IsNotNull(ini);
        }
    }
}
