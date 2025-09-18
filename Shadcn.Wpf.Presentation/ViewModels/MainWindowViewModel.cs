using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Shadcn.Wpf.Models;
using Shadcn.Wpf.Presentation.Services;
using Shadcn.Wpf.Services;
using Shadcn.Wpf.Themes;

namespace Shadcn.Wpf.Presentation.ViewModels;

/// <summary>
/// ViewModel for the MainWindow
/// </summary>
public partial class MainWindowViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private string _statusMessage = "Welcome to Shadcn.Wpf";

    [ObservableProperty]
    private ObservableCollection<NavigationItem> _navigationItems = new();

    [ObservableProperty]
    private string _currentTheme = "";

    [ObservableProperty]
    private string _effectiveTheme = "";

    public MainWindowViewModel(INavigationService navigationService, IMessageService messageService)
    {
        _navigationService = navigationService;
        _messageService = messageService;
        
        // Subscribe to theme changes
        ThemeManager.Instance.PropertyChanged += OnThemeManagerPropertyChanged;
        
        // Subscribe to navigation events
        _navigationService.NavigationOccurred += OnNavigationOccurred;
        
        InitializeNavigationItems();
        UpdateCurrentTheme();
        UpdateEffectiveTheme();
        
        // Set initial status message for debugging
        StatusMessage = $"ViewModel initialized with {NavigationItems?.Count ?? 0} navigation items";
    }

    /// <summary>
    /// Initialize navigation after the service is ready
    /// </summary>
    public void InitializeNavigation()
    {
        // Set initial page after navigation service is initialized
        NavigateToPageCommand.Execute("home");
    }

    /// <summary>
    /// Command to navigate to a page
    /// </summary>
    [RelayCommand]
    private void NavigateToPage(string pageTag)
    {
        try
        {
            _navigationService.NavigateToPage(pageTag);
            
            // Update navigation selection
            UpdateNavigationSelection(pageTag);
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Failed to navigate to page: {ex.Message}", "Navigation Error");
        }
    }

    /// <summary>
    /// Command to toggle theme
    /// </summary>
    [RelayCommand]
    private void ToggleTheme()
    {
        try
        {
            ThemeManager.Instance.ToggleTheme();
            StatusMessage = $"Theme: {CurrentTheme} (Effective: {EffectiveTheme})";
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Failed to toggle theme: {ex.Message}", "Theme Error");
        }
    }

    /// <summary>
    /// Command to set light theme
    /// </summary>
    [RelayCommand]
    private void SetLightTheme()
    {
        try
        {
            ThemeManager.Instance.SetLightTheme();
            StatusMessage = $"Theme set to Light";
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Failed to set light theme: {ex.Message}", "Theme Error");
        }
    }

    /// <summary>
    /// Command to set dark theme
    /// </summary>
    [RelayCommand]
    private void SetDarkTheme()
    {
        try
        {
            ThemeManager.Instance.SetDarkTheme();
            StatusMessage = $"Theme set to Dark";
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Failed to set dark theme: {ex.Message}", "Theme Error");
        }
    }

    /// <summary>
    /// Command to set system theme
    /// </summary>
    [RelayCommand]
    private void SetSystemTheme()
    {
        try
        {
            ThemeManager.Instance.SetSystemTheme();
            StatusMessage = $"Theme set to follow system (Currently: {EffectiveTheme})";
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Failed to set system theme: {ex.Message}", "Theme Error");
        }
    }

    /// <summary>
    /// Command to handle navigation item selection
    /// </summary>
    [RelayCommand]
    private void OnNavigationItemSelected(NavigationItem selectedItem)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"OnNavigationItemSelected called with: {selectedItem?.Title ?? "null"}");
            
            // Show immediate feedback in status
            StatusMessage = $"[DEBUG] OnNavigationItemSelected called for: {selectedItem?.Title ?? "null"}";
            
            if (selectedItem?.Tag is string tag && !string.IsNullOrEmpty(tag))
            {
                StatusMessage = $"Navigating to: {selectedItem.Title}";
                NavigateToPageCommand.Execute(tag);
            }
            else if (selectedItem != null)
            {
                StatusMessage = $"Clicked: {selectedItem.Title} (no tag)";
            }
            else
            {
                StatusMessage = "Invalid navigation item selected";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Navigation error: {ex.Message}";
            _messageService.ShowError($"Navigation failed: {ex.Message}", "Navigation Error");
        }
    }

    /// <summary>
    /// Initialize navigation items
    /// </summary>
    private void InitializeNavigationItems()
    {
        System.Diagnostics.Debug.WriteLine("InitializeNavigationItems called");
        
        NavigationItems = new ObservableCollection<NavigationItem>
        {
            new()
            {
                Icon = GetIcon("HomeIcon"),
                Title = "Home",
                Tag = "home",
                IsSelected = true
            },
            new()
            {
                Icon = GetIcon("DashboardIcon"),
                Title = "Components",
                Children =
                {
                    new() { Icon = GetIcon("AddIcon"), Title = "Buttons", Tag = "buttons" },
                    new() { Icon = GetIcon("DocumentIcon"), Title = "Cards", Tag = "cards" },
                    new() { Icon = GetIcon("EditIcon"), Title = "Forms", Tag = "forms" },
                    new() { Icon = GetIcon("CheckIcon"), Title = "RadioButton", Tag = "radiobutton" },
                    new() { Icon = GetIcon("RefreshIcon"), Title = "Progress", Tag = "progress" },
                    new() { Icon = GetIcon("FolderIcon"), Title = "TabControl", Tag = "tabcontrol" },
                    new() { Icon = GetIcon("FileIcon"), Title = "ListBox", Tag = "listbox" },
                    new() { Icon = GetIcon("CalendarIcon"), Title = "DatePicker", Tag = "datepicker" },
                    new() { Icon = GetIcon("CheckIcon"), Title = "ToggleSwitch", Tag = "toggleswitch" },
                    new() { Icon = GetIcon("EditIcon"), Title = "Typography", Tag = "typography" },
                    new() { Icon = GetIcon("MenuIcon"), Title = "Navigation", Tag = "navigation" }
                }
            },
            new()
            {
                Icon = GetIcon("InfoIcon"),
                Title = "About",
                Tag = "about"
            }
        };
        
        System.Diagnostics.Debug.WriteLine($"Created {NavigationItems.Count} navigation items");
    }
    
    /// <summary>
    /// Get icon geometry from resources
    /// </summary>
    private Geometry? GetIcon(string iconKey)
    {
        try
        {
            return Application.Current?.FindResource(iconKey) as Geometry;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Update navigation selection based on page tag
    /// </summary>
    private void UpdateNavigationSelection(string pageTag)
    {
        // Deselect all items first
        foreach (var item in NavigationItems)
        {
            item.DeselectAll();
        }

        // Select the appropriate item
        foreach (var item in NavigationItems)
        {
            if (item.SelectByTag(pageTag))
            {
                break;
            }
        }
    }

    /// <summary>
    /// Handle theme manager property changes
    /// </summary>
    private void OnThemeManagerPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeManager.CurrentTheme))
        {
            UpdateCurrentTheme();
        }
        else if (e.PropertyName == nameof(ThemeManager.EffectiveTheme))
        {
            UpdateEffectiveTheme();
        }
    }

    /// <summary>
    /// Update current theme property
    /// </summary>
    private void UpdateCurrentTheme()
    {
        CurrentTheme = ThemeManager.Instance.CurrentTheme.ToString();
    }

    /// <summary>
    /// Update effective theme property
    /// </summary>
    private void UpdateEffectiveTheme()
    {
        EffectiveTheme = ThemeManager.Instance.EffectiveTheme.ToString();
    }

    /// <summary>
    /// Handle navigation events
    /// </summary>
    private void OnNavigationOccurred(object? sender, NavigationEventArgs e)
    {
        // Apply page transition animation
        if (e.Page is FrameworkElement page)
        {
            ApplyPageTransition(page);
        }
    }

    /// <summary>
    /// Apply page transition animation
    /// </summary>
    private static void ApplyPageTransition(FrameworkElement page)
    {
        page.Opacity = 0;
        page.Margin = new Thickness(20, 0, -20, 0);

        var storyboard = new Storyboard();

        var opacityAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        Storyboard.SetTarget(opacityAnimation, page);
        Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));

        var marginAnimation = new ThicknessAnimation
        {
            From = new Thickness(20, 0, -20, 0),
            To = new Thickness(0, 0, 0, 0),
            Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        Storyboard.SetTarget(marginAnimation, page);
        Storyboard.SetTargetProperty(marginAnimation, new PropertyPath("Margin"));

        storyboard.Children.Add(opacityAnimation);
        storyboard.Children.Add(marginAnimation);
        storyboard.Begin();
    }

    /// <summary>
    /// Cleanup when the ViewModel is disposed
    /// </summary>
    protected override void OnDeactivated()
    {
        ThemeManager.Instance.PropertyChanged -= OnThemeManagerPropertyChanged;
        _navigationService.NavigationOccurred -= OnNavigationOccurred;
        base.OnDeactivated();
    }
}
