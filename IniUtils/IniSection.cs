using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        /// <summary>
        /// 加算
        /// </summary>
        /// <param name="augend">足される方</param>
        /// <param name="addend">足す方</param>
        /// <returns>加算結果</returns>
        public static IniSection operator +(IniSection augend, IniSection addend)
        {
            IniSection result = new IniSection(augend.FileName, augend.SectionName);
            result.Keys = augend.Keys + addend.Keys;
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
            IniSection result = new IniSection(minuend.FileName, minuend.SectionName);
            result.Keys = minuend.Keys - subtrahend.Keys;
            return result;
        }
    }
}