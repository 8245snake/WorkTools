using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IniUtils
{
    /// <summary>
    /// iniファイルの読み書き機能を提供するクラス
    /// </summary>
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
        /// iniファイルを探すフォルダ
        /// </summary>
        public static string IniFileDirectory = Environment.CurrentDirectory;

        /// <summary>
        /// 独自のパーサーでiniファイルをパースするか
        /// </summary>
        public static bool UseIniFileParser = false;

        /// <summary>
        /// メモリ上に保存してあるiniファイル
        /// </summary>
        private static IniFileList PoolIniFileList = new IniFileList();


        /// <summary>
        /// iniを読み込む
        /// </summary>
        /// <param name="path">iniファイルのパス</param>
        /// <param name="section">セクション</param>
        /// <param name="key">キー</param>
        /// <returns>設定値</returns>
        public static string GetIniValue(string path, string section, string key, string defaultVal = "")
        {
            if (UseIniFileParser)
            {
                string val = GetIniFile(GetFullPath(path))?.Sections[section]?.Keys[key]?.Value;
                if (val == null)
                {
                    return defaultVal;
                }
                return val;
            }

            // レガシー処理
            StringBuilder sb = new StringBuilder(4096);
            GetPrivateProfileString(section, key, defaultVal, sb, sb.Capacity, GetFullPath(path));
            return sb.ToString();
        }



        /// <summary>
        /// キーが存在するかを判定する
        /// </summary>
        /// <param name="path">iniファイルのパス</param>
        /// <param name="section">セクション</param>
        /// <param name="key">キー</param>
        /// <returns></returns>
        public static bool ExistsKey(string path, string section, string key)
        {
            if (UseIniFileParser)
            {
                string val = GetIniFile(path)?.Sections[section]?.Keys[key]?.Value;
                return (val != null);
            }

            // レガシー処理
            string judgeStr = "ma;jfo;g4j340ko09jjjg349985";
            string inival = GetIniValue(path, section, key, judgeStr);
            return (inival != judgeStr);
        }

        /// <summary>
        /// iniに書き込む
        /// </summary>
        /// <param name="path">iniファイルのパス</param>
        /// <param name="section">セクション</param>
        /// <param name="key">キー</param>
        /// <param name="value">設定値</param>
        /// <param name="comment">コメント</param>
        public static void SetIniValue(string path, string section, string key, string value, string comment = "")
        {
            if (UseIniFileParser)
            {
                string filename = Path.GetFileName(path);
                IniFile ini = new IniFile(filename, section, key, value, comment);
                ini.OutputIniFile(GetFullPath(path), comment != "");
                return;
            }

            // レガシー処理
            WritePrivateProfileString(section, key, value, GetFullPath(path));
        }


        /// <summary>
        /// プロパティの属性で指定したiniを読んで結果を返す
        /// </summary>
        /// <param name="type">型名</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        public static object ReadToProperty(Type type, string propertyName)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name != propertyName) { continue; }

                IniDataAttribute attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(IniDataAttribute)) as IniDataAttribute;
                if (attribute == null) { continue; }

                string IniFilePath = GetFullPath(attribute.File);
                string section = attribute.Section;
                string key = attribute.Key;
                object defVal = attribute.DefaultValue;
                // 未定義ならStringDataということにしておく
                IniDataAttribute.DataType dataType = (attribute?.ValueType != null)? attribute.ValueType : IniDataAttribute.DataType.StringData;
                switch (dataType)
                {
                    case IniDataAttribute.DataType.StringData:
                        return GetIniValue(IniFilePath, section, key, defVal?.ToString());
                    case IniDataAttribute.DataType.StringDataWithCarriageReturn:
                        return GetIniValue(IniFilePath, section, key, defVal?.ToString())?.Replace(@"\n", "\r\n");
                    case IniDataAttribute.DataType.StringArrayData:
                        return GetIniValue(IniFilePath, section, key, defVal?.ToString())?.Split(',');
                    case IniDataAttribute.DataType.StringListData:
                        return ReadIniDataList(IniFilePath, section, key);
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
        /// プロパティの属性で指定した設定のデフォルト値を返す
        /// </summary>
        /// <param name="type">型名</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        public static object GetDefaultValue(Type type, string propertyName)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name != propertyName) { continue; }

                IniDataAttribute attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(IniDataAttribute)) as IniDataAttribute;
                if (attribute == null) { continue; }
                return attribute.DefaultValue;
            }
            return default;
        }

        /// <summary>
        /// プロパティの属性で指定したiniに書き込む
        /// </summary>
        /// <param name="type">型名</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        public static void WriteToProperty(Type type, string propertyName, object value)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name != propertyName) { continue; }

                IniDataAttribute attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(IniDataAttribute)) as IniDataAttribute;
                if (attribute == null) { continue; }

                string IniFilePath = GetFullPath(attribute.File);
                string section = attribute.Section;
                string key = attribute.Key;
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
                    case IniDataAttribute.DataType.StringListData:
                        SetIniDataList(IniFilePath, section, key, (List<string>)value);
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
                if (!TryParse(defaultVal?.ToString(), ref parsedDefaultVal))
                {
                    parsedDefaultVal = default;
                }
                result = parsedDefaultVal;
            }
            return result;
        }

        /// <summary>
        /// 1起算の連番キーを読んでリストを返す
        /// </summary>
        /// <param name="IniFilePath">パス</param>
        /// <param name="section">セクション</param>
        /// <param name="key_prefix">連番キーの接頭辞</param>
        /// <returns>値のリスト</returns>
        public static List<string> ReadIniDataList(string IniFilePath, string section, string key_prefix)
        {
            List<string> list = new List<string>();
            if (!UseIniFileParser)
            {
                // レガシー処理
                string nothing = "ogregs9rt89g8er79gse9se9r";
                for (int i = 1; i <= 20000000; i++)
                {
                    string val = GetIniValue(IniFilePath, section, key_prefix + i, nothing);
                    // キーなしなら終了
                    if (val == nothing)
                    {
                        break;
                    }
                    list.Add(val);
                }
                return list;
            }

            IniFile ini = GetIniFile(IniFilePath);
            IniSection targetSection = ini?.Sections[section];
            if (targetSection == null) { return list; }
            for (int i = 1; i <= 20000000; i++)
            {
                IniData data;
                if (targetSection.Keys.TryGetValue(key_prefix + i, out data))
                {
                    list.Add(data.Value);
                }
                else
                {
                    break;
                }
            }

            return list;
        }

        /// <summary>
        /// リストをiniに書き込む
        /// </summary>
        /// <param name="IniFilePath">iniのパス</param>
        /// <param name="section">セクション</param>
        /// <param name="key_prefix">キー名の接頭辞</param>
        /// <param name="list">書き込むList</param>
        public static void SetIniDataList(string IniFilePath, string section, string key_prefix, List<string> list)
        {
            if (! UseIniFileParser) {
                // レガシー処理
                int index = 1;
                foreach (string item in list)
                {
                    SetIniValue(IniFilePath, section, key_prefix + index, item);
                    index++;
                }
                // 最後にキーを削除してストップしておく
                SetIniValue(IniFilePath, section, key_prefix + index, null);
                return;
            }

            // 出力
            string name = Path.GetFileName(IniFilePath);
            IniFile iniInsert = new IniFile(name, section);
            int count = 1;
            foreach (string item in list)
            {
                iniInsert.Sections[section].Keys.Add(new IniData(name, section, key_prefix + count, item));
                count++;
            }
            iniInsert.OutputIniFile(IniFilePath, false);

            // 最大番号以降を消す
            IniFile ini = GetIniFile(IniFilePath);
            List<string> keyNames = new List<string>();
            IniSection targetSection = ini.Sections[section];
            for (int i = count; i < 20000000; i++)
            {
                if (targetSection.Keys.ContainsKey(key_prefix + i))
                {
                    keyNames.Add(key_prefix + i);
                }
                else
                {
                    break;
                }
            }
            if (keyNames.Count > 0)
            {
                DeleteIniData(IniFilePath, section, keyNames, false, true);
            }
           
        }


        /// <summary>
        /// iniの値を削除する
        /// </summary>
        /// <param name="iniPath"></param>
        /// <param name="sectionName"></param>
        /// <param name="keyNames"></param>
        /// <param name="commentOut"></param>
        /// <param name="deleteWithComment"></param>
        public static void DeleteIniData(string iniPath, string sectionName, ICollection<string> keyNames = null, bool commentOut = true, bool deleteWithComment = false) {
            string IniName = Path.GetFileName(iniPath);
            IniFile ini = new IniFile(IniName);
            IniSection section = new IniSection(IniName, sectionName);
            if (keyNames == null)
            {
                section.Delete(iniPath, commentOut);
                return;
            }

            foreach (IniData item in keyNames.Select(key => new IniData(IniName, sectionName, key, "")))
            {
                section.Keys.Add(item);
            }

            ini.Sections.Add(section);
            ini.Delete(iniPath, commentOut, deleteWithComment);
            
        }

        /// <summary>
        /// iniファイルオブジェクトを取得する
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <returns>iniファイルオブジェクト</returns>
        private static IniFile GetIniFile(string path)
        {
            IniFile ini = PoolIniFileList[Path.GetFileName(path)];
            // なければパースして返す
            if (ini == null)
            {
                ini = IniFileParser.ParseIniFile(path);
                PoolIniFileList.Add(ini);
                return ini;
            }

            // あったらフルパスを比較して正しければ返す
            if (ini.MetaData?.FullPath == GetFullPath(path))
            {
                return ini;
            }

            // フルパスが異なればパースして返す
            ini = IniFileParser.ParseIniFile(path);
            PoolIniFileList[Path.GetFileName(path)] = ini;
            return ini;
        }

        /// <summary>
        /// パスが絶対パスかを判定する
        /// </summary>
        /// <param name="path">パス文字列</param>
        /// <returns>絶対パスならtrue</returns>
        private static bool IsAbsolutePath(string path)
        {
            if (path == null || path.Length < 1) { return false; }
            if (path.StartsWith(@"\\")) { return true; }
            if (path.Substring(1, 1) == ":") { return true; }
            return false;
        }

        /// <summary>
        /// 与えたパスの絶対パスを返す
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns>絶対パス</returns>
        /// <remarks>相対パスの場合、IniFileDirectoryを基準に探す</remarks>
        private static string GetFullPath(string path)
        {
            if (IsAbsolutePath(path))
            {
                return Path.GetFullPath(path);
            }

            return Path.GetFullPath(Path.Combine(IniFileDirectory, path));
        }

    }
}
