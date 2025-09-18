# Shadcn.Wpf

<div style="display: flex; align-items: center; justify-content: space-between;">
  <span>
    <a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square" alt=".NET Version"/></a>
    <a href="https://docs.microsoft.com/en-us/dotnet/desktop/wpf/"><img src="https://img.shields.io/badge/WPF-Windows%20Presentation%20Foundation-0078D4?style=flat-square" alt="WPF"/></a>
    <a href="LICENSE.txt"><img src="https://img.shields.io/badge/license-MIT-green?style=flat-square" alt="License"/></a>
  </span>
  <span>
    <strong>English</strong> | <a href="README.zh-CN.md">简体中文</a>
  </span>
</div>

A modern WPF component library inspired by [shadcn/ui](https://ui.shadcn.com/) design principles, providing beautiful, consistent, and easy-to-use UI components for .NET 9 WPF applications.

## ✨ Features

- 🎨 **Modern Design System** - Following shadcn/ui design standards for consistent visual experience
- 🌈 **Rich Component Library** - Comprehensive components covering forms, navigation, feedback, and data display
- 🌓 **Smart Theme System** - Support for light/dark themes with automatic system theme following
- ⚡ **Smooth Animations** - Built-in carefully crafted animations and transitions
- 🎯 **TypeScript-style API** - Clear property naming and comprehensive documentation
- 🔧 **Ready to Use** - No complex configuration needed, quick integration into existing projects
- 📱 **Responsive Design** - Adaptive layouts for different screen sizes
- 🏗️ **MVVM Architecture** - Full MVVM pattern compatibility with data binding support

## 🚀 Quick Start

### Requirements

- .NET 9.0 or higher
- Windows 10/11
- Visual Studio 2022 or JetBrains Rider

### Installation

#### 1. Clone via Git

```bash
git clone https://github.com/MadLongTom/Shadcn.Wpf.git
cd Shadcn.Wpf
dotnet build
```

#### 2. Add to Existing Project

Add the Shadcn.Wpf project as a project reference to your WPF application:

```xml
<ProjectReference Include="path\to\Shadcn.Wpf\Shadcn.Wpf.csproj" />
```

### Basic Usage

#### 1. Add Namespace

Add namespace reference in your XAML files:

```xaml
xmlns:shadcn="clr-namespace:Shadcn.Wpf.Controls;assembly=Shadcn.Wpf"
```

#### 2. Apply Theme Resources

Include theme resources in App.xaml:

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <!-- Component Styles -->
            <ResourceDictionary Source="pack://application:,,,/Shadcn.Wpf;component/Styles/ShadcnStyles.xaml"/>
            <!-- Theme Resources -->
            <ResourceDictionary Source="pack://application:,,,/Shadcn.Wpf;component/Themes/LightTheme.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

#### 3. Use Components

```xaml
<StackPanel Margin="20" Spacing="10">
    <!-- Button Component -->
    <shadcn:ShadcnButton 
        Content="Click Me" 
        Variant="Primary" 
        Size="Default"/>
    
    <!-- TextBox Component -->
    <shadcn:ShadcnTextBox 
        Placeholder="Enter text..."
        MaxLength="100"/>
    
    <!-- Card Component -->
    <shadcn:ShadcnCard Padding="20">
        <StackPanel>
            <TextBlock Text="Card Title" 
                       FontWeight="SemiBold" 
                       FontSize="16" 
                       Margin="0,0,0,8"/>
            <TextBlock Text="Card content description text."
                       Foreground="{DynamicResource MutedForegroundBrush}"/>
        </StackPanel>
    </shadcn:ShadcnCard>
</StackPanel>
```

## 📦 Component Library

### 🧱 Basic Components

| Component | Description | Features |
|-----------|-------------|----------|
| **ShadcnButton** | Multi-variant button component | 7 style variants, 3 sizes, icon support |
| **ShadcnTextBox** | Text input field | Placeholder, validation states, clear button |
| **ShadcnPasswordBox** | Password input field | Show/hide password toggle |
| **ShadcnTextBlock** | Text display | Multiple typography styles |

### 📝 Form Components

| Component | Description | Features |
|-----------|-------------|----------|
| **ShadcnCheckBox** | Checkbox | Three-state support, custom styles |
| **ShadcnRadioButton** | Radio button | Group management, state indication |
| **ShadcnComboBox** | Dropdown select | Search filtering, custom templates |
| **ShadcnSelect** | Selector | Multi-select support, tag display |
| **ShadcnDatePicker** | Date picker | Calendar popup, formatted display |
| **ShadcnCalendar** | Calendar component | Date range selection, event marking |

### 🏗️ Layout Components

| Component | Description | Features |
|-----------|-------------|----------|
| **ShadcnWindow** | Custom window | Borderless design, shadow effects, title bar customization |
| **ShadcnCard** | Card container | Rounded borders, shadow effects, hover states |
| **ShadcnTabControl** | Tab control | Multiple styles, animated transitions |

### 🧭 Navigation Components

| Component | Description | Features |
|-----------|-------------|----------|
| **ShadcnNavMenu** | Navigation menu | Hierarchical structure, icon support, collapsible |

### 💬 Feedback Components

| Component | Description | Features |
|-----------|-------------|----------|
| **ShadcnMessageDialog** | Message dialog | Multiple types, custom buttons, animation effects |
| **ShadcnProgressBar** | Progress bar | Determinate/indeterminate progress, circular style |

### 📊 Data Display

| Component | Description | Features |
|-----------|-------------|----------|
| **ShadcnListBox** | List box | Virtualization, multi-select, custom item templates |
| **ShadcnScrollBar** | Scroll bar | Modern styling, smooth scrolling |

## 🎨 Theme System

### Theme Manager

Shadcn.Wpf provides an intelligent theme management system:

```csharp
// Get theme manager instance
var themeManager = ThemeManager.Instance;

// Set light theme
themeManager.SetLightTheme();

// Set dark theme
themeManager.SetDarkTheme();

// Follow system theme
themeManager.SetSystemTheme();

// Toggle theme
themeManager.ToggleTheme();

// Listen to theme changes
themeManager.ThemeChanged += (sender, args) => 
{
    // Theme has changed
};
```

### Theme Modes

- **Light** - Light theme
- **Dark** - Dark theme  
- **System** - Follow system settings (default)

### Color Tokens

The theme system is based on semantic color tokens:

```xaml
<!-- Primary Colors -->
{DynamicResource PrimaryBrush}
{DynamicResource PrimaryForegroundBrush}

<!-- Semantic Colors -->
{DynamicResource DestructiveBrush}
{DynamicResource SuccessBrush}
{DynamicResource WarningBrush}

<!-- Neutral Colors -->
{DynamicResource BackgroundBrush}
{DynamicResource ForegroundBrush}
{DynamicResource MutedBrush}
{DynamicResource BorderBrush}
```

## 🎯 Button Variant Examples

```xaml
<!-- Primary - Main actions -->
<shadcn:ShadcnButton Content="Confirm" Variant="Primary"/>

<!-- Secondary - Secondary actions -->
<shadcn:ShadcnButton Content="Cancel" Variant="Secondary"/>

<!-- Destructive - Dangerous actions -->
<shadcn:ShadcnButton Content="Delete" Variant="Destructive"/>

<!-- Outline - Outline style -->
<shadcn:ShadcnButton Content="Edit" Variant="Outline"/>

<!-- Ghost - Ghost style -->
<shadcn:ShadcnButton Content="View" Variant="Ghost"/>

<!-- Link - Link style -->
<shadcn:ShadcnButton Content="Learn More" Variant="Link"/>
```

## 🏗️ Project Architecture

### Directory Structure

```
📁 Shadcn.Wpf/                    # Component library core
├── 📁 Controls/                   # UI control implementations
├── 📁 Converters/                 # Value converters
├── 📁 Models/                     # Data models
├── 📁 Services/                   # Core services
├── 📁 Styles/                     # Style resources
└── 📁 Themes/                     # Theme resources

📁 Shadcn.Wpf.Presentation/       # Demo application
├── 📁 Pages/                      # Example pages
├── 📁 ViewModels/                 # View models
├── 📁 Services/                   # Business services
└── 📁 Styles/                     # Application-specific styles
```

### Design Principles

- **Component-based** - Each UI element is an independent, reusable component
- **Consistency** - Unified design language and interaction patterns
- **Accessibility** - Support for keyboard navigation and screen readers
- **Performance** - Virtualization, lazy loading, and other performance optimizations
- **Extensibility** - Easy-to-extend and customizable architecture

## 🤝 Contributing

We welcome community contributions! Please follow these steps:

### Reporting Issues

If you find a bug or have a feature request:

1. Check if a similar issue already exists in [Issues](https://github.com/MadLongTom/Shadcn.Wpf/issues)
2. Create a new Issue using the appropriate template
3. Provide detailed reproduction steps and environment information

### Contributing Code

1. **Fork** this repository
2. **Create branch**: `git checkout -b feature/AmazingFeature`
3. **Write code**: Follow the project's coding standards
4. **Add tests**: Ensure new features have corresponding tests
5. **Commit changes**: `git commit -m 'Add some AmazingFeature'`
6. **Push branch**: `git push origin feature/AmazingFeature`
7. **Create Pull Request**

### Coding Standards

- Use C# 11+ syntax features
- Follow .NET naming conventions
- Add XML documentation comments for public APIs
- Keep code clean and readable

## 📄 License

This project is licensed under the [MIT License](LICENSE.txt).

## 🙏 Acknowledgments

- [shadcn/ui](https://ui.shadcn.com/) - Design inspiration
- [Radix UI](https://www.radix-ui.com/) - Component architecture reference
- [Material Design](https://material.io/) - Design principles reference

## 📞 Support & Contact

- 🐛 [Report Bug](https://github.com/MadLongTom/Shadcn.Wpf/issues/new?template=bug_report.md)
- 💡 [Feature Request](https://github.com/MadLongTom/Shadcn.Wpf/issues/new?template=feature_request.md)
- 💬 [Discussions](https://github.com/MadLongTom/Shadcn.Wpf/discussions)

---

<div align="center">

**If this project helps you, please give it a ⭐ Star!**

Made with ❤️ for the WPF community

</div>