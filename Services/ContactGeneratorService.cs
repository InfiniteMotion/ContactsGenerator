using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactsGenerator.DataSources; // 添加数据源命名空间

namespace ContactsGenerator.Services
{
    /// <summary>
    /// 联系人数据类
    /// </summary>
    public class Contact
    {
        public string FullName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
    }

    /// <summary>
    /// 联系人数据生成服务
    /// </summary>
    public class ContactGeneratorService
    {
        private readonly Random _random;
        // 删除原来的所有数据数组定义，改为使用数据源
        private IDataSource _dataSource;
        private string _currentLanguage = "zh-CN";
        
        public ContactGeneratorService()
        {
            _random = new Random();
        }

        /// <summary>
        /// 生成指定数量的联系人数据
        /// </summary>
        /// <param name="count">联系人数量</param>
        /// <param name="language">语言: "zh-CN" 中文, "en-US" 英文</param>
        /// <returns>联系人列表</returns>
        public List<Contact> GenerateContacts(int count, string language = "zh-CN")
        {
            // 根据语言获取相应的数据源
            if (_currentLanguage != language)
            {
                _dataSource = DataSourceFactory.GetDataSource(language);
                _currentLanguage = language;
            }
            else if (_dataSource == null)
            {
                _dataSource = DataSourceFactory.GetDataSource(language);
                _currentLanguage = language;
            }
            
            var contacts = new List<Contact>();
            
            for (int i = 0; i < count; i++)
            {
                contacts.Add(GenerateSingleContact(language));
            }
            
            return contacts;
        }

        /// <summary>
        /// 生成单个联系人数据
        /// </summary>
        /// <param name="language">语言</param>
        /// <returns>单个联系人</returns>
        private Contact GenerateSingleContact(string language)
        {
            // 确保数据源已初始化
            if (_currentLanguage != language || _dataSource == null)
            {
                _dataSource = DataSourceFactory.GetDataSource(language);
                _currentLanguage = language;
            }
            
            var contact = new Contact();
            
            if (language == "zh-CN")
            {
                // 生成中文联系人
                contact.LastName = _dataSource.LastNames[_random.Next(_dataSource.LastNames.Length)];
                contact.FirstName = _dataSource.FirstNames[_random.Next(_dataSource.FirstNames.Length)];
                contact.FullName = contact.LastName + contact.FirstName;
                contact.Company = contact.LastName + _dataSource.Companies[_random.Next(_dataSource.Companies.Length)];
                contact.JobTitle = _dataSource.JobTitles[_random.Next(_dataSource.JobTitles.Length)];
                contact.Address = $"{_dataSource.Cities[_random.Next(_dataSource.Cities.Length)]}{_dataSource.Streets[_random.Next(_dataSource.Streets.Length)]}{_random.Next(1, 1000)}号";
            }
            else
            {
                // 生成英文联系人
                contact.FirstName = _dataSource.FirstNames[_random.Next(_dataSource.FirstNames.Length)];
                contact.LastName = _dataSource.LastNames[_random.Next(_dataSource.LastNames.Length)];
                contact.FullName = $"{contact.FirstName} {contact.LastName}";
                contact.Company = _dataSource.Companies[_random.Next(_dataSource.Companies.Length)];
                contact.JobTitle = _dataSource.JobTitles[_random.Next(_dataSource.JobTitles.Length)];
                contact.Address = $"{_random.Next(1, 1000)} {_dataSource.Streets[_random.Next(_dataSource.Streets.Length)]}, {_dataSource.Cities[_random.Next(_dataSource.Cities.Length)]}";
            }
            
            // 生成电话号码
            contact.PhoneNumber = GeneratePhoneNumber(language);
            
            // 生成邮箱
            contact.Email = GenerateEmail(contact.FirstName, contact.LastName, contact.Company);
            
            return contact;
        }

        /// <summary>
        /// 生成电话号码
        /// </summary>
        /// <param name="language">语言</param>
        /// <returns>电话号码</returns>
        private string GeneratePhoneNumber(string language)
        {
            if (language == "zh-CN")
            {
                // 中国手机号码格式: 13x/15x/17x/18x xxxx xxxx
                var prefix = _dataSource.PhonePrefixes[_random.Next(_dataSource.PhonePrefixes.Length)];
                var rest = string.Concat(Enumerable.Range(0, 9).Select(_ => _random.Next(0, 10)));
                return $"{prefix}{rest}";
            }
            else
            {
                // 国际电话号码格式: +1 xxx xxx xxxx
                var countryCode = _dataSource.PhonePrefixes[_random.Next(_dataSource.PhonePrefixes.Length)];
                var part1 = string.Concat(Enumerable.Range(0, 3).Select(_ => _random.Next(0, 10)));
                var part2 = string.Concat(Enumerable.Range(0, 3).Select(_ => _random.Next(0, 10)));
                var part3 = string.Concat(Enumerable.Range(0, 4).Select(_ => _random.Next(0, 10)));
                return $"{countryCode} {part1} {part2} {part3}";
            }
        }

        /// <summary>
        /// 生成邮箱地址
        /// </summary>
        /// <param name="firstName">名字</param>
        /// <param name="lastName">姓氏</param>
        /// <param name="company">公司</param>
        /// <returns>邮箱地址</returns>
        private string GenerateEmail(string firstName, string lastName, string company)
        {
            var separator = _dataSource.EmailSeparators[_random.Next(_dataSource.EmailSeparators.Length)];
            var domain = _dataSource.EmailDomains[_random.Next(_dataSource.EmailDomains.Length)];
            
            // 去除公司名称中的特殊字符
            var cleanCompany = new string(company.Where(char.IsLetterOrDigit).ToArray()).ToLower();
            
            // 随机选择邮箱格式
            var format = _random.Next(3);
            string email;
            
            switch (format)
            {
                case 0:
                    email = $"{firstName.ToLower()}{separator}{lastName.ToLower()}@{domain}";
                    break;
                case 1:
                    email = $"{firstName.ToLower()[0]}{lastName.ToLower()}@{domain}";
                    break;
                case 2:
                    email = $"{firstName.ToLower()}{lastName.ToLower()[0]}@{domain}";
                    break;
                default:
                    email = $"{firstName.ToLower()}{separator}{lastName.ToLower()}@{domain}";
                    break;
            }
            
            return email;
        }

        /// <summary>
        /// 导出联系人为VCard格式字符串
        /// </summary>
        /// <param name="contacts">联系人列表</param>
        /// <param name="version">VCard版本</param>
        public string ExportToVCard(IEnumerable<Contact> contacts, string version = "3.0")
        {
            var sb = new StringBuilder();
            
            foreach (var contact in contacts)
            {
                sb.AppendLine("BEGIN:VCARD");
                sb.AppendLine($"VERSION:{version}");
                sb.AppendLine($"FN:{contact.FullName}");
                sb.AppendLine($"N:{contact.LastName};{contact.FirstName};;");
                sb.AppendLine($"TEL;CELL:{contact.PhoneNumber}");
                sb.AppendLine($"EMAIL:{contact.Email}");
                if (!string.IsNullOrEmpty(contact.Address))
                    sb.AppendLine($"ADR:;;{contact.Address}");
                if (!string.IsNullOrEmpty(contact.Company))
                    sb.AppendLine($"ORG:{contact.Company}");
                if (!string.IsNullOrEmpty(contact.JobTitle))
                    sb.AppendLine($"TITLE:{contact.JobTitle}");
                sb.AppendLine("END:VCARD");
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// 导出联系人为CSV格式字符串
        /// </summary>
        /// <param name="contacts">联系人列表</param>
        public string ExportToCSV(IEnumerable<Contact> contacts)
        {
            var sb = new StringBuilder();
            
            // 写入CSV头部
            sb.AppendLine("FullName,FirstName,LastName,PhoneNumber,Email,Address,Company,JobTitle");
            
            foreach (var contact in contacts)
            {
                // 处理CSV中的逗号和引号
                var fields = new[]
                {
                    EscapeCsvField(contact.FullName),
                    EscapeCsvField(contact.FirstName),
                    EscapeCsvField(contact.LastName),
                    EscapeCsvField(contact.PhoneNumber),
                    EscapeCsvField(contact.Email),
                    EscapeCsvField(contact.Address),
                    EscapeCsvField(contact.Company),
                    EscapeCsvField(contact.JobTitle)
                };
                
                sb.AppendLine(string.Join(",", fields));
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// 处理CSV中的特殊字符
        /// </summary>
        /// <param name="field">字段值</param>
        /// <returns>处理后的字段值</returns>
        private string EscapeCsvField(string field)
        {
            if (field == null)
                return "";
            
            // 如果字段包含逗号、引号或换行符，则需要用引号包围
            if (field.Contains(",") || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            {
                // 转义引号
                field = field.Replace("\"", "\"\"");
                // 用引号包围
                field = $"\"{field}\"";
            }
            
            return field;
        }
    }
}