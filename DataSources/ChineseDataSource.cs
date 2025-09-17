using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsGenerator.DataSources
{
    /// <summary>
    /// 中文数据源
    /// </summary>
    public class ChineseDataSource : IDataSource
    {
        public string[] LastNames => new[] { "王", "李", "张", "刘", "陈", "杨", "赵", "黄", "周", "吴" };
        
        public string[] FirstNames => new[] { "伟", "芳", "娜", "秀英", "敏", "静", "强", "磊", "军", "洋" };
        
        public string[] Companies => new[] { "科技有限公司", "信息技术有限公司", "互联网科技有限公司", "软件有限公司", "网络科技有限公司", "电子有限公司", "通信技术有限公司", "数据科技有限公司", "智能科技有限公司", "创新科技有限公司" };
        
        public string[] JobTitles => new[] { "软件工程师", "产品经理", "项目经理", "设计师", "市场经理", "销售经理", "人力资源经理", "财务经理", "运营经理", "客服经理" };
        
        public string[] Cities => new[] { "北京", "上海", "广州", "深圳", "杭州", "成都", "武汉", "南京", "西安", "重庆" };
        
        public string[] Streets => new[] { "人民大道", "中山路", "解放路", "北京路", "上海路", "广州路", "深圳路", "杭州路", "成都路", "武汉路" };
        
        public string[] PhonePrefixes => new[] { "13", "15", "17", "18" };
        
        public string[] EmailDomains => new[] { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "icloud.com" };
        
        public string[] EmailSeparators => new[] { "", ".", "_" };
    }
}