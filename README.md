# ContactsGenerator - Avalonia MVVM 应用

这是一个基于Avalonia框架和MVVM模式的桌面应用程序。

## 技术栈
- .NET 8.0
- Avalonia 11.3.6
- CommunityToolkit.Mvvm

## 项目结构
```
├── App.axaml/axaml.cs - 应用程序入口点
├── Assets/ - 资源文件
├── Models/ - 数据模型
├── ViewModels/ - 视图模型
└── Views/ - 视图
```

## 如何运行
1. 确保已安装.NET 8.0 SDK
2. 运行以下命令：
   ```
   dotnet restore
   dotnet build
   dotnet run
   ```

## 功能
- 基于MVVM模式的架构
- 使用Avalonia UI框架构建跨平台桌面应用
- 支持Windows、Linux和macOS平台
