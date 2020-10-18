using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IniUtils
{
    public class IniFileUtility
    {

        #region DLL_IMPORT

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedstring,
            int nSize,
            string lpFileName);

        [DllImport("KERNEL32.DLL")]
        private static extern int WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName
            );

        #endregion

        /// <summary>
        /// iniを読み込む
        /// </summary>
        /// <param name="path">iniファイルのパス</param>
        /// <param name="section">セクション</param>
        /// <param name="key">キー</param>
        /// <returns>設定値</returns>
        public static string GetIniValue(string path, string section, string key, string defaultVal = "")
        {
            StringBuilder sb = new StringBuilder(4096);
            GetPrivateProfileString(section, key, defaultVal, sb, sb.Capacity, path);
            return sb.ToString();
        }

        /// <summary>
        /// iniに書き込む
        /// </summary>
        /// <param name="path">iniファイルのパス</param>
        /// <param name="section">セクション</param>
        /// <param name="key">キー</param>
        /// <param name="value">設定値</param>
        public static void SetIniValue(string path, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }


        /// <summary>
        /// プロパティの属性で指定したiniを読んで結果を返す
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        public static object ReadToProperty(Type type, string propertyName)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name != propertyName) { continue; }

                IniDataAttribute attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(IniDataAttribute)) as IniDataAttribute;
                if (attribute == null) { continue; }

                string IniFilePath = attribute.File;
                string section = attribute.Section;
                string key = attribute.Key;
                object defVal = attribute.DefaultValue;
                IniDataAttribute.DataType dataType = attribute.ValueType;
                switch (dataType)
                {
                    case IniDataAttribute.DataType.StringData:
                        return GetIniValue(IniFilePath, section, key, defVal?.ToString());
                    case IniDataAttribute.DataType.StringDataWithCarriageReturn:
                        return GetIniValue(IniFilePath, section, key, defVal?.ToString()).Replace(@"\n", "\r\n");
                    case IniDataAttribute.DataType.StringArrayData:
                        return GetIniValue(IniFilePath, section, key, defVal?.ToString()).Split(',');
                    case IniDataAttribute.DataType.IntegerData:
                        return ReadIniDataNumeric<int>(IniFilePath, section, key, defVal);
                    case IniDataAttribute.DataType.FloatData:
                        return ReadIniDataNumeric<double>(IniFilePath, section, key, defVal);
                    case IniDataAttribute.DataType.BooleanFlag:
                        return (GetIniValue(IniFilePath, section, key, defVal?.ToString()) == "1");
                    default:
                        break;
                }
            }
            return default;
        }

        /// <summary>
        /// プロパティの属性で指定したiniに書き込む
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        public static void WriteToProperty(Type type, string propertyName, object value)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name != propertyName) { continue; }

                IniDataAttribute attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(IniDataAttribute)) as IniDataAttribute;
                if (attribute == null) { continue; }

                string IniFilePath = attribute.File;
                string section = attribute.Section;
                string key = attribute.Key;
                object defVal = attribute.DefaultValue;
                IniDataAttribute.DataType dataType = attribute.ValueType;
                switch (dataType)
                {
                    case IniDataAttribute.DataType.IntegerData:
                    case IniDataAttribute.DataType.FloatData:
                    case IniDataAttribute.DataType.StringData:
                        SetIniValue(IniFilePath, section, key, value?.ToString());
                        break;
                    case IniDataAttribute.DataType.StringDataWithCarriageReturn:
                        SetIniValue(IniFilePath, section, key, value?.ToString().Replace("\r\n", @"\n"));
                        break;
                    case IniDataAttribute.DataType.StringArrayData:
                        SetIniValue(IniFilePath, section, key, string.Join(",", (string[])value));
                        break;
                    case IniDataAttribute.DataType.BooleanFlag:
                        SetIniValue(IniFilePath, section, key, ((bool)value) ? "1" : "0");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// TryPerseのジェネリック版
        /// </summary>
        /// <typeparam name="T">なんでもいい</typeparam>
        /// <param name="input">変換元</param>
        /// <param name="result">変換先</param>
        /// <returns>成功したらtrue</returns>
        public static bool TryParse<T>(string input, ref T result)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    //ConvertFromString(string text)の戻りは object なので T型でキャストする
                    result = (T)converter.ConvertFromString(input);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ini設定値を数値で返す
        /// </summary>
        /// <param name="section">セクション</param>
        /// <param name="key">キー</param>
        /// <param name="defaultVal">デフォルト値</param>
        /// <returns>設定値</returns>
        public static object ReadIniDataNumeric<T>(string IniFilePath, string section, string key, object defaultVal)
        {
            string iniVal = GetIniValue(IniFilePath, section, key, defaultVal?.ToString());
            T result = default;
            if (!TryParse(iniVal, ref result))
            {
                // パースに失敗したらデフォルト値をTにパースして渡す
                T parsedDefaultVal = default;
                if (!TryParse(defaultVal.ToString(), ref parsedDefaultVal))
                {
                    parsedDefaultVal = default;
                }
                result = parsedDefaultVal;
            }
            return result;
        }
    }
}
