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

        private static IEnumerable<string> readFileLines(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }

        private static IEnumerable<string> removeNoneSenceLine(IEnumerable<string> lines)
        {
            foreach (string item in lines)
            {
                string line = item.Trim();

                if (line.Length < 1)
                {
                    continue;
                }

                // コメント行なら無視
                if (line.IndexOf(";") == 0)
                {
                    continue;
                }

                if (line.IndexOf("=") < 0 && line.IndexOf("[") < 0)
                {
                    continue;
                }

                yield return line;
            }
        }

        private static IEnumerable<IniSection> getSections(IEnumerable<string> lines, string fileName)
        {
            string sectionName = "";
            IniSection section = null;
            foreach (string line in lines)
            {
                // セクションを見つけたら
                if (line.IndexOf("[") > -1 && line.IndexOf("]") > -1)
                {
                    int start = line.IndexOf("[") + 1;
                    int length = line.IndexOf("]") - start;
                    sectionName = line.Substring(start, length);
                    if (section == null)
                    {
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

                int indexOfEqual = line.IndexOf("=");
                string key = line.Substring(0, indexOfEqual).Trim();
                // Keyも先勝ち
                if (!section.Keys.ContainsKey(key))
                {
                    string value = line.Substring(indexOfEqual + 1).Trim();
                    IniData ini = new IniData(fileName, sectionName, key, value);
                    section.Keys.Add(ini);
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
