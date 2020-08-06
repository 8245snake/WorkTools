using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniUtils
{
    public class IniFileList : IDictionary<string, IniFile>, ICollection<IniFile>
    {
        public List<IniFile> _list = new List<IniFile>();

        public IniFile this[string fileName] {
            get
            {
                foreach (IniFile file in _list)
                {
                    if (file.FileName.ToUpper() == fileName.ToUpper())
                    {
                        return file;
                    }
                }
                return null;
            }

            set => throw new NotImplementedException();
        }

        public ICollection<string> Keys {
            get {

                return _list.Select(x => x.FileName).ToList();
            }
        }

        public ICollection<IniFile> Values
        {
            get
            {
                return _list;
            }
        }

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
            throw new NotImplementedException();
        }

        public void CopyTo(IniFile[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, IniFile>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, IniFile> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IniFile item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out IniFile value)
        {
            throw new NotImplementedException();
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
        public void ExportAll(string directory)
        {
            foreach (IniFile file in _list)
            {
                file.Export(directory);
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
            IniFileList result = augend;
            foreach (IniFile file in addend.Values)
            {
                string fileName = file.FileName;
                if (!result.ContainsKey(fileName))
                {
                    // ないファイルならそのまま加える
                    result.Add(file);
                }
                else
                {
                    // あるファイルなら差分を足す
                    result[fileName] += file;
                }
            }
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
            IniFileList result = new IniFileList();
            // 引かれる方のリストで回す
            foreach (IniFile file in minuend.Values)
            {
                string fileName = file.FileName;
                if (!subtrahend.ContainsKey(fileName))
                {
                    // 引かれる方にだけある要素なら残す
                    result.Add(file);
                }
                else
                {
                    // 引かれる方と引く方にある場合は差分だけ残す
                    IniFile sub = file - subtrahend[fileName];
                    if (sub.Sections.Count > 0)
                    {
                        result.Add(sub);
                    }
                }
            }
            return result;
        }
    }
}
