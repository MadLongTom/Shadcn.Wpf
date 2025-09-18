using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Shadcn.Wpf.Models;

/// <summary>
/// Event arguments for navigation item selection
/// </summary>
public class NavigationItemSelectedEventArgs : RoutedEventArgs
{
    public NavigationItem SelectedItem { get; }

    public NavigationItemSelectedEventArgs(NavigationItem selectedItem, RoutedEvent routedEvent) : base(routedEvent)
    {
        SelectedItem = selectedItem;
    }
}

/// <summary>
/// Represents a navigation item in the NavMenu
/// </summary>
public partial class NavigationItem : ObservableObject
{
    [ObservableProperty]
    private string _title = "";

    [ObservableProperty]
    private Geometry? _icon;

    [ObservableProperty]
    private bool _isExpanded = false;

    [ObservableProperty]
    private bool _isSelected = false;

    [ObservableProperty]
    private bool _isEnabled = true;

    [ObservableProperty]
    private string _badge = "";

    [ObservableProperty]
    private object? _tag;

    public ObservableCollection<NavigationItem> Children { get; set; } = new();

    public bool HasChildren => Children.Count > 0;

    // Make ClickCommand raise change notifications so XAML binding updates even if set after item materialization
    private ICommand? _clickCommand;
    public ICommand? ClickCommand
    {
        get => _clickCommand;
        set => SetProperty(ref _clickCommand, value);
    }

    /// <summary>
    /// Command to toggle expansion of the navigation item
    /// </summary>
    [RelayCommand]
    private void ToggleExpansion()
    {
        IsExpanded = !IsExpanded;
    }

    /// <summary>
    /// Command to select this navigation item
    /// </summary>
    [RelayCommand]
    private void Select()
    {
        IsSelected = true;
    }

    /// <summary>
    /// Method to deselect this item and all children
    /// </summary>
    public void DeselectAll()
    {
        IsSelected = false;
        foreach (var child in Children)
        {
            child.DeselectAll();
        }
    }

    /// <summary>
    /// Method to find and select an item by tag
    /// </summary>
    public bool SelectByTag(object? tag)
    {
        if (Equals(Tag, tag))
        {
            IsSelected = true;
            return true;
        }

        foreach (var child in Children)
        {
            if (child.SelectByTag(tag))
            {
                IsExpanded = true; // Expand parent if child is selected
                return true;
            }
        }

        return false;
    }
}
