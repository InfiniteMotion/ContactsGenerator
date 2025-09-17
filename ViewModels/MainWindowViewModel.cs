using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactsGenerator.Services;
using ContactsGenerator.DataSources; // 添加数据源命名空间
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia;
using System.Linq;

namespace ContactsGenerator.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ContactGeneratorService _contactGeneratorService;

        [ObservableProperty]
        private int _contactCount = 10;

        [ObservableProperty]
        private string _selectedLanguage = "zh-CN";

        [ObservableProperty]
        private string _selectedFormat = "vcard";

        [ObservableProperty]
        private string _selectedVersion = "3.0";

        [ObservableProperty]
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        [ObservableProperty]
        private bool _isGenerating = false;

        [ObservableProperty]
        private string _statusMessage = "就绪";

        // 修改为动态加载支持的语言
        public List<string> Languages { get; } = GetSupportedLanguages();
        
        public List<string> Formats { get; } = new List<string> { "vcard", "csv" };
        public List<string> Versions { get; } = new List<string> { "2.1", "3.0", "4.0" };

        public ICommand GenerateContactsCommand { get; }
        public IAsyncRelayCommand ExportContactsCommand { get; }

        public MainWindowViewModel()
        {
            _contactGeneratorService = new ContactGeneratorService();
            GenerateContactsCommand = new RelayCommand(GenerateContacts);
            ExportContactsCommand = new AsyncRelayCommand(ExportContacts, CanExportContacts);
            PropertyChanged += OnPropertyChanged;
        }

        // 添加获取支持语言的方法
        private static List<string> GetSupportedLanguages()
        {
            // 通过反射获取所有支持的语言代码
            return new List<string> { "zh-CN", "en-US", "ja-JP" };
        }

        private void GenerateContacts()
        {
            IsGenerating = true;
            StatusMessage = "正在生成联系人...";

            try
            {
                var generatedContacts = _contactGeneratorService.GenerateContacts(ContactCount, SelectedLanguage);
                StatusMessage = $"生成了 {generatedContacts.Count} 个联系人，正在添加到列表...";
                
                // 添加调试信息
                if (generatedContacts.Count > 0)
                {
                    var firstContact = generatedContacts[0];
                    StatusMessage = $"第一个联系人: {firstContact.FullName}, {firstContact.PhoneNumber}, {firstContact.Email}";
                }
                
                // 重新创建ObservableCollection以确保UI更新
                Contacts = new ObservableCollection<Contact>(generatedContacts);
                
                StatusMessage = $"列表中现有 {Contacts.Count} 个联系人";
                
                // 通知导出命令检查是否可以执行
                ((IAsyncRelayCommand)ExportContactsCommand).NotifyCanExecuteChanged();
                StatusMessage = $"已生成 {ContactCount} 个联系人";
            }
            catch (System.Exception ex)
            {
                StatusMessage = $"生成失败: {ex.Message}";
            }
            finally
            {
                IsGenerating = false;
            }
        }

        private async Task ExportContacts()
        {
            try
            {
                string content = string.Empty;
                string extension = string.Empty;
                string fileName = string.Empty;

                if (SelectedFormat == "vcard")
                {
                    content = _contactGeneratorService.ExportToVCard(Contacts, SelectedVersion);
                    extension = "vcf";
                    fileName = $"contacts_{System.DateTime.Now:yyyyMMdd_HHmmss}.{extension}";
                }
                else if (SelectedFormat == "csv")
                {
                    content = _contactGeneratorService.ExportToCSV(Contacts);
                    extension = "csv";
                    fileName = $"contacts_{System.DateTime.Now:yyyyMMdd_HHmmss}.{extension}";
                }

                // 获取主窗口引用
                var mainWindow = (Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;

                if (mainWindow == null)
                {
                    StatusMessage = "导出失败: 无法获取主窗口";
                    return;
                }

                // 使用 StorageProvider 实现文件保存
                var storageProvider = mainWindow.StorageProvider;
                var filePickerSaveOptions = new FilePickerSaveOptions
                {
                    Title = "导出联系人",
                    SuggestedFileName = fileName,
                    FileTypeChoices = new[]
                    {
                        new FilePickerFileType($"{SelectedFormat.ToUpper()} 文件 (*.{extension})")
                        {
                            Patterns = new[] { $"*.{extension}" }
                        },
                        new FilePickerFileType("所有文件 (*.*)")
                        {
                            Patterns = new[] { "*.*" }
                        }
                    }
                };

                var result = await storageProvider.SaveFilePickerAsync(filePickerSaveOptions);

                if (result != null)
                {
                    using var stream = await result.OpenWriteAsync();
                    using var writer = new StreamWriter(stream);
                    await writer.WriteAsync(content);
                    StatusMessage = $"联系人已导出到 {result.Name}";
                }
            }
            catch (System.Exception ex)
            {
                StatusMessage = $"导出失败: {ex.Message}";
            }
        }

        private bool CanExportContacts()
        {
            return Contacts != null && Contacts.Count > 0;
        }

        
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Contacts))
            {
                // 直接调用 NotifyCanExecuteChanged 方法
                ((IAsyncRelayCommand)ExportContactsCommand).NotifyCanExecuteChanged();
            }
        }
    }
}