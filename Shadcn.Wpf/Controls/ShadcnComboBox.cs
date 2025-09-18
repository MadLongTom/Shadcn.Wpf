using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// A styled combo box control with Shadcn design
/// </summary>
public class ShadcnComboBox : ComboBox
{
    static ShadcnComboBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnComboBox), 
            new FrameworkPropertyMetadata(typeof(ShadcnComboBox)));
    }

    public ShadcnComboBox()
    {
        Loaded += OnLoaded;
    }

    #region Dependency Properties

    /// <summary>
    /// Gets or sets the placeholder text
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(ShadcnComboBox),
            new PropertyMetadata("Select an option"));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the combo box has an error state
    /// </summary>
    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.Register(nameof(HasError), typeof(bool), typeof(ShadcnComboBox),
            new PropertyMetadata(false, OnHasErrorChanged));

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show search functionality
    /// </summary>
    public static readonly DependencyProperty IsSearchableProperty =
        DependencyProperty.Register(nameof(IsSearchable), typeof(bool), typeof(ShadcnComboBox),
            new PropertyMetadata(false));

    public bool IsSearchable
    {
        get => (bool)GetValue(IsSearchableProperty);
        set => SetValue(IsSearchableProperty, value);
    }

    /// <summary>
    /// Gets or sets whether multiple items can be selected
    /// </summary>
    public static readonly DependencyProperty IsMultiSelectProperty =
        DependencyProperty.Register(nameof(IsMultiSelect), typeof(bool), typeof(ShadcnComboBox),
            new PropertyMetadata(false));

    public bool IsMultiSelect
    {
        get => (bool)GetValue(IsMultiSelectProperty);
        set => SetValue(IsMultiSelectProperty, value);
    }

    /// <summary>
    /// Gets or sets the collection of selected items (for multi-select)
    /// </summary>
    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register(nameof(SelectedItems), typeof(IList), typeof(ShadcnComboBox),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public IList? SelectedItems
    {
        get => (IList?)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    /// <summary>
    /// Gets whether the placeholder should be shown
    /// </summary>
    public static readonly DependencyProperty ShowPlaceholderProperty =
        DependencyProperty.Register(nameof(ShowPlaceholder), typeof(bool), typeof(ShadcnComboBox),
            new PropertyMetadata(true));

    public bool ShowPlaceholder
    {
        get => (bool)GetValue(ShowPlaceholderProperty);
        set => SetValue(ShowPlaceholderProperty, value);
    }

    #endregion

    #region Overrides

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        base.OnSelectionChanged(e);
        UpdatePlaceholderVisibility();
    }

    protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
        UpdatePlaceholderVisibility();
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);
        UpdateVisualState();
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        UpdateVisualState();
    }

    #endregion

    #region Event Handlers

    private static void OnHasErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnComboBox comboBox)
        {
            comboBox.UpdateVisualState();
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdatePlaceholderVisibility();
        UpdateVisualState();
    }

    #endregion

    #region Methods

    private void UpdatePlaceholderVisibility()
    {
        ShowPlaceholder = SelectedItem == null || 
                         (SelectedItem is string str && string.IsNullOrEmpty(str));
    }

    private void UpdateVisualState()
    {
        // Visual state updates will be handled by the template triggers
    }

    /// <summary>
    /// Adds an item to the selected items collection (for multi-select)
    /// </summary>
    public void AddSelectedItem(object item)
    {
        if (!IsMultiSelect) return;

        SelectedItems ??= new List<object>();
        if (!SelectedItems.Contains(item))
        {
            SelectedItems.Add(item);
        }
    }

    /// <summary>
    /// Removes an item from the selected items collection (for multi-select)
    /// </summary>
    public void RemoveSelectedItem(object item)
    {
        if (!IsMultiSelect || SelectedItems == null) return;

        SelectedItems.Remove(item);
    }

    /// <summary>
    /// Clears all selected items (for multi-select)
    /// </summary>
    public void ClearSelectedItems()
    {
        if (!IsMultiSelect) return;

        SelectedItems?.Clear();
    }

    #endregion
}
