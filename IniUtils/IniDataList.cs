using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniUtils
{
    public class IniDataList : IDictionary<string, IniData>, ICollection<IniData>
    {
        public List<IniData> _list = new List<IniData>();

        public IniData this[string key] {
            get {
                foreach (IniData ini in _list)
                {
                    if (ini.KeyName.ToUpper() == key.ToUpper())
                    {
                        return ini;
                    }
                }
                return null;
            }
            // setは使用しない
            set => throw new NotImplementedException();
        }

        public ICollection<IniData> Values => _list;

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        ICollection<string> IDictionary<string, IniData>.Keys {
            get
            {
                return Keys;
            }
        }

        public ICollection<string> Keys {
            get
            {
                return _list.Select(x => x.KeyName).ToList();
            }
        }

        public void Add(string key, IniData value)
        {
            _list.Add(value);
        }

        public void Add(KeyValuePair<string, IniData> item)
        {
            _list.Add(item.Value);
        }

        public void Add(IniData item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(KeyValuePair<string, IniData> item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(IniData item)
        {
            return _list.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<string, IniData>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IniData[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, IniData>> GetEnumerator()
        {
            foreach (IniData ini in _list)
            {
                yield return new KeyValuePair<string, IniData>(ini.KeyName, ini);
            }
        }

        public bool Remove(string key)
        {
            // キー名が一致するするもの全て削除する
            return _list.RemoveAll(ini => ini.KeyName == key) > 0;
        }

        public bool Remove(KeyValuePair<string, IniData> item)
        {
            // キー名と値が一致するするもの全て削除する
            return _list.RemoveAll(ini => item.Value.KeyName == ini.KeyName && item.Value.Value == ini.Value) > 0;
        }

        public bool Remove(IniData item)
        {
            // キー名と値が一致するするもの全て削除する
            return _list.RemoveAll(ini => item.KeyName == ini.KeyName && item.Value == ini.Value) > 0;

        }

        public bool TryGetValue(string key, out IniData value)
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

        IEnumerator<IniData> IEnumerable<IniData>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public void ExportAll(string directory)
        {
            foreach (IniData ini in _list)
            {
                ini.Export(directory);
            }
        }

        /// <summary>
        /// 加算
        /// </summary>
        /// <param name="augend">足される方</param>
        /// <param name="addend">足す方</param>
        /// <returns>加算結果</returns>
        public static IniDataList operator +(IniDataList augend, IniDataList addend)
        {
            IniDataList result = augend;
            foreach (IniData ini in addend.Values)
            {
                // 同じキーがあったらaugendを優先
                if (! result.ContainsKey(ini.KeyName))
                {
                    result.Add(ini);
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
        public static IniDataList operator -(IniDataList minuend, IniDataList subtrahend)
        {
            IniDataList result = new IniDataList();
            // 引かれる方のリストで回す
            foreach (IniData ini in minuend.Values)
            {
                string keyName = ini.KeyName;
                if (!subtrahend.ContainsKey(keyName))
                {
                    // 引かれる方にだけある要素なら残す
                    result.Add(ini);
                }
                else
                {
                    if (ini.Value != subtrahend[keyName].Value)
                    {
                        // 引かれる方と引く方にあるが値が違う場合、引かれる方を採用する
                        result.Add(ini);
                    }
                }
            }
            return result;
        }
    }
}
