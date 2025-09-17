using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsGenerator.DataSources
{
    /// <summary>
    /// 英文数据源
    /// </summary>
    public class EnglishDataSource : IDataSource
    {
        public string[] LastNames => new[] { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };
        
        public string[] FirstNames => new[] { "John", "Jane", "Mike", "Sarah", "David", "Emily", "Michael", "Emma", "James", "Olivia" };
        
        public string[] Companies => new[] { "Tech Inc.", "Information Technology Co.", "Internet Tech Co.", "Software Ltd.", "Network Tech Inc.", "Electronics Co.", "Communication Tech Co.", "Data Tech Ltd.", "Smart Tech Inc.", "Innovation Tech Co." };
        
        public string[] JobTitles => new[] { "Software Engineer", "Product Manager", "Project Manager", "Designer", "Marketing Manager", "Sales Manager", "HR Manager", "Finance Manager", "Operations Manager", "Customer Service Manager" };
        
        public string[] Cities => new[] { "Beijing", "Shanghai", "Guangzhou", "Shenzhen", "Hangzhou", "Chengdu", "Wuhan", "Nanjing", "Xi'an", "Chongqing" };
        
        public string[] Streets => new[] { "Main Street", "Park Avenue", "Ocean Drive", "Maple Road", "Oak Street", "Pine Avenue", "Cedar Lane", "Elm Street", "Birch Road", "Willow Avenue" };
        
        public string[] PhonePrefixes => new[] { "+1", "+44", "+33", "+49", "+81", "+86" };
        
        public string[] EmailDomains => new[] { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "icloud.com" };
        
        public string[] EmailSeparators => new[] { "", ".", "_" };
    }
}