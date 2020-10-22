using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace IniUtils
{
    public class IniFile
    {
        public string FileName = "";
        public IniSectionList Sections = new IniSectionList();

        public IniFile(string fileName)
        {
            FileName = fileName;
        }

        public IEnumerable<IniSection> GetIniSections()
        {
            foreach (IniSection item in Sections.GetIniSections())
            {
                yield return item;
            }
        }

        public void Export(string directory)
        {
            Sections.ExportAll(directory);
        }


        /// <summary>
        /// 加算
        /// </summary>
        /// <param name="augend">足される方</param>
        /// <param name="addend">足す方</param>
        /// <returns>加算結果</returns>
        public static IniFile operator +(IniFile augend, IniFile addend)
        {
            IniFile result = new IniFile(augend.FileName);
            result.Sections = augend.Sections + addend.Sections;
            return result;
        }

        /// <summary>
        /// 減算
        /// </summary>
        /// <param name="minuend">引かれる方</param>
        /// <param name="subtrahend">引く方（ベース）</param>
        /// <returns>減算結果</returns>
        public static IniFile operator -(IniFile minuend, IniFile subtrahend)
        {
            IniFile result = new IniFile(minuend.FileName);
            result.Sections = minuend.Sections - subtrahend.Sections;
            return result;
        }

        /// <summary>
        /// 除算
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        public static IniFile operator /(IniFile minuend, IniFile subtrahend)
        {
            IniFile result = new IniFile(minuend.FileName);
            result.Sections = minuend.Sections / subtrahend.Sections;
            return result;
        }

        public void OutputIniFile(string path, bool outputComment)
        {
            if (File.Exists(path))
            {
                MergeIniFile(path, outputComment);
            }
            else
            {
                WriteNewFile(path, outputComment);
            }
        }

        /// <summary>
        /// 設定をすべて書き出す
        /// </summary>
        /// <param name="path">書き出すファイルパス（既にあれば上書きされる）</param>
        /// <param name="outputComment">コメントも書き出すか</param>
        private void WriteNewFile(string path, bool outputComment)
        {
            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            using (StreamWriter writer = new StreamWriter(path, false, encoding))
            {
                Sections.WriteAll(writer, outputComment);
            }
        }

        /// <summary>
        /// 指定したファイルに書き込む
        /// </summary>
        /// <param name="path"></param>
        /// <param name="outputComment"></param>
        private void MergeIniFile(string path, bool outputComment)
        {
            // 書き込む相手との差分を計算しておく
            IniFile other = IniFileParser.ParseIniFile(path);
            IniFile diff = this - other;
            IniFile quotient = this / other;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            string[] del = { "\r\n" };
            string sectionName = "";
            string sectionName_save = "";
            bool sectionHitFlg = false;
            string tmpPath = path + ".tmp";
            File.Delete(tmpPath);
            // 一時ファイルに書き込み
            using (StreamWriter writer = new StreamWriter(tmpPath, true, encoding))
            {
                foreach (string line in ReadFileLines(path))
                {

                    if (line.Trim() == "" || (!IniFileParser.IsCommentLine(line) && !IniFileParser.IsSectionLine(line) && !IniFileParser.IsKeyValueLine(line)))
                    {
                        // 無意味な行はそのまま通す
                        writer.WriteLine(line);
                        continue;
                    }

                    if (IniFileParser.IsSectionLine(line, ref sectionName))
                    {
                        if (sectionHitFlg && sectionName_save != "") {
                            // このときはセクションの終わりに到達したときなので
                            // thisにしかないキーを書き出す必要がある
                            foreach (IniData data in diff.Sections[sectionName_save].GetIniValues())
                            {
                                if (quotient.Sections[sectionName_save]?.Keys[data.KeyName] == null) { continue; }
                                // ini書き出し
                                data.Write(writer, outputComment);
                            }
                        }

                        // 差分があるセクションか判定（このフラグはセクション行でのみ更新される）
                        sectionHitFlg = diff.Sections.ContainsKey(sectionName);
                        sectionName_save = sectionName;
                    }

                    // 差分があるセクション処理中
                    if (sectionHitFlg)
                    {
                        string key = "", value = "";
                        if (IniFileParser.IsKeyValueLine(line, ref key, ref value))
                        {
                            // 差分があるキーか判定
                            if (diff.Sections[sectionName].Keys.ContainsKey(key))
                            {
                                IniData data = diff.Sections[sectionName].Keys[key];
                                IniData otherComment = other.Sections[sectionName].Keys[key];

                                // ini書き出し
                                data.Write(writer, outputComment && otherComment.Comment != data.Comment);
                                continue;
                            }
                        }
                    }

                    writer.WriteLine(line);
                }

                if (sectionHitFlg && sectionName_save != "")
                {
                    // このときはセクションの終わりに到達したときなので
                    // thisにしかないキーを書き出す必要がある
                    foreach (IniData data in diff.Sections[sectionName_save].GetIniValues())
                    {
                        if (quotient.Sections[sectionName_save]?.Keys[data.KeyName] == null) { continue; }
                        // ini書き出し
                        data.Write(writer, outputComment);
                    }
                }

                // 最後に、thisにしかないセクションを書き出す
                foreach (IniSection section in quotient.GetIniSections()
                    .Where(section => ! other.Sections.ContainsKey(section.SectionName)))
                {
                    section.Write(writer, outputComment);
                }
            }

            // ファイルに書き込み
            string bkPath = path + ".bk";
            File.Delete(bkPath);
            File.Move(path, bkPath);
            File.Move(tmpPath, path);
            File.Delete(bkPath);
        }

        private IEnumerable<string> ReadFileLines(string path)
        {
            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            using (StreamReader reader = new StreamReader(path, encoding))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
    }
}
