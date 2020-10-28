using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace IniUtils
{
    public class IniFileList : IDictionary<string, IniFile>, ICollection<IniFile>, IEnumerable<IniFile>
    {
        public List<IniFile> _list;

        public IniFileList()
        {
            _list = new List<IniFile>();
        }

        public IniFileList(ICollection<IniFile> list)
        {
            _list = list.ToList();
        }

        public IniFile this[string fileName] {
            get => _list.Where(ini => ini.FileName.ToUpper() == fileName.ToUpper()).FirstOrDefault();

            set
            {
                List<IniFile> list = _list.Where(ini => ini.FileName.ToUpper() != fileName.ToUpper()).ToList();
                list.Add(value);
                _list = list;
            }
        }

        public ICollection<string> Keys { get => _list.Select(x => x.FileName).ToList(); }

        public ICollection<IniFile> Values { get => _list; }

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public void Add(string key, IniFile value)
        {
            _list.Add(value);
        }

        public void Add(KeyValuePair<string, IniFile> item)
        {
            _list.Add(item.Value);
        }

        public void Add(IniFile item)
        {
            _list.Add(item);
        }
        public void AddAll(ICollection<IniFile> items)
        {
            foreach (IniFile item in items)
            {
                _list.Add(item);
            }
        }
        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(KeyValuePair<string, IniFile> item)
        {
            return _list.Contains(item.Value);
        }

        public bool Contains(IniFile item)
        {
            return _list.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<string, IniFile>[] array, int arrayIndex)
        {
            _list.CopyTo(array.Select(s => s.Value).ToArray(), arrayIndex);
        }

        public void CopyTo(IniFile[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, IniFile>> GetEnumerator()
        {
            foreach (IniFile ini in _list)
            {
                yield return new KeyValuePair<string, IniFile>(ini.FileName, ini);
            }
        }

        public bool Remove(string key)
        {
            // キー名が一致するもの全て削除する
            return _list.RemoveAll(ini => ini.FileName.ToUpper() == key.ToUpper()) > 0;
        }

        public bool Remove(KeyValuePair<string, IniFile> item)
        {
            // キー名が一致するもの全て削除する
            return _list.RemoveAll(ini => item.Value.FileName.ToUpper() == ini.FileName.ToUpper()) > 0;
        }

        public bool Remove(IniFile item)
        {
            return _list.Remove(item);
        }

        public bool TryGetValue(string key, out IniFile value)
        {
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            value = null;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator<IniFile> IEnumerable<IniFile>.GetEnumerator()
        {
            foreach (IniFile file in _list)
            {
                yield return file;
            }
        }

        public IEnumerable<IniFile> GetIniFiles()
        {
            foreach (IniFile item in _list)
            {
                yield return item;
            }
        }

        public void ExportAll(string directory)
        {
            foreach (IniFile file in _list)
            {
                file.Export(directory);
            }
        }



        public void OutputAll(string directory)
        {
            foreach (IniFile file in _list)
            {
                file.OutputIniFile(Path.Combine(directory, file.FileName), true);
            }
        }


        /// <summary>
        /// 加算
        /// </summary>
        /// <param name="augend">足される方</param>
        /// <param name="addend">足す方</param>
        /// <returns>加算結果</returns>
        public static IniFileList operator +(IniFileList augend, IniFileList addend)
        {
            if (augend == null) { return new IniFileList(); }
            if (addend == null) { return new IniFileList(augend); }
            IniFileList result = new IniFileList();
            // セクションの合体
            result.AddAll(augend.GetIniFiles()
                .Select(section => section + addend[section.FileName]).ToList());
            // addendにしかないセクションを追加する
            result.AddAll(addend / augend);
            return result;
        }

        /// <summary>
        /// 減算
        /// </summary>
        /// <param name="minuend">引かれる方</param>
        /// <param name="subtrahend">引く方（ベース）</param>
        /// <returns>減算結果</returns>
        public static IniFileList operator -(IniFileList minuend, IniFileList subtrahend)
        {
            if (subtrahend == null) { return new IniFileList(minuend); }
            IniFileList result = new IniFileList();
            result.AddAll(minuend / subtrahend);
            result.AddAll(minuend % subtrahend);
            return result;
        }

        /// <summary>
        /// 除算
        /// </summary>
        /// <param name="dividend">割られる集合</param>
        /// <param name="divisor">割る集合</param>
        /// <returns>除算結果</returns>
        /// <remarks>割られる集合のみに存在する要素を返す</remarks>
        public static IniFileList operator /(IniFileList dividend, IniFileList divisor)
        {
            if (divisor == null) { return new IniFileList(dividend); }
            // dividendにしかないキーを集めて返す
            return new IniFileList(dividend.GetIniFiles()
                .Select(file => file / divisor[file.FileName])
                .Where(files => files.Sections?.Count > 0).ToList());
        }

        /// <summary>
        /// 剰余
        /// </summary>
        /// <param name="dividend">割られる集合</param>
        /// <param name="divisor">割る集合</param>
        /// <returns>剰余結果</returns>
        /// <remarks>両方に存在して値が異なる要素のみ返す（dividendの値を採用する）</remarks>
        public static IniFileList operator %(IniFileList dividend, IniFileList divisor)
        {
            if (divisor == null) { return new IniFileList(); }
            // 両方にあるキーで、値が異なるものを集めて返す
            return new IniFileList(dividend.GetIniFiles()
                .Select(file => file / divisor[file.FileName])
                .Where(files => files.Sections?.Count > 0).ToList());
        }
    }
}
