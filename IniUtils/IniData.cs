using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IniUtils
{
    public class IniData
    {
        private string _FileName = "";
        private string _SectionName = "";
        private string _KeyName = "";
        private string _Value = "";
        private string _Comment = "";
        private string _RawString = "";

        public string FileName { get => _FileName; }
        public string SectionName { get => _SectionName; }
        public string KeyName { get => _KeyName; }
        public string Value  { get => _Value; }
        public string Comment { get => _Comment; }
        public string RawString { get => _RawString; }

        public IniData(string file, string section , string key , string value, string comment = "", string rawString = "")
        {
            _FileName = file;
            _SectionName = section;
            _KeyName = key;
            _Value = value;
            _Comment = comment;
            _RawString = rawString;
        }

        public void Export(string directory)
        {
            string path = Path.Combine(directory, FileName);
            IniFileUtility.SetIniValue(path, SectionName, KeyName, Value);
        }

        public void Write(StreamWriter writer, bool outputComment)
        {
            // コメント書き出し
            if (outputComment && _Comment != "")
            {
                string[] del = { "\r\n" };
                foreach (string line in _Comment.Split(del, StringSplitOptions.None))
                {
                    writer.WriteLine(";" + line);
                }
            }
            // キー＆値書き出し
            writer.WriteLine(_RawString);
        }

        public static bool operator ==(IniData data1, IniData data2)
        {
            return (data1?.SectionName == data2?.SectionName && data1?.KeyName == data2?.KeyName);
        }
        public static bool operator !=(IniData data1, IniData data2)
        {
            return !(data1 == data2);
        }
    }
}
