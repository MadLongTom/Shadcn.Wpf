using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// Shadcn-styled Select/DropDown control with data binding support
/// </summary>
[TemplatePart(Name = "PART_ToggleButton", Type = typeof(ToggleButton))]
[TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
[TemplatePart(Name = "PART_ItemsPresenter", Type = typeof(ItemsPresenter))]
public class ShadcnSelect : ItemsControl
{
    static ShadcnSelect()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnSelect), new FrameworkPropertyMetadata(typeof(ShadcnSelect)));
    }

    public ShadcnSelect()
    {
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Ensure selection is synchronized
        if (SelectedItem == null && !string.IsNullOrEmpty(SelectedValuePath) && SelectedValue != null)
        {
            SetSelectedItemFromValue();
        }
    }

    #region Dependency Properties

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(ShadcnSelect),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));

    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register(nameof(SelectedValue), typeof(object), typeof(ShadcnSelect),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedValueChanged));

    public static readonly DependencyProperty SelectedValuePathProperty =
        DependencyProperty.Register(nameof(SelectedValuePath), typeof(string), typeof(ShadcnSelect),
            new FrameworkPropertyMetadata(string.Empty, OnSelectedValuePathChanged));

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(ShadcnSelect),
            new FrameworkPropertyMetadata("Select an option..."));

    public static readonly DependencyProperty IsDropDownOpenProperty =
        DependencyProperty.Register(nameof(IsDropDownOpen), typeof(bool), typeof(ShadcnSelect),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDropDownOpenChanged));

    public static readonly DependencyProperty MaxDropDownHeightProperty =
        DependencyProperty.Register(nameof(MaxDropDownHeight), typeof(double), typeof(ShadcnSelect),
            new FrameworkPropertyMetadata(200.0));

    public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(ShadcnSelect),
            new FrameworkPropertyMetadata(false));

    #endregion

    #region Properties

    [Bindable(true)]
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    [Bindable(true)]
    public object SelectedValue
    {
        get => GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    public string SelectedValuePath
    {
        get => (string)GetValue(SelectedValuePathProperty);
        set => SetValue(SelectedValuePathProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    [Bindable(true)]
    public bool IsDropDownOpen
    {
        get => (bool)GetValue(IsDropDownOpenProperty);
        set => SetValue(IsDropDownOpenProperty, value);
    }

    public double MaxDropDownHeight
    {
        get => (double)GetValue(MaxDropDownHeightProperty);
        set => SetValue(MaxDropDownHeightProperty, value);
    }

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    #endregion

    #region Events

    public static readonly RoutedEvent SelectionChangedEvent =
        EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble,
            typeof(SelectionChangedEventHandler), typeof(ShadcnSelect));

    public event SelectionChangedEventHandler SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    #endregion

    #region Template Parts

    private ToggleButton? _toggleButton;
    private ContentPresenter? _contentPresenter;
    private Popup? _popup;
    private ItemsPresenter? _itemsPresenter;

    #endregion

    #region Overrides

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // Cleanup old event handlers
        if (_toggleButton != null)
        {
            _toggleButton.Click -= OnToggleButtonClick;
        }

        // Get template parts
        _toggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;
        _popup = GetTemplateChild("PART_Popup") as Popup;
        _itemsPresenter = GetTemplateChild("PART_ItemsPresenter") as ItemsPresenter;
        
        // Get ContentPresenter from ToggleButton template
        if (_toggleButton != null)
        {
            _toggleButton.ApplyTemplate();
            _contentPresenter = _toggleButton.Template?.FindName("PART_ContentPresenter", _toggleButton) as ContentPresenter;
        }

        // Setup new event handlers
        if (_toggleButton != null)
        {
            _toggleButton.Click += OnToggleButtonClick;
        }

        if (_popup != null)
        {
            _popup.Opened += OnPopupOpened;
            _popup.Closed += OnPopupClosed;
        }

        // Delay UpdateContentPresenter to ensure ToggleButton template is fully loaded
        Dispatcher.BeginInvoke(new Action(UpdateContentPresenter), System.Windows.Threading.DispatcherPriority.Loaded);
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new ShadcnSelectItem();
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is ShadcnSelectItem;
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
        base.PrepareContainerForItemOverride(element, item);

        if (element is ShadcnSelectItem selectItem)
        {
            selectItem.Select += OnItemSelected;
            
            // Set up display binding
            if (!string.IsNullOrEmpty(DisplayMemberPath) && item != null)
            {
                var binding = new Binding(DisplayMemberPath) { Source = item };
                selectItem.SetBinding(ContentControl.ContentProperty, binding);
            }
            else
            {
                selectItem.Content = item;
            }

            // Update selection state
            selectItem.IsSelected = object.Equals(item, SelectedItem);
        }
    }

    protected override void ClearContainerForItemOverride(DependencyObject element, object item)
    {
        if (element is ShadcnSelectItem selectItem)
        {
            selectItem.Select -= OnItemSelected;
        }
        base.ClearContainerForItemOverride(element, item);
    }

    #endregion

    #region Event Handlers

    private void OnToggleButtonClick(object sender, RoutedEventArgs e)
    {
        if (!IsReadOnly)
        {
            IsDropDownOpen = !IsDropDownOpen;
            e.Handled = true; // Prevent the event from bubbling
        }
    }

    private void OnPopupOpened(object? sender, EventArgs e)
    {
        // Popup opened - ensure ToggleButton state is synchronized
        if (_toggleButton != null)
        {
            _toggleButton.IsChecked = true;
        }
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        // Popup closed - ensure ToggleButton state is synchronized
        if (_toggleButton != null)
        {
            _toggleButton.IsChecked = false;
        }
        
        // Only set IsDropDownOpen to false if it's currently true to avoid unnecessary notifications
        if (IsDropDownOpen)
        {
            IsDropDownOpen = false;
        }
    }

    private void OnItemSelected(object sender, RoutedEventArgs e)
    {
        if (sender is ShadcnSelectItem selectItem)
        {
            var oldItem = SelectedItem;
            SelectedItem = selectItem.DataContext ?? selectItem.Content;
            IsDropDownOpen = false;

            // Raise SelectionChanged event
            var args = new SelectionChangedEventArgs(SelectionChangedEvent, 
                oldItem != null ? new[] { oldItem } : new object[0],
                SelectedItem != null ? new[] { SelectedItem } : new object[0]);
            RaiseEvent(args);
        }
    }

    #endregion

    #region Property Change Handlers

    private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnSelect select)
        {
            select.OnSelectedItemChanged(e.OldValue, e.NewValue);
        }
    }

    private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnSelect select)
        {
            select.OnSelectedValueChanged(e.NewValue);
        }
    }

    private static void OnSelectedValuePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnSelect select)
        {
            select.OnSelectedValuePathChanged();
        }
    }

    private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnSelect select)
        {
            select.OnIsDropDownOpenChanged((bool)e.NewValue);
        }
    }

    private void OnSelectedItemChanged(object? oldValue, object? newValue)
    {
        UpdateContentPresenter();
        UpdateItemsSelection();
        // Immediately update SelectedValue so external bindings (SelectedPersonId) are in sync before viewmodel setter logic uses it
        UpdateSelectedValue();
    }

    private void OnSelectedValueChanged(object? newValue)
    {
        if (!object.Equals(GetSelectedValue(), newValue))
        {
            SetSelectedItemFromValue();
        }
    }

    private void OnSelectedValuePathChanged()
    {
        UpdateSelectedValue();
    }

    private void OnIsDropDownOpenChanged(bool isOpen)
    {
        if (_popup != null)
        {
            _popup.IsOpen = isOpen;
        }
    }

    #endregion

    #region Helper Methods

    private void UpdateSelectedValue()
    {
        var newValue = GetSelectedValue();
        if (!object.Equals(SelectedValue, newValue))
        {
            SelectedValue = newValue!;
        }
    }

    private object? GetSelectedValue()
    {
        if (SelectedItem == null)
            return null;

        if (string.IsNullOrEmpty(SelectedValuePath))
            return SelectedItem;

        try
        {
            var property = SelectedItem.GetType().GetProperty(SelectedValuePath);
            return property?.GetValue(SelectedItem);
        }
        catch
        {
            return SelectedItem;
        }
    }

    private void SetSelectedItemFromValue()
    {
        if (SelectedValue == null)
        {
            SelectedItem = null!;
            return;
        }

        if (string.IsNullOrEmpty(SelectedValuePath))
        {
            SelectedItem = SelectedValue;
            return;
        }

        foreach (var item in Items)
        {
            if (item != null)
            {
                try
                {
                    var property = item.GetType().GetProperty(SelectedValuePath);
                    var value = property?.GetValue(item);
                    if (object.Equals(value, SelectedValue))
                    {
                        SelectedItem = item;
                        return;
                    }
                }
                catch
                {
                    // Continue to next item
                }
            }
        }
    }

    private void UpdateContentPresenter()
    {
        if (_contentPresenter == null) return;

        if (SelectedItem != null)
        {
            if (!string.IsNullOrEmpty(DisplayMemberPath))
            {
                var binding = new Binding(DisplayMemberPath) { Source = SelectedItem };
                _contentPresenter.SetBinding(ContentPresenter.ContentProperty, binding);
            }
            else
            {
                _contentPresenter.Content = SelectedItem;
            }
        }
        else
        {
            _contentPresenter.Content = null;
        }
    }

    private void UpdateItemsSelection()
    {
        foreach (var container in GetContainers())
        {
            if (container is ShadcnSelectItem selectItem)
            {
                var item = selectItem.DataContext ?? selectItem.Content;
                selectItem.IsSelected = object.Equals(item, SelectedItem);
            }
        }
    }

    private IEnumerable<DependencyObject> GetContainers()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            var container = ItemContainerGenerator.ContainerFromIndex(i);
            if (container != null)
                yield return container;
        }
    }

    #endregion
}

/// <summary>
/// Container for ShadcnSelect items
/// </summary>
public class ShadcnSelectItem : ContentControl
{
    static ShadcnSelectItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnSelectItem), new FrameworkPropertyMetadata(typeof(ShadcnSelectItem)));
    }

    public ShadcnSelectItem()
    {
        MouseLeftButtonUp += OnMouseLeftButtonUp;
    }

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ShadcnSelectItem),
            new FrameworkPropertyMetadata(false));

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public static readonly RoutedEvent SelectEvent =
        EventManager.RegisterRoutedEvent(nameof(Select), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ShadcnSelectItem));

    public event RoutedEventHandler Select
    {
        add => AddHandler(SelectEvent, value);
        remove => RemoveHandler(SelectEvent, value);
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(SelectEvent));
        e.Handled = true;
    }
}
