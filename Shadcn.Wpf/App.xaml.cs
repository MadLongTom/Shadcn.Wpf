using System.Windows;
using Shadcn.Wpf.Configuration;
using Shadcn.Wpf.Themes;

namespace Shadcn.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // Initialize dependency injection container
        ServiceConfiguration.InitializeContainer();
        
        // Initialize theme manager - now defaults to following system theme
        var themeManager = ThemeManager.Instance;
        
        // 确保主题正确应用（在 Application 完全初始化后）
        this.Dispatcher.BeginInvoke(() =>
        {
            // 强制应用当前主题以确保系统主题被正确检测和应用
            themeManager.RefreshTheme();
        }, System.Windows.Threading.DispatcherPriority.Loaded);
        
        // Theme manager will automatically detect and follow system theme
        // You can still manually set themes if needed:
        // themeManager.CurrentTheme = Theme.Light;  // Force light theme
        // themeManager.CurrentTheme = Theme.Dark;   // Force dark theme
        // themeManager.CurrentTheme = Theme.System; // Follow system (default)
    }
}