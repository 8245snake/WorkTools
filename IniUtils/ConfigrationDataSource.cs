using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IniUtils
{
    /// <summary>
    /// プロパティ属性で指定した設定値の読み書き機能を提供するクラス
    /// </summary>
    public class ConfigrationDataSource
    {
        /// <summary>
        /// プロパティ属性で指定した設定を読み取る
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        protected string ReadStringProperty(string propertyName)
        {
            return (string)IniFileUtility.ReadToProperty(GetType(), propertyName);
        }

        /// <summary>
        /// プロパティ属性で指定した設定を読み取る
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        protected string[] ReadArrayProperty(string propertyName)
        {
            return (string[])IniFileUtility.ReadToProperty(GetType(), propertyName);
        }

        /// <summary>
        /// プロパティ属性で指定した設定を読み取る
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        protected List<string> ReadListProperty(string propertyName)
        {
            return (List<string>)IniFileUtility.ReadToProperty(GetType(), propertyName);
        }

        /// <summary>
        /// プロパティ属性で指定した設定を読み取る
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        protected int ReadIntegerProperty(string propertyName)
        {
            return (int)IniFileUtility.ReadToProperty(GetType(), propertyName);
        }

        /// <summary>
        /// プロパティ属性で指定した設定を読み取る
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        protected double ReadDoubleProperty(string propertyName)
        {
            return (double)IniFileUtility.ReadToProperty(GetType(), propertyName);
        }

        /// <summary>
        /// プロパティ属性で指定した設定を読み取る
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>設定値</returns>
        protected bool ReadBooleanProperty(string propertyName)
        {
            return (bool)IniFileUtility.ReadToProperty(GetType(), propertyName);
        }

        /// <summary>
        /// プロパティ属性で指定したiniに書き込む
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="value">設定値</param>
        protected void WriteProperty(string propertyName, object value)
        {
            IniFileUtility.WriteToProperty(GetType(), propertyName, value);
        }

        /// <summary>
        /// プロパティ属性で指定したiniとプライベート変数に書き込む
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="value">設定値</param>
        /// <param name="privateField"></param>
        protected void WriteProperty(string propertyName, object value, ref object privateField)
        {
            privateField = value;
            IniFileUtility.WriteToProperty(GetType(), propertyName, value);
        }
    }
}
