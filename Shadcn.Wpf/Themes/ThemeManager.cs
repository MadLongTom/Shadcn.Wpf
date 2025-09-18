using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Win32;

namespace Shadcn.Wpf.Themes;

public enum Theme
{
    Light,
    Dark,
    System
}

public class ThemeManager : INotifyPropertyChanged
{
    private static ThemeManager? _instance;
    private Theme _currentTheme = Theme.System;
    private bool _systemDarkMode;

    public static ThemeManager Instance => _instance ??= new ThemeManager();

    /// <summary>
    /// 当前主题设置（Light, Dark, 或 System）
    /// </summary>
    public Theme CurrentTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme != value)
            {
                _currentTheme = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDarkTheme));
                OnPropertyChanged(nameof(EffectiveTheme));
                ApplyTheme();
                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// 实际生效的主题（Light 或 Dark）
    /// </summary>
    public Theme EffectiveTheme => _currentTheme == Theme.System 
        ? (_systemDarkMode ? Theme.Dark : Theme.Light) 
        : _currentTheme;

    /// <summary>
    /// 当前是否为深色主题（兼容性属性）
    /// </summary>
    public bool IsDarkTheme
    {
        get => EffectiveTheme == Theme.Dark;
        set => CurrentTheme = value ? Theme.Dark : Theme.Light;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler? ThemeChanged;

    private ThemeManager()
    {
        // 初始化系统主题检测
        _systemDarkMode = IsSystemDarkTheme();
        
        // 监听系统主题变化
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
        
        // 默认跟随系统主题
        _currentTheme = Theme.System;
        
        // 注意：不在构造函数中调用 ApplyTheme()，因为 Application.Current 可能还没有准备好
        // 主题将在 App.OnStartup 中应用
    }

    ~ThemeManager()
    {
        // 清理事件监听
        SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
    }

    /// <summary>
    /// 切换主题（Light -> Dark -> System -> Light）
    /// </summary>
    public void ToggleTheme()
    {
        CurrentTheme = _currentTheme switch
        {
            Theme.Light => Theme.Dark,
            Theme.Dark => Theme.System,
            Theme.System => Theme.Light,
            _ => Theme.Light
        };
    }

    /// <summary>
    /// 设置为浅色主题
    /// </summary>
    public void SetLightTheme()
    {
        CurrentTheme = Theme.Light;
    }

    /// <summary>
    /// 设置为深色主题
    /// </summary>
    public void SetDarkTheme()
    {
        CurrentTheme = Theme.Dark;
    }

    /// <summary>
    /// 设置为跟随系统主题
    /// </summary>
    public void SetSystemTheme()
    {
        CurrentTheme = Theme.System;
    }

    /// <summary>
    /// 强制重新应用当前主题
    /// </summary>
    public void RefreshTheme()
    {
        ApplyTheme();
    }

    /// <summary>
    /// 检测系统是否为深色主题
    /// </summary>
    private bool IsSystemDarkTheme()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");
            var isDark = value is int intValue && intValue == 0;
            
            // 调试信息
            System.Diagnostics.Debug.WriteLine($"System theme detection: AppsUseLightTheme = {value}, IsDark = {isDark}");
            
            return isDark;
        }
        catch (Exception ex)
        {
            // 如果无法读取注册表，默认返回浅色主题
            System.Diagnostics.Debug.WriteLine($"Failed to detect system theme: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// 处理系统用户偏好设置变化
    /// </summary>
    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category == UserPreferenceCategory.General)
        {
            var newSystemDarkMode = IsSystemDarkTheme();
            if (_systemDarkMode != newSystemDarkMode)
            {
                _systemDarkMode = newSystemDarkMode;
                
                // 如果当前设置为跟随系统主题，则触发主题更新
                if (_currentTheme == Theme.System)
                {
                    OnPropertyChanged(nameof(EffectiveTheme));
                    OnPropertyChanged(nameof(IsDarkTheme));
                    ApplyTheme();
                    ThemeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    private void ApplyTheme()
    {
        var application = Application.Current;
        if (application?.Resources == null) 
        {
            System.Diagnostics.Debug.WriteLine("ApplyTheme: Application or Resources is null");
            return;
        }

        System.Diagnostics.Debug.WriteLine($"ApplyTheme: CurrentTheme = {CurrentTheme}, EffectiveTheme = {EffectiveTheme}, SystemDarkMode = {_systemDarkMode}");

        // Remove existing theme resources
        var resourcesToRemove = new List<ResourceDictionary>();
        foreach (ResourceDictionary resource in application.Resources.MergedDictionaries)
        {
            if (resource.Source?.ToString().Contains("Theme") == true)
            {
                resourcesToRemove.Add(resource);
            }
        }

        foreach (var resource in resourcesToRemove)
        {
            application.Resources.MergedDictionaries.Remove(resource);
        }

        // Add new theme resource based on effective theme
        var themeUri = EffectiveTheme == Theme.Dark
            ? new Uri("pack://application:,,,/Themes/DarkTheme.xaml", UriKind.Absolute)
            : new Uri("pack://application:,,,/Themes/LightTheme.xaml", UriKind.Absolute);

        System.Diagnostics.Debug.WriteLine($"ApplyTheme: Loading theme from {themeUri}");

        application.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = themeUri });
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
