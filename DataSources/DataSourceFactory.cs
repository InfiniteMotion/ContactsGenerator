using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsGenerator.DataSources
{
    /// <summary>
    /// 数据源工厂类
    /// </summary>
    public static class DataSourceFactory
    {
        /// <summary>
        /// 根据语言获取数据源
        /// </summary>
        /// <param name="language">语言代码</param>
        /// <returns>数据源实例</returns>
        public static IDataSource GetDataSource(string language)
        {
            return language switch
            {
                "zh-CN" => new ChineseDataSource(),
                "en-US" => new EnglishDataSource(),
                "ja-JP" => new JapaneseDataSource(), // 添加日语数据源支持
                _ => new ChineseDataSource() // 默认使用中文数据源
            };
        }
    }
}