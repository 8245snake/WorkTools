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
            if (augend?.FileName == null) { return null; }
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
            if (minuend?.FileName == null) { return null; }
            IniFile result = new IniFile(minuend.FileName);
            result.Sections = minuend.Sections - subtrahend?.Sections;
            return result;
        }

        /// <summary>
        /// 除算
        /// </summary>
        /// <param name="dividend">割られる集合</param>
        /// <param name="divisor">割る集合</param>
        /// <returns>除算結果</returns>
        /// <remarks>割られる集合のみに存在する要素を返す</remarks>
        public static IniFile operator /(IniFile dividend, IniFile divisor)
        {
            if (dividend?.FileName == null) { return null; }
            IniFile result = new IniFile(dividend.FileName);
            result.Sections = dividend.Sections / divisor?.Sections;
            return result;
        }

        /// <summary>
        /// 剰余
        /// </summary>
        /// <param name="dividend">割られる集合</param>
        /// <param name="divisor">割る集合</param>
        /// <returns>剰余結果</returns>
        /// <remarks>両方に存在して値が異なる要素のみ返す（dividendの値を採用する）</remarks>
        public static IniFile operator %(IniFile dividend, IniFile divisor)
        {
            if (dividend?.FileName == null) { return null; }
            IniFile result = new IniFile(dividend.FileName);
            result.Sections = dividend.Sections % divisor?.Sections;
            return result;
        }

        /// <summary>
        /// 積算
        /// </summary>
        /// <param name="multiplicand">被乗数</param>
        /// <param name="multiplier">乗数</param>
        /// <returns>積算結果</returns>
        /// <remarks>両方に存在して値が等しい要素のみ返す</remarks>
        public static IniFile operator *(IniFile multiplicand, IniFile multiplier)
        {
            if (multiplicand?.FileName == null) { return null; }
            IniFile result = new IniFile(multiplicand.FileName);
            result.Sections = multiplicand.Sections * multiplier?.Sections;
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

        /// <summary>
        /// 指定したファイルからキーを削除する
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="commentOut">コメントアウト：true, 行削除:false</param>
        /// <param name="deleteWithComment">行削除の際にコメント行も削除するか</param>
        public void Delete(string path, bool commentOut = true, bool deleteWithComment = false)
        {
            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            string sectionName = "";
            bool sectionHitFlg = false;
            string tmpPath = path + ".tmp";
            File.Delete(tmpPath);
            List<string> comments = new List<string>();
            // 一時ファイルに書き込み
            using (StreamWriter writer = new StreamWriter(tmpPath, true, encoding))
            {
                foreach (string line in ReadFileLines(path))
                {
                    // セクション行かの判定
                    if (IniFileParser.IsSectionLine(line, ref sectionName))
                    {
                        // この分岐に入ったときはセクションが変わったときなのでキャッシュを全て吐き出す
                        foreach (string item in comments)
                        {
                            writer.WriteLine(item);
                        }
                        comments.Clear();

                        // 差分があるセクションか判定（このフラグはセクション行でのみ更新される）
                        sectionHitFlg = this.Sections.ContainsKey(sectionName);
                    }

                    // 削除対象のセクション処理中
                    if (sectionHitFlg)
                    {
                        // この行がコメントか、意味のない行ならキャッシュする
                        if (IniFileParser.IsCommentLine(line) || (!IniFileParser.IsSectionLine(line) && !IniFileParser.IsKeyValueLine(line)))
                        {
                            // 生の文字列をキャッシュしておく
                            comments.Add(line);
                            continue;
                        }

                        string key = "", value = "";
                        if (IniFileParser.IsKeyValueLine(line, ref key, ref value))
                        {
                            // 削除対象のキーか判定
                            if (this.Sections[sectionName]?.Keys[key] != null)
                            {
                                // 削除対象のキーだった場合の処理
                                if (!deleteWithComment || commentOut)
                                {
                                    // コメントごと削除しないモードならコメントはキャッシュから戻す
                                    foreach (string item in comments)
                                    {
                                        writer.WriteLine(item);
                                    }
                                }

                                if (commentOut)
                                {
                                    // コメントアウト
                                    writer.WriteLine(";" + line);
                                }
                            }
                            else
                            {
                                // 削除対象でない場合はそのまま帰す
                                foreach (string item in comments)
                                {
                                    writer.WriteLine(item);
                                }
                                writer.WriteLine(line);
                            }

                            comments.Clear();
                            continue;
                        }
                    }

                    // 削除対象ではないセクションはそのまま書き込む
                    writer.WriteLine(line);
                }

            }

            // ファイルに書き込み
            string bkPath = path + ".bk";
            File.Delete(bkPath);
            File.Move(path, bkPath);
            File.Move(tmpPath, path);
            File.Delete(bkPath);
        }

    }
}
