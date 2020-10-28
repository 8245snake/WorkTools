using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniUtils
{
    public class IniDataList : IDictionary<string, IniData>, ICollection<IniData>, IEnumerable<IniData>
    {
        public List<IniData> _list;

        public IniDataList()
        {
            _list = new List<IniData>();
        }

        public IniDataList(ICollection<IniData> list)
        {
            _list = list.ToList();
        }

        public IniData this[string key] {
            get => _list.Where(ini => ini.KeyName.ToUpper() == key.ToUpper()).FirstOrDefault();

            set {
                List<IniData> list = _list.Where(ini => ini.KeyName.ToUpper() != key.ToUpper()).ToList();
                list.Add(value);
                _list = list;
            }
        }

        public ICollection<IniData> Values => _list;

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        ICollection<string> IDictionary<string, IniData>.Keys { get => Keys; }

        public ICollection<string> Keys { get => _list.Select(x => x.KeyName).ToList(); }

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

        public void AddAll(ICollection<IniData> items)
        {
            foreach (IniData item in items)
            {
                _list.Add(item);
            }
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(KeyValuePair<string, IniData> item)
        {
            return _list.Contains(item.Value);
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
            _list.CopyTo(array.Select(s => s.Value).ToArray(), arrayIndex);
        }

        public void CopyTo(IniData[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
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
            return _list.RemoveAll(ini => ini.KeyName.ToUpper() == key.ToUpper()) > 0;
        }

        public bool Remove(KeyValuePair<string, IniData> item)
        {
            // キー名と値が一致するするもの全て削除する
            return _list.RemoveAll(ini => item.Value.KeyName.ToUpper() == ini.KeyName.ToUpper() && item.Value.Value.ToUpper() == ini.Value.ToUpper()) > 0;
        }

        public bool Remove(IniData item)
        {
            return _list.Remove(item);
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

        public IEnumerable<IniData> GetIniValues()
        {
            foreach (IniData ini in _list)
            {
                yield return ini;
            }
        }

        public void ExportAll(string directory)
        {
            foreach (IniData ini in _list)
            {
                ini.Export(directory);
            }
        }

        public void WriteAll(StreamWriter writer, bool outputComment)
        {
            foreach (IniData data in _list)
            {
                // ini書き出し
                data.Write(writer, outputComment);
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
            if (augend == null) { return new IniDataList(); }
            if (addend == null) { return new IniDataList(augend); }
            IniDataList result = new IniDataList(augend);
            // augendに存在しないaddendの要素を追加する
            result.AddAll(addend.GetIniValues().Where(ini => !augend.ContainsKey(ini.KeyName)).ToList());
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
            if (minuend == null) { return new IniDataList(); }
            if (subtrahend == null) { return new IniDataList(minuend); }
            IniDataList result = new IniDataList();
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
        public static IniDataList operator /(IniDataList dividend, IniDataList divisor)
        {
            if (dividend == null) { return new IniDataList(); }
            if (divisor == null) { return new IniDataList(dividend); }
            // dividendにしかないキーを集めて返す
            return new IniDataList(dividend.GetIniValues()
                .Where(ini => !divisor.ContainsKey(ini.KeyName)).ToList());
        }


        /// <summary>
        /// 剰余
        /// </summary>
        /// <param name="dividend">割られる集合</param>
        /// <param name="divisor">割る集合</param>
        /// <returns>剰余結果</returns>
        /// <remarks>両方に存在して値が異なる要素のみ返す（dividendの値を採用する）</remarks>
        public static IniDataList operator %(IniDataList dividend, IniDataList divisor)
        {
            if (dividend == null || divisor == null) { return new IniDataList(); }
            // 両方にあるキーで、値が異なるものを集めて返す
            return new IniDataList(dividend.GetIniValues()
                .Where(ini => divisor.ContainsKey(ini.KeyName))
                .Where(ini => !ini.IsSameKeyValue(divisor[ini.KeyName])).ToList());
        }


        /// <summary>
        /// 積算
        /// </summary>
        /// <param name="multiplicand">被乗数</param>
        /// <param name="multiplier">乗数</param>
        /// <returns>積算結果</returns>
        /// <remarks>両方に存在して値が等しい要素のみ返す</remarks>
        public static IniDataList operator *(IniDataList multiplicand, IniDataList multiplier)
        {
            if (multiplicand == null || multiplier == null) { return new IniDataList(); }
            // 両方にあるキーで、値が等しいものを集めて返す
            return new IniDataList(multiplicand.GetIniValues()
                .Where(ini => multiplier.ContainsKey(ini.KeyName))
                .Where(ini => ini.IsSameKeyValue(multiplier[ini.KeyName])).ToList());
        }
    }
}
