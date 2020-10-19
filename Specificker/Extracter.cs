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
    public class Extracter
    {
        public enum ExecOperationMode
        {
            Add = 0,
            Sub,
            Exclusive
        }

        private string _inputPath1;
        private string _inputPath2;
        private string _outputPath;
        private ExecOperationMode _Mode;

        public Extracter(string input1, string input2 , string output, ExecOperationMode Mode)
        {
            _inputPath1 = input1;
            _inputPath2 = input2;
            _outputPath = output;
            _Mode = Mode;
        }

        public void ExtractFile(BackgroundWorker bw)
        {
            
            IniFile ini1 = IniFileParser.ParseIniFile(_inputPath1);
            bw.ReportProgress(25);
            
            IniFile ini2 = IniFileParser.ParseIniFile(_inputPath2);
            bw.ReportProgress(50);

            IniFile result = null;
            switch (_Mode)
            {
                case ExecOperationMode.Add:
                    result = ini1 + ini2;
                    break;
                case ExecOperationMode.Sub:
                    result = ini1 - ini2;
                    break;
                case ExecOperationMode.Exclusive:
                    result = ini1 / ini2;
                    break;
                default:
                    break;
            }

            bw.ReportProgress(75);

            result.OutputIniFile(Path.Combine(_outputPath, result.FileName), true);
            bw.ReportProgress(100);
        }

        public void ExtractFolder(BackgroundWorker bw)
        {
            
            IniFileList Files1 = IniFileParser.ParseIniFolder(_inputPath1);
            bw.ReportProgress(25);
            
            IniFileList Files2 = IniFileParser.ParseIniFolder(_inputPath2);
            bw.ReportProgress(50);

            IniFileList result = null;
            switch (_Mode)
            {
                case ExecOperationMode.Add:
                    result = Files1 + Files2;
                    break;
                case ExecOperationMode.Sub:
                    result = Files1 - Files2;
                    break;
                case ExecOperationMode.Exclusive:
                    result = Files1 / Files2;
                    break;
                default:
                    break;
            }

            bw.ReportProgress(75);

            result.OutputAll(_outputPath);
            bw.ReportProgress(100);
        }
    }
}
