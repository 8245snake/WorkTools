using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DocumentCollector
{
    class DragDropUtil
    {

        public static string GetFilePathFromDragDrop(DragEventArgs e)
        {
            // ファイルが渡されていない場合
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return "";

            // ファイルは複数来る
            foreach (string filePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                // 先頭の１つで終わり
                return filePath + "";
            }
            return "";
        }

        public static bool IsDropedFile(DragEventArgs e)
        {
            string path = GetFilePathFromDragDrop(e);
            bool isDirectory = File.GetAttributes(path).HasFlag(FileAttributes.Directory);
            return !isDirectory;
        }
    }
}
