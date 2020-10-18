using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using System.ComponentModel;

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

            IEnumerable<string> lines = readFileLines(iniFilePath);
            lines = removeNoneSenceLine(lines);

            foreach (IniSection section in getSections(lines, fileName))
            {
                if (! iniFile.Sections.ContainsKey(section.SectionName))
                {
                    iniFile.Sections.Add(section.SectionName, section);
                } 
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
        private static IEnumerable<string> readFileLines(string filePath)
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
        private static IEnumerable<string> removeNoneSenceLine(IEnumerable<string> lines)
        {
            foreach (string item in lines)
            {
                string line = item.Trim();

                // 空白行
                if (line.Length < 1)
                {
                    continue;
                }

                // コメント行なら通す
                if (line.IndexOf(";") == 0)
                {
                    yield return line;
                    continue;
                }

                // 意味をなさない行
                if (line.IndexOf("=") < 0 && line.IndexOf("[") < 0 && line.IndexOf("]") < 0)
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
        private static IEnumerable<IniSection> getSections(IEnumerable<string> lines, string fileName)
        {
            string sectionName = "";
            IniSection section = null;
            List<string> comments = new List<string>();

            foreach (string line in lines)
            {
                // セクションを見つけたら
                if (line.StartsWith("[") && line.IndexOf("]") > -1)
                {
                    int start = line.IndexOf("[") + 1;
                    int length = line.IndexOf("]") - start;
                    sectionName = line.Substring(start, length);
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
                if (line.IndexOf(";") == 0 )
                {
                    comments.Add(line.Trim(';'));
                    continue;
                }

                int indexOfEqual = line.IndexOf("=");
                if (indexOfEqual < 0)
                {
                    continue;
                }
                string key = line.Substring(0, indexOfEqual).Trim();
                // Keyも先勝ち
                if (!section.Keys.ContainsKey(key))
                {
                    string value = line.Substring(indexOfEqual + 1).Trim();
                    IniData ini = new IniData(fileName, sectionName, key, value, string.Join("\r\n", comments));
                    section.Keys.Add(ini);
                    comments.Clear();
                }
            }
            if (section != null)
            {
                yield return section;
            }
        }

        #endregion
    }


}
