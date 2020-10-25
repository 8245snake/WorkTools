using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IniUtils
{
    public class IniFileParser
    {
        #region STATIC

        /// <summary>
        /// 単一のiniファイルをパースする
        /// </summary>
        /// <param name="iniFilePath">iniファイルのパス</param>
        /// <returns>iniファイルデータ</returns>
        public static IniFile ParseIniFile(string iniFilePath)
        {
            string fileName = Path.GetFileName(iniFilePath);
            IniFile iniFile = new IniFile(fileName);

            IEnumerable<string> lines = ReadFileLines(iniFilePath);
            lines = RemoveNoneSenceLine(lines);

            foreach (IniSection section in ParseSections(lines, fileName))
            {
                // 既に登録済みのセクションは弾く
                if (iniFile.Sections.ContainsKey(section.SectionName)) { continue; }
                // セクション追加
                iniFile.Sections.Add(section.SectionName, section);
            }
            return iniFile;
        }

        /// <summary>
        /// 指定したiniをパースする（複数）
        /// </summary>
        /// <param name="iniFilePathArr">iniファイルのパス</param>
        /// <returns>iniファイルデータのリスト<</returns>
        public static IniFileList ParseIniFiles(params string[] iniFilePathArr)
        {
            IniFileList list = new IniFileList();
            foreach (string path in iniFilePathArr)
            {
                list.Add(ParseIniFile(path));
            }
            return list;
        }

        /// <summary>
        /// 指定したフォルダ内のiniをパースする
        /// </summary>
        /// <param name="iniFolder">iniがあるフォルダパス</param>
        /// <returns>iniファイルデータのリスト</returns>
        public static IniFileList ParseIniFolder(string iniFolder)
        {
            IniFileList list = new IniFileList();
            foreach (string path in Directory.EnumerateFiles(iniFolder, "*.ini"))
            {
                list.Add(ParseIniFile(path));
            }
            return list;
        }

        /// <summary>
        /// ファイルから１行ずつ読み出す
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>１行分の文字列</returns>
        private static IEnumerable<string> ReadFileLines(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.GetEncoding("shift_jis")))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }

        /// <summary>
        /// 無意味な行を除いて返すフィルタ関数
        /// </summary>
        /// <param name="lines">１行分の文字列</param>
        /// <returns>意味のある１行分の文字列</returns>
        /// <remarks>空白行、セクションやキーとして成り立っていない行を消す。コメントは残す。</remarks>
        private static IEnumerable<string> RemoveNoneSenceLine(IEnumerable<string> lines)
        {
            foreach (string item in lines)
            {
                string line = item.Trim();

                // 意味をなさない行
                if (!IsCommentLine(line) && !IsSectionLine(line) && !IsKeyValueLine(line))
                {
                    continue;
                }

                yield return line;
            }
        }

        /// <summary>
        /// セクション単位で抽出して都度返す
        /// </summary>
        /// <param name="lines">読みだした行</param>
        /// <param name="fileName">iniファイル名</param>
        /// <returns>セクション</returns>
        private static IEnumerable<IniSection> ParseSections(IEnumerable<string> lines, string fileName)
        {
            string sectionName = "";
            IniSection section = null;
            List<string> comments = new List<string>();

            foreach (string line in lines)
            {
                // セクションを見つけたら
                if (IsSectionLine(line, ref sectionName))
                {
                    if (section == null)
                    {
                        // １回目しか通らない
                        section = new IniSection(fileName, sectionName);
                    }
                    else
                    {
                        yield return section;
                        // これまでのセクションを返して新しいセクションで開始
                        section = new IniSection(fileName, sectionName);
                    }
                    continue;
                }

                // コメントならばキャッシュし、次に見つかるキーまで保持
                string commnet = "";
                if (IsCommentLine(line, ref commnet))
                {
                    comments.Add(commnet);
                    continue;
                }

                // キーを見つけたらセクションに追加
                string key ="", value = "";
                if (!IsKeyValueLine(line, ref key, ref value))
                {
                    continue;
                }
                // Keyも先勝ち
                if (!section.Keys.ContainsKey(key))
                {
                    IniData ini = new IniData(fileName, sectionName, key, value, string.Join("\r\n", comments), line);
                    section.Keys.Add(ini);
                    comments.Clear();
                }
            }
            if (section != null)
            {
                yield return section;
            }
        }

        /// <summary>
        /// その行がセクションかを判定し、OUT引数に返す
        /// </summary>
        /// <param name="line">判定する行</param>
        /// <param name="sectionName">セクション名返却用</param>
        /// <returns>セクションならtrue</returns>
        public static bool IsSectionLine(string line, ref string sectionName)
        {
            // セクションを見つけたら
            if (line.StartsWith("[") && line.IndexOf("]") > -1)
            {
                int start = 1;
                int length = line.IndexOf("]") - start;
                sectionName = line.Substring(start, length);
                return true;
            }
            return false;
        }

        public static bool IsSectionLine(string line)
        {
            return (line.StartsWith("[") && line.IndexOf("]") > -1);
        }

        public static bool IsCommentLine(string line, ref string commnet)
        {
            if (line.StartsWith(";"))
            {
                commnet = line.TrimStart(';');
                return true;
            }
            return false;
        }
        public static bool IsCommentLine(string line)
        {
            return (line.StartsWith(";"));
        }

        public static bool IsKeyValueLine(string line, ref string key, ref string value)
        {
            int indexOfEqual = line.IndexOf("=");
            if (indexOfEqual < 0)
            {
                return false;
            }
            key = line.Substring(0, indexOfEqual).Trim();
            value = line.Substring(indexOfEqual + 1).Trim();
            // 値がダブルクオーテーションで囲われている場合は中身を返す
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value.Substring(1, value.Length - 2);
            }
            return true;
        }

        public static bool IsKeyValueLine(string line)
        {
            return (line.IndexOf("=") > 0);
        }

        #endregion
    }


}
