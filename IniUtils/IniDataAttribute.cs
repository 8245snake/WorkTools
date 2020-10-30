using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IniUtils
{
    /// <summary>
    /// INIファイルに紐付くプロパティの属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IniDataAttribute : System.Attribute
    {
        /// <summary>
        /// DefaultValueに使用する設定値の型
        /// </summary>
        public enum DataType
        {
            /// <summary>
            /// 単純な文字列型
            /// </summary>
            StringData,
            /// <summary>
            /// 改行付きの文字列型（\nを改行コードに置換する）
            /// </summary>
            StringDataWithCarriageReturn,
            /// <summary>
            /// カンマ区切り型（配列にパースする）
            /// </summary>
            StringArrayData,
            /// <summary>
            /// 連番キー設定（リストにパースする）
            /// </summary>
            StringListData,
            /// <summary>
            /// 整数値型
            /// </summary>
            IntegerData,
            /// <summary>
            /// 浮動小数点数型
            /// </summary>
            FloatData,
            /// <summary>
            /// Boolean型（1がtrueでそれ以外はfalse）
            /// </summary>
            BooleanFlag
        }

        /// <summary>
        /// iniファイルのパス
        /// </summary>
        public string File { set; get; }
        /// <summary>
        /// セクション名
        /// </summary>
        public string Section { set; get; }
        /// <summary>
        /// キー名
        /// </summary>
        public string Key { set; get; }
        /// <summary>
        /// 設定値の型
        /// </summary>
        public DataType ValueType { set; get; }
        /// <summary>
        /// デフォルト値
        /// </summary>
        public object DefaultValue { set; get; }
    }
}
