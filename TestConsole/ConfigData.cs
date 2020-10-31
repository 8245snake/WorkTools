using System;
using System.Collections.Generic;
using System.Linq;
using IniUtils;

namespace TestConsole
{
    public class ConfigData : ConfigrationDataSource
    {

        private static string _TestStringValue;
        private static string[] _TestStringArray;
        private static List<string> _TestStringList;
        private static double _TestDoubleValue;

        [IniData(File = "TEST1.ini", Section = "Main", Key = "TestString", ValueType =IniDataAttribute.DataType.StringData)]
        public string TestStringValue { get => _TestStringValue; set => _TestStringValue = value; }

        [IniData(File = "TEST1.ini", Section = "Main", Key = "TestStringArray", ValueType = IniDataAttribute.DataType.StringArrayData)]
        public string[] TestStringArray { get => _TestStringArray; set => _TestStringArray = value; }

        [IniData(File = "TEST1.ini", Section = "Main", Key = "TestList_", ValueType = IniDataAttribute.DataType.StringListData)]
        public List<string> TestStringList { get => _TestStringList; set => _TestStringList = value; }

        [IniData(File = "TEST1.ini", Section = "Main", Key = "DoubleValue", ValueType = IniDataAttribute.DataType.FloatData)]
        public double TestDoubleValue { get => _TestDoubleValue; set => _TestDoubleValue = value; }

        [IniData(File = "TEST1.ini", Section = "his", Key = "", ValueType = IniDataAttribute.DataType.StringListData)]
        public List<string> TestHistry { get; set; }

        [IniData(File = "TEST1.ini", Section = "sub", Key = "test")]
        public string TestNull { get; set; }


        public ConfigData()
        {
            _TestStringValue = ReadStringProperty(nameof(TestStringValue));
            _TestStringArray = ReadArrayProperty(nameof(TestStringArray));
            _TestStringList = ReadListProperty(nameof(TestStringList));
            _TestDoubleValue = ReadDoubleProperty(nameof(TestDoubleValue));

            WriteProperty(nameof(TestStringValue), "hogehoge");
            WriteProperty(nameof(TestStringArray), new string[] { "あいえうお", "かきくけお", "ささいすえいお" });
            List<string> list = new List<string>();
            list.AddRange(new string[] { "A", "B", "C" });
            WriteProperty(nameof(TestHistry), list);
            WriteProperty(nameof(TestDoubleValue), 2.87);

            TestNull = ReadStringProperty(nameof(TestNull));
        }


    }
}
