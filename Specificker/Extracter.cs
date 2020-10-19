using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using IniUtils;

namespace Specificker
{
    class Extracter
    {
        private string _inputPath1;
        private string _inputPath2;
        private string _outputPath;

        public Extracter(string input1, string input2 , string output)
        {
            _inputPath1 = input1;
            _inputPath2 = input2;
            _outputPath = output;
        }

        public void ExtractFile(BackgroundWorker bw)
        {
            
            IniFile ini1 = IniFileParser.ParseIniFile(_inputPath1);
            bw.ReportProgress(25);
            
            IniFile ini2 = IniFileParser.ParseIniFile(_inputPath2);
            bw.ReportProgress(50);
            
            IniFile sub = ini1 - ini2;
            bw.ReportProgress(75);

            sub.OutputIniFile(Path.Combine(_outputPath, sub.FileName), true);
            bw.ReportProgress(100);
        }

        public void ExtractFolder(BackgroundWorker bw)
        {
            
            IniFileList Files1 = IniFileParser.ParseIniFolder(_inputPath1);
            bw.ReportProgress(25);
            
            IniFileList Files2 = IniFileParser.ParseIniFolder(_inputPath2);
            bw.ReportProgress(50);
            
            IniFileList sub = Files1 - Files2;
            bw.ReportProgress(75);
            
            sub.OutputAll(_outputPath);
            bw.ReportProgress(100);
        }
    }
}
