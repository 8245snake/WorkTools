using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IniUtils
{
    public class IniData
    {
        public string FileName = "";
        public string SectionName = "";
        public string KeyName = "";
        public string Value = "";
        public string Comment = "";
        

        public IniData(string file, string section , string key , string value, string comment)
        {
            FileName = file;
            SectionName = section;
            KeyName = key;
            Value = value;
            Comment = comment;
        }

        public void Export(string directory)
        {
            string path = Path.Combine(directory, FileName);
            IniFileUtility.SetIniValue(path, SectionName, KeyName, Value);
        }
    }
}
