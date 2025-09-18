using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Models;
using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.ViewModels;

/// <summary>
/// ViewModel for the NavigationPage
/// </summary>
public partial class NavigationPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private string _statusMessage = "Ready";

    [ObservableProperty]
    private ObservableCollection<NavigationItem> _demoNavigationItems = new();

    [ObservableProperty]
    private bool _isMenuCollapsed = false;

    public NavigationPageViewModel(IMessageService messageService) 
        : base("Navigation Components", "Navigation menus and breadcrumbs")
    {
        _messageService = messageService;
        InitializeDemoNavigationItems();
    }

    /// <summary>
    /// Command to toggle menu collapse
    /// </summary>
    [RelayCommand]
    private void ToggleCollapse()
    {
        IsMenuCollapsed = !IsMenuCollapsed;
        StatusMessage = $"Menu is now {(IsMenuCollapsed ? "collapsed" : "expanded")}";
        _messageService.ShowInformation($"Menu toggled to {(IsMenuCollapsed ? "collapsed" : "expanded")} state", "Navigation Demo");
    }

    /// <summary>
    /// Command to expand all items
    /// </summary>
    [RelayCommand]
    private void ExpandAll()
    {
        // Expand all navigation items
        if (DemoNavigationItems != null)
        {
            foreach (var item in DemoNavigationItems)
            {
                ExpandNavigationItem(item);
            }
        }
        StatusMessage = "All items expanded";
        _messageService.ShowInformation("All navigation items have been expanded", "Navigation Demo");
    }

    /// <summary>
    /// Command to collapse all items
    /// </summary>
    [RelayCommand]
    private void CollapseAll()
    {
        // Collapse all navigation items
        if (DemoNavigationItems != null)
        {
            foreach (var item in DemoNavigationItems)
            {
                CollapseNavigationItem(item);
            }
        }
        StatusMessage = "All items collapsed";
        _messageService.ShowInformation("All navigation items have been collapsed", "Navigation Demo");
    }

    /// <summary>
    /// Command to add badges
    /// </summary>
    [RelayCommand]
    private void AddBadges()
    {
        // Add badges to navigation items
        if (DemoNavigationItems != null)
        {
            var dashboardItem = DemoNavigationItems.FirstOrDefault(x => x.Tag?.ToString() == "dashboard");
            if (dashboardItem != null) dashboardItem.Badge = "4";
            
            var helpItem = DemoNavigationItems.FirstOrDefault(x => x.Tag?.ToString() == "help");
            if (helpItem != null) helpItem.Badge = "New";
        }
        StatusMessage = "Badges added to items";
        _messageService.ShowInformation("Badges have been added to navigation items", "Navigation Demo");
    }

    /// <summary>
    /// Command to clear badges
    /// </summary>
    [RelayCommand]
    private void ClearBadges()
    {
        // Clear all badges from navigation items
        if (DemoNavigationItems != null)
        {
            foreach (var item in DemoNavigationItems)
            {
                ClearBadgesFromItem(item);
            }
        }
        StatusMessage = "All badges cleared";
        _messageService.ShowInformation("All badges have been cleared from navigation items", "Navigation Demo");
    }

    /// <summary>
    /// Command to handle demo navigation item selection
    /// </summary>
    [RelayCommand]
    private void DemoNavigationItemSelected(NavigationItem selectedItem)
    {
        StatusMessage = $"Selected: {selectedItem?.Title ?? "None"}";
        _messageService.ShowInformation($"Demo navigation item selected: {selectedItem?.Title ?? "None"}", "Navigation Demo");
    }

    /// <summary>
    /// Initialize demo navigation items
    /// </summary>
    private void InitializeDemoNavigationItems()
    {
        DemoNavigationItems.Add(new NavigationItem
        {
            Title = "Dashboard",
            Icon = GetIcon("DashboardIcon"),
            Tag = "dashboard"
        });

        DemoNavigationItems.Add(new NavigationItem
        {
            Title = "Components",
            Icon = GetIcon("DashboardIcon"),
            Tag = "components",
            Badge = "New",
            Children = new ObservableCollection<NavigationItem>
            {
                new NavigationItem { Title = "Buttons", Icon = GetIcon("AddIcon"), Tag = "buttons" },
                new NavigationItem { Title = "Cards", Icon = GetIcon("DocumentIcon"), Tag = "cards" },
                new NavigationItem { Title = "Forms", Icon = GetIcon("EditIcon"), Tag = "forms" }
            }
        });

        DemoNavigationItems.Add(new NavigationItem
        {
            Title = "Settings",
            Icon = GetIcon("SettingsIcon"),
            Tag = "settings",
            Badge = "3",
            Children = new ObservableCollection<NavigationItem>
            {
                new NavigationItem { Title = "General", Icon = GetIcon("SettingsIcon"), Tag = "general" },
                new NavigationItem { Title = "Security", Icon = GetIcon("ErrorIcon"), Tag = "security" },
                new NavigationItem { Title = "Advanced", Icon = GetIcon("WarningIcon"), Tag = "advanced" }
            }
        });

        DemoNavigationItems.Add(new NavigationItem
        {
            Title = "Help",
            Icon = GetIcon("HelpIcon"),
            Tag = "help"
        });
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
    /// Recursively expand a navigation item and all its children
    /// </summary>
    private void ExpandNavigationItem(NavigationItem item)
    {
        if (item.HasChildren)
        {
            item.IsExpanded = true;
            foreach (var child in item.Children)
            {
                ExpandNavigationItem(child);
            }
        }
    }

    /// <summary>
    /// Recursively collapse a navigation item and all its children
    /// </summary>
    private void CollapseNavigationItem(NavigationItem item)
    {
        if (item.HasChildren)
        {
            item.IsExpanded = false;
            foreach (var child in item.Children)
            {
                CollapseNavigationItem(child);
            }
        }
    }

    /// <summary>
    /// Recursively clear badges from a navigation item and all its children
    /// </summary>
    private void ClearBadgesFromItem(NavigationItem item)
    {
        item.Badge = string.Empty;
        foreach (var child in item.Children)
        {
            ClearBadgesFromItem(child);
        }
    }
}
