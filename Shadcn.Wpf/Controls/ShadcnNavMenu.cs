using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Models;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// A collapsible navigation menu control that supports hierarchical navigation items
/// </summary>
[TemplatePart(Name = "PART_ToggleButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_Content", Type = typeof(ItemsControl))]
public class ShadcnNavMenu : Control
{
    private Button? _toggleButton;
    private ItemsControl? _itemsControl;

    static ShadcnNavMenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnNavMenu), 
            new FrameworkPropertyMetadata(typeof(ShadcnNavMenu)));
    }

    public ShadcnNavMenu()
    {
        NavigationItems = new ObservableCollection<NavigationItem>();
    }

    #region Dependency Properties

    /// <summary>
    /// Gets or sets whether the navigation menu is collapsed
    /// </summary>
    public static readonly DependencyProperty IsCollapsedProperty =
        DependencyProperty.Register(nameof(IsCollapsed), typeof(bool), typeof(ShadcnNavMenu),
            new PropertyMetadata(false, OnIsCollapsedChanged));

    public bool IsCollapsed
    {
        get => (bool)GetValue(IsCollapsedProperty);
        set => SetValue(IsCollapsedProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the menu when expanded
    /// </summary>
    public static readonly DependencyProperty ExpandedWidthProperty =
        DependencyProperty.Register(nameof(ExpandedWidth), typeof(double), typeof(ShadcnNavMenu),
            new PropertyMetadata(250.0));

    public double ExpandedWidth
    {
        get => (double)GetValue(ExpandedWidthProperty);
        set => SetValue(ExpandedWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the menu when collapsed
    /// </summary>
    public static readonly DependencyProperty CollapsedWidthProperty =
        DependencyProperty.Register(nameof(CollapsedWidth), typeof(double), typeof(ShadcnNavMenu),
            new PropertyMetadata(64.0));

    public double CollapsedWidth
    {
        get => (double)GetValue(CollapsedWidthProperty);
        set => SetValue(CollapsedWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the collection of navigation items
    /// </summary>
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(ShadcnNavMenu),
            new PropertyMetadata(null, OnItemsSourceChanged));

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the header content
    /// </summary>
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(ShadcnNavMenu),
            new PropertyMetadata(null));

    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

/// <summary>
    /// Gets or sets whether to show the toggle button
    /// </summary>
    public static readonly DependencyProperty ShowToggleButtonProperty =
        DependencyProperty.Register(nameof(ShowToggleButton), typeof(bool), typeof(ShadcnNavMenu),
            new PropertyMetadata(true));

    public bool ShowToggleButton
    {
        get => (bool)GetValue(ShowToggleButtonProperty);
        set => SetValue(ShowToggleButtonProperty, value);
    }

    /// <summary>
    /// Gets or sets the navigation items
    /// </summary>
    public static readonly DependencyProperty NavigationItemsProperty =
        DependencyProperty.Register(nameof(NavigationItems), typeof(ObservableCollection<NavigationItem>), typeof(ShadcnNavMenu),
            new PropertyMetadata(null, OnNavigationItemsChanged));

    public ObservableCollection<NavigationItem>? NavigationItems
    {
        get => (ObservableCollection<NavigationItem>?)GetValue(NavigationItemsProperty);
        set => SetValue(NavigationItemsProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to execute when a navigation item is selected
    /// </summary>
    public static readonly DependencyProperty NavigationItemSelectedCommandProperty =
        DependencyProperty.Register(nameof(NavigationItemSelectedCommand), typeof(ICommand), typeof(ShadcnNavMenu),
            new PropertyMetadata(null));

    public ICommand? NavigationItemSelectedCommand
    {
        get => (ICommand?)GetValue(NavigationItemSelectedCommandProperty);
        set => SetValue(NavigationItemSelectedCommandProperty, value);
    }

    #endregion

    #region Events

    /// <summary>
    /// Occurs when a navigation item is selected
    /// </summary>
    public static readonly RoutedEvent NavigationItemSelectedEvent =
        EventManager.RegisterRoutedEvent(nameof(NavigationItemSelected), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ShadcnNavMenu));

    public event RoutedEventHandler NavigationItemSelected
    {
        add => AddHandler(NavigationItemSelectedEvent, value);
        remove => RemoveHandler(NavigationItemSelectedEvent, value);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the currently selected navigation item
    /// </summary>
    public NavigationItem? SelectedItem { get; set; }

    #endregion

    #region Overrides

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_toggleButton != null)
        {
            _toggleButton.Click -= OnToggleButtonClick;
        }

        _toggleButton = GetTemplateChild("PART_ToggleButton") as Button;
        _itemsControl = GetTemplateChild("PART_Content") as ItemsControl;

        if (_toggleButton != null)
        {
            _toggleButton.Click += OnToggleButtonClick;
        }

        // Set ItemsSource after getting the ItemsControl
        if (_itemsControl != null && NavigationItems != null)
        {
            _itemsControl.ItemsSource = NavigationItems;
        }

        UpdateWidth();
    }

    #endregion

    #region Event Handlers

    private static void OnIsCollapsedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnNavMenu navMenu)
        {
            navMenu.UpdateWidth();
        }
    }

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnNavMenu navMenu)
        {
            navMenu.UpdateNavigationItems();
        }
    }

    private static void OnNavigationItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnNavMenu navMenu)
        {
            // Setup commands for all navigation items
            if (navMenu.NavigationItems != null)
            {
                foreach (var item in navMenu.NavigationItems)
                {
                    navMenu.SetupNavigationItem(item);
                }
            }
            
            // Update the items control if template is applied
            if (navMenu._itemsControl != null)
            {
                navMenu._itemsControl.ItemsSource = navMenu.NavigationItems;
            }
        }
    }

    private void OnToggleButtonClick(object sender, RoutedEventArgs e)
    {
        IsCollapsed = !IsCollapsed;
    }

    #endregion

    #region Methods

    private void UpdateWidth()
    {
        var targetWidth = IsCollapsed ? CollapsedWidth : ExpandedWidth;
        
        // Create and start width animation
        var widthAnimation = new DoubleAnimation
        {
            To = targetWidth,
            Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        
        // Start the animation
        BeginAnimation(WidthProperty, widthAnimation);
        
        // Animate toggle button rotation if it exists
        AnimateToggleButton();
    }

    private void AnimateToggleButton()
    {
        if (_toggleButton != null)
        {
            // Find the TextBlock with the ToggleIcon name in the template
            var toggleIcon = _toggleButton.Template?.FindName("ToggleIcon", _toggleButton) as FrameworkElement;
            
            if (toggleIcon?.RenderTransform is RotateTransform rotateTransform)
            {
                // When collapsed, rotate 180 degrees to point left
                // When expanded, keep at 0 degrees to point right
                var targetAngle = IsCollapsed ? 180 : 0;
                
                var rotateAnimation = new DoubleAnimation
                {
                    To = targetAngle,
                    Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            }
        }
    }

    private void UpdateNavigationItems()
    {
        System.Diagnostics.Debug.WriteLine("UpdateNavigationItems called");
        
        if (NavigationItems == null)
            NavigationItems = new ObservableCollection<NavigationItem>();
            
        NavigationItems.Clear();
        
        if (ItemsSource != null)
        {
            System.Diagnostics.Debug.WriteLine($"ItemsSource has {ItemsSource.Cast<object>().Count()} items");
            foreach (var item in ItemsSource)
            {
                if (item is NavigationItem navItem)
                {
                    System.Diagnostics.Debug.WriteLine($"Processing NavigationItem: {navItem.Title}");
                    SetupNavigationItem(navItem);
                    NavigationItems.Add(navItem);
                }
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("ItemsSource is null");
        }
        
        // Update the ItemsControl if it exists
        if (_itemsControl != null)
        {
            System.Diagnostics.Debug.WriteLine("Updating ItemsControl.ItemsSource");
            _itemsControl.ItemsSource = NavigationItems;
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("_itemsControl is null");
        }
    }

    private void SetupNavigationItem(NavigationItem item)
    {
        item.ClickCommand = new RelayCommand(() => SelectNavigationItem(item));
        System.Diagnostics.Debug.WriteLine($"Setup NavigationItem: {item.Title} with ClickCommand");
        
        foreach (var child in item.Children)
        {
            SetupNavigationItem(child);
        }
    }

    private void SelectNavigationItem(NavigationItem item)
    {
        System.Diagnostics.Debug.WriteLine($"SelectNavigationItem called for: {item.Title}");
        
        // Always execute the command first
        NavigationItemSelectedCommand?.Execute(item);
        System.Diagnostics.Debug.WriteLine($"NavigationItemSelectedCommand executed for: {item.Title}");
        
        // Toggle expansion for items with children, otherwise select the item
        if (item.HasChildren)
        {
            item.IsExpanded = !item.IsExpanded;
        }
        else
        {
            // Clear previous selection
            if (NavigationItems != null)
            {
                ClearSelection(NavigationItems);
            }
            
            // Set new selection
            item.IsSelected = true;
            SelectedItem = item;
            
            // Raise event
            var args = new RoutedEventArgs(NavigationItemSelectedEvent, item);
            RaiseEvent(args);
        }
    }

    private void ClearSelection(IEnumerable<NavigationItem> items)
    {
        foreach (var item in items)
        {
            item.IsSelected = false;
            ClearSelection(item.Children);
        }
    }

    #endregion
}
