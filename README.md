# Shadcn.Wpf

一个基于 shadcn/ui 设计理念的 WPF 组件库，为 WPF 应用程序提供现代化、美观的 UI 组件。

## 特性

✨ **现代化设计** - 基于 shadcn/ui 的设计系统，提供一致的视觉体验  
🎨 **丰富的组件** - 包含常用的 UI 组件，如按钮、输入框、卡片、对话框等  
🌗 **主题支持** - 支持亮色和暗色主题  
🎭 **动画效果** - 内置流畅的动画和过渡效果  
📱 **响应式设计** - 适配不同尺寸的窗口和屏幕  
🔧 **易于使用** - 简洁的 API 设计，开箱即用  

## 技术栈

- **.NET 9.0** - 最新的 .NET 框架
- **WPF** - Windows Presentation Foundation
- **CommunityToolkit.Mvvm** - MVVM 模式支持
- **Microsoft.Extensions.Hosting** - 依赖注入和服务管理

## 组件列表

### 基础组件
- **ShadcnButton** - 多种样式的按钮组件（Default, Primary, Destructive, Outline, Secondary, Ghost, Link）
- **ShadcnTextBox** - 输入框组件
- **ShadcnPasswordBox** - 密码输入框
- **ShadcnTextBlock** - 文本显示组件

### 表单组件
- **ShadcnCheckBox** - 复选框
- **ShadcnRadioButton** - 单选按钮
- **ShadcnComboBox** - 下拉选择框
- **ShadcnSelect** - 选择器
- **ShadcnDatePicker** - 日期选择器
- **ShadcnCalendar** - 日历组件

### 容器组件
- **ShadcnWindow** - 自定义窗口容器
- **ShadcnCard** - 卡片容器
- **ShadcnTabControl** - 标签页控件

### 导航组件
- **ShadcnNavMenu** - 导航菜单

### 反馈组件
- **ShadcnMessageDialog** - 消息对话框
- **ShadcnProgressBar** - 进度条

### 其他组件
- **ShadcnListBox** - 列表框
- **ShadcnScrollBar** - 滚动条

## 快速开始

### 安装

1. 克隆项目到本地：
```bash
git clone https://github.com/MadLongTom/Shadcn.Wpf.git
```

2. 使用 Visual Studio 或 JetBrains Rider 打开 `Shadcn.Wpf.sln` 解决方案文件

3. 构建项目：
```bash
dotnet build
```

### 基本使用

在你的 WPF 项目中引用 Shadcn.Wpf，然后在 XAML 中使用组件：

```xaml
<Window x:Class="YourApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:controls="clr-namespace:Shadcn.Wpf.Controls;assembly=Shadcn.Wpf">
    
    <StackPanel Margin="20">
        <!-- 按钮示例 -->
        <controls:ShadcnButton 
            Content="Primary Button" 
            Variant="Primary" 
            Size="Default" 
            Margin="0,5"/>
        
        <!-- 输入框示例 -->
        <controls:ShadcnTextBox 
            Placeholder="请输入内容..." 
            Margin="0,5"/>
        
        <!-- 卡片示例 -->
        <controls:ShadcnCard Margin="0,10">
            <StackPanel>
                <TextBlock Text="卡片标题" FontWeight="Bold" Margin="0,0,0,10"/>
                <TextBlock Text="这是卡片的内容区域。"/>
            </StackPanel>
        </controls:ShadcnCard>
    </StackPanel>
</Window>
```

### 使用自定义窗口

```xaml
<controls:ShadcnWindow x:Class="YourApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:controls="clr-namespace:Shadcn.Wpf.Controls;assembly=Shadcn.Wpf"
        Title="My App" 
        Height="600" 
        Width="800"
        ShowTitleBar="True"
        ShowSystemButtons="True"
        EnableDropShadow="True"
        EnableAnimations="True">
    
    <!-- 窗口内容 -->
    
</controls:ShadcnWindow>
```

## 按钮变体示例

```xaml
<!-- 不同的按钮样式 -->
<controls:ShadcnButton Content="Default" Variant="Default"/>
<controls:ShadcnButton Content="Primary" Variant="Primary"/>
<controls:ShadcnButton Content="Destructive" Variant="Destructive"/>
<controls:ShadcnButton Content="Outline" Variant="Outline"/>
<controls:ShadcnButton Content="Secondary" Variant="Secondary"/>
<controls:ShadcnButton Content="Ghost" Variant="Ghost"/>
<controls:ShadcnButton Content="Link" Variant="Link"/>
```

## 项目结构

```
Shadcn.Wpf/
├── Commands/          # 命令相关
├── Configuration/     # 配置管理
├── Controls/          # UI 控件
├── Converters/        # 值转换器
├── Models/           # 数据模型
├── Pages/            # 页面
├── Services/         # 业务服务
├── Styles/           # 样式资源
├── Themes/           # 主题资源
└── ViewModels/       # 视图模型
```

## 贡献

欢迎贡献代码！请遵循以下步骤：

1. Fork 本项目
2. 创建你的特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交你的更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 开启一个 Pull Request

## 许可证

本项目采用 MIT 许可证 - 查看 [LICENSE.txt](LICENSE.txt) 文件了解详情。

## 支持

如果您在使用过程中遇到问题，请通过以下方式获取帮助：

- 🐛 [报告 Bug](https://github.com/MadLongTom/Shadcn.Wpf/issues)
- 💡 [功能请求](https://github.com/MadLongTom/Shadcn.Wpf/issues)

---

**如果这个项目对您有帮助，请给个 ⭐ Star 支持一下！**