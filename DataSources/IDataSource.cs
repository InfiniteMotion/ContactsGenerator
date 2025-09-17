namespace ContactsGenerator.DataSources
{
    /// <summary>
    /// 数据源接口
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// 姓氏数据
        /// </summary>
        string[] LastNames { get; }
        
        /// <summary>
        /// 名字数据
        /// </summary>
        string[] FirstNames { get; }
        
        /// <summary>
        /// 公司数据
        /// </summary>
        string[] Companies { get; }
        
        /// <summary>
        /// 职位数据
        /// </summary>
        string[] JobTitles { get; }
        
        /// <summary>
        /// 城市数据
        /// </summary>
        string[] Cities { get; }
        
        /// <summary>
        /// 街道数据
        /// </summary>
        string[] Streets { get; }
        
        /// <summary>
        /// 电话号码前缀
        /// </summary>
        string[] PhonePrefixes { get; }
        
        /// <summary>
        /// 邮箱域名
        /// </summary>
        string[] EmailDomains { get; }
        
        /// <summary>
        /// 邮箱分隔符
        /// </summary>
        string[] EmailSeparators { get; }
    }
}