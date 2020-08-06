using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
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
            StringBuilder sb = new StringBuilder(256);
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
    }
}
