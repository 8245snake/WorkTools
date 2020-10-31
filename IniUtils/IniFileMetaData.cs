using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace IniUtils
{
    public class IniFileMetaData
    {
        /// <summary>
        /// ファイルのフルパス
        /// </summary>
        public string FullPath = "";

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreationTime;

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime LastWriteTime;

        public IniFileMetaData()
        {
            FullPath = "";
            CreationTime = new DateTime();
            LastWriteTime = new DateTime();
        }

        public IniFileMetaData(string iniFilePath)
        {
            FullPath = Path.GetFullPath(iniFilePath);
            CreationTime = File.GetCreationTime(iniFilePath);
            LastWriteTime = File.GetLastWriteTime(iniFilePath);
        }
    }
}
