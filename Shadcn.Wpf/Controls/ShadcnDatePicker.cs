using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Shadcn.Wpf.Models;
using System.Windows.Media;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// DatePicker variant types
/// </summary>
public enum DatePickerVariant
{
    Default,
    Outline,
    Ghost
}

/// <summary>
/// DatePicker size types
/// </summary>
public enum DatePickerSize
{
    Small,
    Medium,
    Large
}

/// <summary>
/// A styled date picker control with Shadcn design
/// </summary>
public class ShadcnDatePicker : DatePicker
{
    #region Private Fields
    
    private Button? _clearButton;
    private Button? _calendarButton;
    private Popup? _popup;
    private ShadcnCalendar? _customCalendar;

    #endregion

    static ShadcnDatePicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnDatePicker), 
            new FrameworkPropertyMetadata(typeof(ShadcnDatePicker)));
    }

    public ShadcnDatePicker()
    {
        Loaded += OnLoaded;
        // Handle mouse down events to detect clicks outside popup
        PreviewMouseDown += OnPreviewMouseDown;
        
        // Disable the native dropdown completely
        SetValue(IsDropDownOpenProperty, false);
        
        // Monitor for IsDropDownOpen changes and prevent them
        DependencyPropertyDescriptor.FromProperty(IsDropDownOpenProperty, typeof(DatePicker))
            ?.AddValueChanged(this, OnIsDropDownOpenChanged);
            
        // Override mouse button down to prevent native calendar
        PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
    }

    private void OnPreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        // If this is a click on our calendar button, handle it ourselves
        if (_calendarButton != null && e.Source == _calendarButton)
        {
            e.Handled = true;
            if (_popup != null)
            {
                _popup.IsOpen = !_popup.IsOpen;
            }
            // Force native dropdown to stay closed
            SetValue(IsDropDownOpenProperty, false);
        }
    }

    private void OnIsDropDownOpenChanged(object? sender, EventArgs e)
    {
        // Always force native dropdown to stay closed
        if (IsDropDownOpen)
        {
            SetValue(IsDropDownOpenProperty, false);
        }
    }

    #region Event Handlers

    private void OnPreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        // If popup is open and click is outside the popup, close it
        if (_popup != null && _popup.IsOpen)
        {
            var popupChild = _popup.Child;
            if (popupChild != null)
            {
                var point = e.GetPosition(popupChild);
                var bounds = new Rect(0, 0, popupChild.RenderSize.Width, popupChild.RenderSize.Height);
                
                // Also check if click is on the calendar button
                var buttonPoint = e.GetPosition(_calendarButton);
                var buttonBounds = new Rect(0, 0, _calendarButton?.RenderSize.Width ?? 0, _calendarButton?.RenderSize.Height ?? 0);
                
                if (!bounds.Contains(point) && !buttonBounds.Contains(buttonPoint))
                {
                    _popup.IsOpen = false;
                }
            }
        }
    }

    #endregion

    #region Dependency Properties

    /// <summary>
    /// Gets or sets the placeholder text when no date is selected
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(ShadcnDatePicker),
            new PropertyMetadata("Select a date"));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the date picker has an error state
    /// </summary>
    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.Register(nameof(HasError), typeof(bool), typeof(ShadcnDatePicker),
            new PropertyMetadata(false, OnHasErrorChanged));

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets the error message
    /// </summary>
    public static readonly DependencyProperty ErrorMessageProperty =
        DependencyProperty.Register(nameof(ErrorMessage), typeof(string), typeof(ShadcnDatePicker),
            new PropertyMetadata(string.Empty));

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    /// <summary>
    /// Gets or sets the helper text
    /// </summary>
    public static readonly DependencyProperty HelperTextProperty =
        DependencyProperty.Register(nameof(HelperText), typeof(string), typeof(ShadcnDatePicker),
            new PropertyMetadata(string.Empty));

    public string HelperText
    {
        get => (string)GetValue(HelperTextProperty);
        set => SetValue(HelperTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the label text
    /// </summary>
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(ShadcnDatePicker),
            new PropertyMetadata(string.Empty));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the visual variant of the date picker
    /// </summary>
    public static readonly DependencyProperty VariantProperty =
        DependencyProperty.Register(nameof(Variant), typeof(DatePickerVariant), typeof(ShadcnDatePicker),
            new PropertyMetadata(DatePickerVariant.Default));

    public DatePickerVariant Variant
    {
        get => (DatePickerVariant)GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the date picker
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(DatePickerSize), typeof(ShadcnDatePicker),
            new PropertyMetadata(DatePickerSize.Medium));

    public DatePickerSize Size
    {
        get => (DatePickerSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius of the date picker
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ShadcnDatePicker),
            new PropertyMetadata(new CornerRadius(6)));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show the calendar icon
    /// </summary>
    public static readonly DependencyProperty ShowIconProperty =
        DependencyProperty.Register(nameof(ShowIcon), typeof(bool), typeof(ShadcnDatePicker),
            new PropertyMetadata(true));

    public bool ShowIcon
    {
        get => (bool)GetValue(ShowIconProperty);
        set => SetValue(ShowIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon geometry
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(Geometry), typeof(ShadcnDatePicker),
            new PropertyMetadata(null));

    public Geometry Icon
    {
        get => (Geometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the date picker is clearable
    /// </summary>
    public static readonly DependencyProperty IsClearableProperty =
        DependencyProperty.Register(nameof(IsClearable), typeof(bool), typeof(ShadcnDatePicker),
            new PropertyMetadata(true));

    public bool IsClearable
    {
        get => (bool)GetValue(IsClearableProperty);
        set => SetValue(IsClearableProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show the week numbers
    /// </summary>
    public static readonly DependencyProperty ShowWeekNumbersProperty =
        DependencyProperty.Register(nameof(ShowWeekNumbers), typeof(bool), typeof(ShadcnDatePicker),
            new PropertyMetadata(false));

    public bool ShowWeekNumbers
    {
        get => (bool)GetValue(ShowWeekNumbersProperty);
        set => SetValue(ShowWeekNumbersProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the text input is enabled
    /// </summary>
    public static readonly DependencyProperty IsTextInputEnabledProperty =
        DependencyProperty.Register(nameof(IsTextInputEnabled), typeof(bool), typeof(ShadcnDatePicker),
            new PropertyMetadata(true, OnIsTextInputEnabledChanged));

    public bool IsTextInputEnabled
    {
        get => (bool)GetValue(IsTextInputEnabledProperty);
        set => SetValue(IsTextInputEnabledProperty, value);
    }

    #endregion

    #region Event Handlers

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ApplyTemplate();
        UpdateVisualState();
    }

    private static void OnHasErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnDatePicker datePicker)
        {
            datePicker.UpdateVisualState();
        }
    }

    private static void OnIsTextInputEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnDatePicker datePicker)
        {
            datePicker.UpdateTextInputState();
        }
    }

    #endregion

    #region Private Methods

    private void UpdateVisualState()
    {
        // Update visual state based on current properties
        // This will be handled by the template triggers
    }

    private void UpdateTextInputState()
    {
        // Update the IsReadOnly property of the DatePickerTextBox
        if (GetTemplateChild("PART_TextBox") is DatePickerTextBox textBox)
        {
            textBox.IsReadOnly = !IsTextInputEnabled;
        }
    }
    
    private void UpdateTextDisplay()
    {
        // Force update the text display
        if (GetTemplateChild("PART_TextBox") is DatePickerTextBox textBox)
        {
            if (SelectedDate.HasValue)
            {
                textBox.Text = SelectedDate.Value.ToString("d");
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }
        // Also try to find any TextBox in the template if DatePickerTextBox is not found
        else if (GetTemplateChild("PART_TextBox") is TextBox regularTextBox)
        {
            if (SelectedDate.HasValue)
            {
                regularTextBox.Text = SelectedDate.Value.ToString("d");
            }
            else
            {
                regularTextBox.Text = string.Empty;
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Clears the selected date
    /// </summary>
    public void Clear()
    {
        if (IsClearable)
        {
            SelectedDate = null;
        }
    }

    /// <summary>
    /// Sets the date picker to today's date
    /// </summary>
    public void SetToday()
    {
        SelectedDate = DateTime.Today;
    }

    #endregion

    #region Overrides

    public override void OnApplyTemplate()
    {
        // Remove existing event handlers
        if (_clearButton != null)
            _clearButton.Click -= ClearButton_Click;
        if (_calendarButton != null)
            _calendarButton.Click -= CalendarButton_Click;
        if (_popup != null)
            _popup.Opened -= Popup_Opened;
        if (_customCalendar != null)
        {
            _customCalendar.DateSelected -= Calendar_DateSelected;
            _customCalendar.CloseRequested -= Calendar_CloseRequested;
        }

        // DO NOT call base.OnApplyTemplate() to prevent native calendar behavior
        // Instead, manually apply our template
        
        // Get template parts
        _clearButton = GetTemplateChild("ClearButton") as Button;
        _calendarButton = GetTemplateChild("PART_Button") as Button;
        _popup = GetTemplateChild("PART_Popup") as Popup;
        _customCalendar = GetTemplateChild("CustomCalendar") as ShadcnCalendar;
        
        // Hook up the clear button event
        if (_clearButton != null)
        {
            _clearButton.Click += ClearButton_Click;
        }
        
        // Hook up the calendar button event - completely override default behavior
        if (_calendarButton != null)
        {
            _calendarButton.Click += CalendarButton_Click;
        }
        
        // Hook up the popup and calendar events
        if (_popup != null)
        {
            _popup.Opened += Popup_Opened;
        }
        
        if (_customCalendar != null)
        {
            _customCalendar.DateSelected += Calendar_DateSelected;
            _customCalendar.CloseRequested += Calendar_CloseRequested;
            
            // Sync the initial selected date
            _customCalendar.SelectedDate = SelectedDate;
        }
        
        // Update text input state
        UpdateTextInputState();
        
        // Update text display if we have a selected date
        UpdateTextDisplay();
        
        // Ensure native dropdown stays closed
        SetValue(IsDropDownOpenProperty, false);
    }

    protected override void OnSelectedDateChanged(SelectionChangedEventArgs e)
    {
        // Call base to maintain proper selection behavior
        base.OnSelectedDateChanged(e);
        
        // Clear any error state when a valid date is selected
        if (SelectedDate.HasValue && HasError)
        {
            HasError = false;
        }
        
        // Update the calendar's selected date if it exists
        if (_customCalendar != null)
        {
            _customCalendar.SelectedDate = SelectedDate;
        }
        
        // Update text display
        Dispatcher.BeginInvoke(new Action(() =>
        {
            UpdateTextDisplay();
        }), System.Windows.Threading.DispatcherPriority.Loaded);
    }

    #endregion

    #region Event Handlers

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        Clear();
    }
    
    private void CalendarButton_Click(object sender, RoutedEventArgs e)
    {
        // Prevent any native behavior
        e.Handled = true;
        
        if (_popup != null)
        {
            _popup.IsOpen = !_popup.IsOpen;
        }
        
        // Force native dropdown to stay closed
        if (IsDropDownOpen)
        {
            SetValue(IsDropDownOpenProperty, false);
        }
    }
    
    private void Popup_Opened(object? sender, EventArgs e)
    {
        if (_customCalendar != null)
        {
            // Sync the calendar with current date picker state
            _customCalendar.SelectedDate = SelectedDate;
            _customCalendar.ShowWeekNumbers = ShowWeekNumbers;
            
            if (SelectedDate.HasValue)
            {
                _customCalendar.DisplayDate = SelectedDate.Value;
            }
            else
            {
                _customCalendar.DisplayDate = DateTime.Today;
            }
        }
    }
    
    private void Calendar_DateSelected(object? sender, DateSelectedEventArgs e)
    {
        // Update the DatePicker's selected date using the property (not SetValue directly)
        SelectedDate = e.SelectedDate;
        
        // Close the popup after date selection
        if (_popup != null)
        {
            _popup.IsOpen = false;
        }
    }
    
    private void Calendar_CloseRequested(object? sender, EventArgs e)
    {
        if (_popup != null)
        {
            _popup.IsOpen = false;
        }
    }

    #endregion
}
