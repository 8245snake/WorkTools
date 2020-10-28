using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniUtils
{
    public class IniSection
    {
        public string FileName = "";
        public string SectionName = "";
        public IniDataList Keys = new IniDataList();

        public IniSection(string file, string section)
        {
            FileName = file;
            SectionName = section;
        }
        public void Export(string directory)
        {
            Keys.ExportAll(directory);
        }

        public void Write(StreamWriter writer, bool outputComment)
        {
            // セクション書き出し
            writer.WriteLine("[" + this.SectionName + "]");
            Keys.WriteAll(writer, outputComment);
        }

        public void Delete(string path, bool commentOut = true)
        {
            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            string sectionName = "";
            bool sectionHitFlg = false;
            string tmpPath = path + ".tmp";
            File.Delete(tmpPath);
            // 一時ファイルに書き込み
            using (StreamWriter writer = new StreamWriter(tmpPath, true, encoding))
            {
                foreach (string line in ReadFileLines(path))
                {
                    // セクション行かの判定
                    if (IniFileParser.IsSectionLine(line, ref sectionName))
                    {
                        // 差分があるセクションか判定（このフラグはセクション行でのみ更新される）
                        sectionHitFlg = this.SectionName == sectionName;
                    }

                    // 削除対象のセクション処理中
                    if (sectionHitFlg)
                    {
                        if (commentOut)
                        {
                            // コメントアウト
                            writer.WriteLine(";" + line);
                        }
                        continue;
                        
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

        public IEnumerable<IniData> GetIniValues()
        {
            foreach (IniData item in Keys.GetIniValues())
            {
                yield return item;
            }        
        }



        /// <summary>
        /// 加算
        /// </summary>
        /// <param name="augend">足される方</param>
        /// <param name="addend">足す方</param>
        /// <returns>加算結果</returns>
        public static IniSection operator +(IniSection augend, IniSection addend)
        {
            if (augend?.SectionName == null) { return null; }
            IniSection result = new IniSection(augend.FileName, augend.SectionName);
            result.Keys = augend?.Keys + addend?.Keys;
            return result;
        }

        /// <summary>
        /// 減算
        /// </summary>
        /// <param name="minuend">引かれる方</param>
        /// <param name="subtrahend">引く方（ベース）</param>
        /// <returns>減算結果</returns>
        public static IniSection operator -(IniSection minuend, IniSection subtrahend)
        {
            if (minuend?.SectionName == null) { return null; }
            IniSection result = new IniSection(minuend.FileName, minuend.SectionName);
            result.Keys = minuend?.Keys - subtrahend?.Keys;
            return result;
        }

        /// <summary>
        /// 除算
        /// </summary>
        /// <param name="dividend">割られる集合</param>
        /// <param name="divisor">割る集合</param>
        /// <returns>除算結果</returns>
        /// <remarks>割られる集合のみに存在する要素を返す</remarks>
        public static IniSection operator /(IniSection dividend, IniSection divisor)
        {
            if (dividend?.SectionName == null) { return null; }
            IniSection result = new IniSection(dividend.FileName, dividend.SectionName);
            result.Keys = dividend?.Keys / divisor?.Keys;
            return result;
        }

        /// <summary>
        /// 剰余
        /// </summary>
        /// <param name="dividend">割られる集合</param>
        /// <param name="divisor">割る集合</param>
        /// <returns>剰余結果</returns>
        /// <remarks>両方に存在して値が異なる要素のみ返す（dividendの値を採用する）</remarks>
        public static IniSection operator %(IniSection dividend, IniSection divisor)
        {
            if (dividend?.SectionName == null) { return null; }
            IniSection result = new IniSection(dividend.FileName, dividend.SectionName);
            result.Keys = dividend?.Keys % divisor?.Keys;
            return result;
        }

        public bool IsSameAllContents(IniSection section)
        {
            if (section == null) { return false; }
            foreach (IniData data in section.GetIniValues())
            {
                if (!(bool)Keys[data.KeyName]?.IsSameKeyValue(data))
                {
                    return false;
                }
            }
            return true;
        }

    }
}