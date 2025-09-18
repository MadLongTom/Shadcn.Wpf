# Shadcn.Wpf

<div align="right">
  <a href="README.md">English</a> | <strong>简体中文</strong>
</div>

[![.NET Version](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-Windows%20Presentation%20Foundation-0078D4?style=flat-square)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![License](https://img.shields.io/badge/license-MIT-green?style=flat-square)](LICENSE.txt)

一个基于 [shadcn/ui](https://ui.shadcn.com/) 设计理念的现代化 WPF 组件库，为 .NET 9 WPF 应用程序提供美观、一致且易用的 UI 组件集合。

## ✨ 特性

- 🎨 **现代化设计系统** - 遵循 shadcn/ui 设计规范，提供一致的视觉体验
- 🌈 **丰富的组件库** - 涵盖表单、导航、反馈、数据展示等多个类别的组件
- 🌓 **智能主题系统** - 支持亮色/暗色主题，可跟随系统设置自动切换
- ⚡ **流畅动画效果** - 内置精心设计的动画和过渡效果
- 🎯 **TypeScript 风格 API** - 清晰的属性命名和文档注释
- 🔧 **开箱即用** - 无需复杂配置，快速集成到现有项目
- 📱 **响应式设计** - 支持不同尺寸屏幕的自适应布局
- 🏗️ **MVVM 架构** - 完全兼容 MVVM 模式，支持数据绑定

## 🚀 快速开始

### 环境要求

- .NET 9.0 或更高版本
- Windows 10/11
- Visual Studio 2022 或 JetBrains Rider

### 安装

#### 1. 通过 Git 克隆

```bash
git clone https://github.com/MadLongTom/Shadcn.Wpf.git
cd Shadcn.Wpf
dotnet build
```

#### 2. 添加到现有项目

将 Shadcn.Wpf 项目作为项目引用添加到您的 WPF 应用程序中：

```xml
<ProjectReference Include="path\to\Shadcn.Wpf\Shadcn.Wpf.csproj" />
```

### 基本使用

#### 1. 添加命名空间

在您的 XAML 文件中添加命名空间引用：

```xaml
xmlns:shadcn="clr-namespace:Shadcn.Wpf.Controls;assembly=Shadcn.Wpf"
```

#### 2. 应用主题资源

在 App.xaml 中引入主题资源：

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <!-- 组件样式 -->
            <ResourceDictionary Source="pack://application:,,,/Shadcn.Wpf;component/Styles/ShadcnStyles.xaml"/>
            <!-- 主题资源 -->
            <ResourceDictionary Source="pack://application:,,,/Shadcn.Wpf;component/Themes/LightTheme.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

#### 3. 使用组件

```xaml
<StackPanel Margin="20" Spacing="10">
    <!-- 按钮组件 -->
    <shadcn:ShadcnButton 
        Content="点击我" 
        Variant="Primary" 
        Size="Default"/>
    
    <!-- 输入框组件 -->
    <shadcn:ShadcnTextBox 
        Placeholder="请输入内容..."
        MaxLength="100"/>
    
    <!-- 卡片组件 -->
    <shadcn:ShadcnCard Padding="20">
        <StackPanel>
            <TextBlock Text="卡片标题" 
                       FontWeight="SemiBold" 
                       FontSize="16" 
                       Margin="0,0,0,8"/>
            <TextBlock Text="卡片内容描述文本。"
                       Foreground="{DynamicResource MutedForegroundBrush}"/>
        </StackPanel>
    </shadcn:ShadcnCard>
</StackPanel>
```

## 📦 组件库

### 🧱 基础组件

| 组件 | 描述 | 特性 |
|------|------|------|
| **ShadcnButton** | 多变体按钮组件 | 7种样式变体、3种尺寸、支持图标 |
| **ShadcnTextBox** | 文本输入框 | 占位符、验证状态、清除按钮 |
| **ShadcnPasswordBox** | 密码输入框 | 密码显示/隐藏切换 |
| **ShadcnTextBlock** | 文本显示 | 多种排版样式 |

### 📝 表单组件

| 组件 | 描述 | 特性 |
|------|------|------|
| **ShadcnCheckBox** | 复选框 | 三态支持、自定义样式 |
| **ShadcnRadioButton** | 单选按钮 | 组管理、状态指示 |
| **ShadcnComboBox** | 下拉选择框 | 搜索过滤、自定义模板 |
| **ShadcnSelect** | 选择器 | 多选支持、标签显示 |
| **ShadcnDatePicker** | 日期选择器 | 日历弹窗、格式化显示 |
| **ShadcnCalendar** | 日历组件 | 日期范围选择、事件标记 |

### 🏗️ 布局组件

| 组件 | 描述 | 特性 |
|------|------|------|
| **ShadcnWindow** | 自定义窗口 | 无边框设计、阴影效果、标题栏定制 |
| **ShadcnCard** | 卡片容器 | 圆角边框、阴影效果、悬停状态 |
| **ShadcnTabControl** | 标签页控件 | 多种样式、动画切换 |

### 🧭 导航组件

| 组件 | 描述 | 特性 |
|------|------|------|
| **ShadcnNavMenu** | 导航菜单 | 层级结构、图标支持、折叠展开 |

### 💬 反馈组件

| 组件 | 描述 | 特性 |
|------|------|------|
| **ShadcnMessageDialog** | 消息对话框 | 多种类型、自定义按钮、动画效果 |
| **ShadcnProgressBar** | 进度条 | 确定/不确定进度、环形样式 |

### 📊 数据展示

| 组件 | 描述 | 特性 |
|------|------|------|
| **ShadcnListBox** | 列表框 | 虚拟化、多选、自定义项模板 |
| **ShadcnScrollBar** | 滚动条 | 现代化样式、平滑滚动 |

## 🎨 主题系统

### 主题管理器

Shadcn.Wpf 提供了智能的主题管理系统：

```csharp
// 获取主题管理器实例
var themeManager = ThemeManager.Instance;

// 设置亮色主题
themeManager.SetLightTheme();

// 设置暗色主题
themeManager.SetDarkTheme();

// 跟随系统主题
themeManager.SetSystemTheme();

// 切换主题
themeManager.ToggleTheme();

// 监听主题变化
themeManager.ThemeChanged += (sender, args) => 
{
    // 主题已更改
};
```

### 主题模式

- **Light** - 亮色主题
- **Dark** - 暗色主题  
- **System** - 跟随系统设置（默认）

### 颜色令牌

主题系统基于语义化的颜色令牌：

```xaml
<!-- 主要颜色 -->
{DynamicResource PrimaryBrush}
{DynamicResource PrimaryForegroundBrush}

<!-- 语义颜色 -->
{DynamicResource DestructiveBrush}
{DynamicResource SuccessBrush}
{DynamicResource WarningBrush}

<!-- 中性颜色 -->
{DynamicResource BackgroundBrush}
{DynamicResource ForegroundBrush}
{DynamicResource MutedBrush}
{DynamicResource BorderBrush}
```

## 🎯 按钮变体示例

```xaml
<!-- Primary - 主要操作 -->
<shadcn:ShadcnButton Content="确认" Variant="Primary"/>

<!-- Secondary - 次要操作 -->
<shadcn:ShadcnButton Content="取消" Variant="Secondary"/>

<!-- Destructive - 危险操作 -->
<shadcn:ShadcnButton Content="删除" Variant="Destructive"/>

<!-- Outline - 轮廓样式 -->
<shadcn:ShadcnButton Content="编辑" Variant="Outline"/>

<!-- Ghost - 幽灵样式 -->
<shadcn:ShadcnButton Content="查看" Variant="Ghost"/>

<!-- Link - 链接样式 -->
<shadcn:ShadcnButton Content="了解更多" Variant="Link"/>
```

## 🏗️ 项目架构

### 目录结构

```
📁 Shadcn.Wpf/                    # 组件库核心
├── 📁 Controls/                   # UI 控件实现
├── 📁 Converters/                 # 值转换器
├── 📁 Models/                     # 数据模型
├── 📁 Services/                   # 核心服务
├── 📁 Styles/                     # 样式资源
└── 📁 Themes/                     # 主题资源

📁 Shadcn.Wpf.Presentation/       # 演示应用
├── 📁 Pages/                      # 示例页面
├── 📁 ViewModels/                 # 视图模型
├── 📁 Services/                   # 业务服务
└── 📁 Styles/                     # 应用特定样式
```

### 设计原则

- **组件化** - 每个 UI 元素都是独立的、可复用的组件
- **一致性** - 统一的设计语言和交互模式
- **可访问性** - 支持键盘导航和屏幕阅读器
- **性能优化** - 虚拟化、延迟加载等性能优化策略
- **扩展性** - 易于扩展和自定义的架构设计

## 🤝 贡献指南

我们欢迎社区贡献！请遵循以下步骤：

### 报告问题

如果您发现了 bug 或有功能请求，请：

1. 检查 [Issues](https://github.com/MadLongTom/Shadcn.Wpf/issues) 中是否已存在相似问题
2. 使用合适的 Issue 模板创建新的 Issue
3. 提供详细的重现步骤和环境信息

### 贡献代码

1. **Fork** 本仓库
2. **创建分支**: `git checkout -b feature/AmazingFeature`
3. **编写代码**: 遵循项目的代码规范
4. **添加测试**: 确保新功能有相应的测试
5. **提交更改**: `git commit -m 'Add some AmazingFeature'`
6. **推送分支**: `git push origin feature/AmazingFeature`
7. **创建 Pull Request**

### 代码规范

- 使用 C# 11+ 语法特性
- 遵循 .NET 命名约定
- 为公共 API 添加 XML 文档注释
- 保持代码简洁和可读性

## 📄 许可证

本项目基于 [MIT 许可证](LICENSE.txt) 开源。

## 🙏 致谢

- [shadcn/ui](https://ui.shadcn.com/) - 设计灵感来源
- [Radix UI](https://www.radix-ui.com/) - 组件架构参考
- [Material Design](https://material.io/) - 设计原则参考

## 📞 支持与联系

- 🐛 [报告 Bug](https://github.com/MadLongTom/Shadcn.Wpf/issues/new?template=bug_report.md)
- 💡 [功能请求](https://github.com/MadLongTom/Shadcn.Wpf/issues/new?template=feature_request.md)
- 💬 [讨论交流](https://github.com/MadLongTom/Shadcn.Wpf/discussions)

---

<div align="center">

**如果这个项目对您有帮助，请给个 ⭐ Star 支持一下！**

Made with ❤️ for the WPF community

</div>