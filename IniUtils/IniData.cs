using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IniUtils
{
    public class IniData : ICloneable
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
        public string KeyValue
        {
            get
            {
                if (_RawString != "") { return _RawString; }
                return _KeyName + "=" + _Value;
            }
        }

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
            writer.WriteLine(KeyValue);
        }

        public bool IsSameKeyValue(IniData data)
        {
            if (data == null) { return false; }
            return (data.KeyName.ToUpper() == this.KeyName.ToUpper() 
                && data.Value.ToUpper() == this.Value.ToUpper());
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
