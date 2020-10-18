using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniUtils
{
    public class IniSectionList : IDictionary<string, IniSection>, ICollection<IniSection>
    {
        private List<IniSection> _list = new List<IniSection>();

        public IniSection this[string sectionName] {
            get {
                foreach (IniSection section in _list)
                {
                    if (section.SectionName.ToUpper() == sectionName.ToUpper())
                    {
                        return section;
                    }
                }
                return null;
            }
            
            set => throw new NotImplementedException();
        }

        public List<string> SectionNames
        {
            get
            {
                return _list.Select(x => x.SectionName).ToList();
            }
        }

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
            throw new NotImplementedException();
        }

        public void CopyTo(IniSection[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, IniSection>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, IniSection> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IniSection item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out IniSection value)
        {
            throw new NotImplementedException();
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
        public void ExportAll(string directory)
        {
            foreach (IniSection section in _list)
            {
                section.Export(directory);
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
            IniSectionList result = augend;
            foreach (IniSection section in addend.Values)
            {
                string sectionName = section.SectionName;
                if (!result.ContainsKey(sectionName))
                {
                    // ないセクションならそのまま加える
                    result.Add(section);
                }
                else
                {
                    // あるセクションなら差分を足す
                    result[sectionName].Keys += section.Keys;
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
        public static IniSectionList operator -(IniSectionList minuend, IniSectionList subtrahend)
        {
            IniSectionList result = new IniSectionList();
            // 引かれる方のリストで回す
            foreach (IniSection section in minuend.Values)
            {
                string sectionName = section.SectionName;
                if (!subtrahend.ContainsKey(sectionName))
                {
                    // 引かれる方にだけある要素なら残す
                    result.Add(section);
                }
                else
                {
                    // 引かれる方と引く方にある場合は差分だけ残す
                    IniSection sub = section - subtrahend[sectionName];
                    if (sub.Keys.Count > 0)
                    {
                        result.Add(sub);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 第一引数の方にしかないセクションを返す
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        public static IniSectionList operator /(IniSectionList minuend, IniSectionList subtrahend)
        {
            IniSectionList result = new IniSectionList();
            // 引かれる方のリストで回す
            foreach (IniSection section in minuend.Values)
            {
                string sectionName = section.SectionName;
                if (!subtrahend.ContainsKey(sectionName))
                {
                    // 引かれる方にだけある要素なら残す
                    result.Add(section);
                }
                else
                {
                    // 引かれる方と引く方にある場合は差分だけ残す
                    IniSection sub = section / subtrahend[sectionName];
                    if (sub.Keys.Count > 0)
                    {
                        result.Add(sub);
                    }
                }
            }
            return result;
        }
    }
}
