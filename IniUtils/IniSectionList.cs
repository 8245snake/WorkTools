using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniUtils
{
    public class IniSectionList : IDictionary<string, IniSection>, ICollection<IniSection>, IEnumerable<IniSection>
    {
        private List<IniSection> _list;

        public IniSectionList()
        {
            _list = new List<IniSection>();
        }

        public IniSectionList(ICollection<IniSection> list)
        {
            _list = list.ToList();
        }

        public IniSection this[string sectionName] {
            get => _list.Where(ini => ini.SectionName.ToUpper() == sectionName.ToUpper()).FirstOrDefault();

            set
            {
                List<IniSection> list = _list.Where(ini => ini.SectionName.ToUpper() != sectionName.ToUpper()).ToList();
                list.Add(value);
                _list = list;
            }
        }

        public List<string> SectionNames { get => _list.Select(x => x.SectionName).ToList(); }

        public ICollection<string> Keys => SectionNames;

        public ICollection<IniSection> Values => _list;

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public void Add(string key, IniSection value)
        {
            _list.Add(value);
        }

        public void Add(KeyValuePair<string, IniSection> item)
        {
            _list.Add(item.Value);
        }

        public void Add(IniSection item)
        {
            _list.Add(item);
        }
        public void AddAll(ICollection<IniSection> items)
        {
            foreach (IniSection item in items)
            {
                _list.Add(item);
            }
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(KeyValuePair<string, IniSection> item)
        {
            return _list.Contains(item.Value);
        }

        public bool Contains(IniSection item)
        {
            return _list.Contains(item);
        }

        public bool ContainsKey(string sectionName)
        {
            return SectionNames.Contains(sectionName);
        }

        public void CopyTo(KeyValuePair<string, IniSection>[] array, int arrayIndex)
        {
            _list.CopyTo(array.Select(s => s.Value).ToArray(), arrayIndex);
        }

        public void CopyTo(IniSection[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, IniSection>> GetEnumerator()
        {
            foreach (IniSection ini in _list)
            {
                yield return new KeyValuePair<string, IniSection>(ini.SectionName, ini);
            }
        }

        public bool Remove(string key)
        {
            // キー名が一致するもの全て削除する
            return _list.RemoveAll(ini => ini.SectionName.ToUpper() == key.ToUpper()) > 0;
        }

        public bool Remove(KeyValuePair<string, IniSection> item)
        {
            // キー名が一致するもの全て削除する
            return _list.RemoveAll(ini => item.Value.SectionName.ToUpper() == ini.SectionName.ToUpper()) > 0;
        }

        public bool Remove(IniSection item)
        {
            return _list.Remove(item);
        }

        public bool TryGetValue(string key, out IniSection value)
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

        IEnumerator<IniSection> IEnumerable<IniSection>.GetEnumerator()
        {
            foreach (IniSection item in _list)
            {
                yield return item;
            }
        }

        public IEnumerable<IniSection> GetIniSections()
        {
            foreach (IniSection item in _list)
            {
                yield return item;
            }
        }

        public void ExportAll(string directory)
        {
            foreach (IniSection section in _list)
            {
                section.Export(directory);
            }
        }

        public void WriteAll(StreamWriter writer, bool outputComment)
        {
            foreach (IniSection data in _list)
            {
                data.Write(writer, outputComment);
            }
        }

        /// <summary>
        /// 加算
        /// </summary>
        /// <param name="augend">足される方</param>
        /// <param name="addend">足す方</param>
        /// <returns>加算結果</returns>
        public static IniSectionList operator +(IniSectionList augend, IniSectionList addend)
        {
            IniSectionList result = new IniSectionList(augend);
            // augendに存在しないaddendの要素を追加する
            result.AddAll(addend.GetIniSections().Where(ini => !augend.ContainsKey(ini.SectionName)).ToList());
            return result;
        }

        /// <summary>
        /// 減算
        /// </summary>
        /// <param name="minuend">引かれる方</param>
        /// <param name="subtrahend">引く方（ベース）</param>
        /// <returns>減算結果</returns>
        public static IniSectionList operator -(IniSectionList minuend, IniSectionList subtrahend)
        {
            IniSectionList result = new IniSectionList();
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
        public static IniSectionList operator /(IniSectionList dividend, IniSectionList divisor)
        {
            // dividendにしかないキーを集めて返す
            return new IniSectionList(dividend.GetIniSections()
                .Where(ini => !divisor.ContainsKey(ini.SectionName)).ToList());
        }

        /// <summary>
        /// 剰余
        /// </summary>
        /// <param name="dividend">割られる集合</param>
        /// <param name="divisor">割る集合</param>
        /// <returns>剰余結果</returns>
        /// <remarks>両方に存在して値が異なる要素のみ返す（dividendの値を採用する）</remarks>
        public static IniSectionList operator %(IniSectionList dividend, IniSectionList divisor)
        {
            // 両方にあるキーで、値が異なるものを集めて返す
            return new IniSectionList(dividend.GetIniSections()
                .Where(section => divisor.ContainsKey(section.SectionName))
                .Where(section => section != divisor[section.SectionName]).ToList());
        }
    }
}
