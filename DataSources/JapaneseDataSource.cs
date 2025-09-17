using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsGenerator.DataSources
{
    /// <summary>
    /// 日语数据源
    /// </summary>
    public class JapaneseDataSource : IDataSource
    {
        public string[] LastNames => new[] { "佐藤", "鈴木", "高橋", "田中", "渡辺", "伊藤", "山本", "中村", "小林", "加藤" };
        
        public string[] FirstNames => new[] { "太郎", "花子", "一郎", "次郎", "三郎", "美咲", "翔太", "葵", "大翔", "莉子" };
        
        public string[] Companies => new[] { "株式会社", "有限会社", "合名会社", "合同会社", "相互会社", "財団法人", "社団法人", "学校法人", "宗教法人", "独立行政法人" };
        
        public string[] JobTitles => new[] { "ソフトウェアエンジニア", "プロダクトマネージャー", "プロジェクトマネージャー", "デザイナー", "マーケティングマネージャー", "セールスマネージャー", "人事マネージャー", "財務マネージャー", "オペレーションマネージャー", "カスタマーサービスマネージャー" };
        
        public string[] Cities => new[] { "東京", "大阪", "名古屋", "横浜", "札幌", "福岡", "京都", "神戸", "広島", "仙台" };
        
        public string[] Streets => new[] { "一番街", "二番街", "三番街", "四番街", "五番街", "六番街", "七番街", "八番街", "九番街", "十番街" };
        
        public string[] PhonePrefixes => new[] { "080", "090", "070", "060" };
        
        public string[] EmailDomains => new[] { "gmail.com", "yahoo.co.jp", "outlook.com", "hotmail.co.jp", "icloud.com" };
        
        public string[] EmailSeparators => new[] { "", ".", "_" };
    }
}