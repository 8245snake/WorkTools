using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
