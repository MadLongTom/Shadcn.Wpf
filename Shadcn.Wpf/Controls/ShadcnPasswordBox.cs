using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// A styled password input control with Shadcn design
/// </summary>
public class ShadcnPasswordBox : Control
{
    private PasswordBox? _passwordBox;
    private TextBox? _visibleTextBox;
    private Border? _border;
    private Button? _toggleButton;

    static ShadcnPasswordBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnPasswordBox), 
            new FrameworkPropertyMetadata(typeof(ShadcnPasswordBox)));
    }

    public ShadcnPasswordBox()
    {
        Focusable = true;
    }

    #region Dependency Properties

    /// <summary>
    /// Gets or sets the password value
    /// </summary>
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register(nameof(Password), typeof(string), typeof(ShadcnPasswordBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged));

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    /// <summary>
    /// Gets or sets the placeholder text
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(ShadcnPasswordBox),
            new PropertyMetadata(string.Empty));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the control shows helper text beneath
    /// </summary>
    public static readonly DependencyProperty HelperTextProperty =
        DependencyProperty.Register(nameof(HelperText), typeof(string), typeof(ShadcnPasswordBox),
            new PropertyMetadata(string.Empty));

    public string HelperText
    {
        get => (string)GetValue(HelperTextProperty);
        set => SetValue(HelperTextProperty, value);
    }

    /// <summary>
    /// Gets or sets an explicit invalid visual state (used for shake animation, etc.)
    /// </summary>
    public static readonly DependencyProperty IsInvalidProperty =
        DependencyProperty.Register(nameof(IsInvalid), typeof(bool), typeof(ShadcnPasswordBox),
            new PropertyMetadata(false, OnIsInvalidChanged));

    public bool IsInvalid
    {
        get => (bool)GetValue(IsInvalidProperty);
        set => SetValue(IsInvalidProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the password box has an error state
    /// </summary>
    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.Register(nameof(HasError), typeof(bool), typeof(ShadcnPasswordBox),
            new PropertyMetadata(false, OnHasErrorChanged));

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show the password toggle button
    /// </summary>
    public static readonly DependencyProperty ShowPasswordToggleProperty =
        DependencyProperty.Register(nameof(ShowPasswordToggle), typeof(bool), typeof(ShadcnPasswordBox),
            new PropertyMetadata(true));

    public bool ShowPasswordToggle
    {
        get => (bool)GetValue(ShowPasswordToggleProperty);
        set => SetValue(ShowPasswordToggleProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the password is visible
    /// </summary>
    public static readonly DependencyProperty IsPasswordVisibleProperty =
        DependencyProperty.Register(nameof(IsPasswordVisible), typeof(bool), typeof(ShadcnPasswordBox),
            new PropertyMetadata(false, OnPasswordVisibilityChanged));

    public bool IsPasswordVisible
    {
        get => (bool)GetValue(IsPasswordVisibleProperty);
        set => SetValue(IsPasswordVisibleProperty, value);
    }

    #endregion

    #region Routed Events

    /// <summary>
    /// Occurs when the password changes
    /// </summary>
    public static readonly RoutedEvent PasswordChangedEvent =
        EventManager.RegisterRoutedEvent(nameof(PasswordChanged), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ShadcnPasswordBox));

    public event RoutedEventHandler PasswordChanged
    {
        add => AddHandler(PasswordChangedEvent, value);
        remove => RemoveHandler(PasswordChangedEvent, value);
    }

    #endregion

    #region Overrides

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_passwordBox != null)
        {
            _passwordBox.PasswordChanged -= OnInternalPasswordChanged;
        }

        if (_visibleTextBox != null)
        {
            _visibleTextBox.TextChanged -= OnVisibleTextChanged;
        }

        if (_toggleButton != null)
        {
            _toggleButton.Click -= OnToggleButtonClick;
        }

        _passwordBox = GetTemplateChild("PART_PasswordBox") as PasswordBox;
        _visibleTextBox = GetTemplateChild("PART_VisibleTextBox") as TextBox;
        _border = GetTemplateChild("PART_Border") as Border;
        _toggleButton = GetTemplateChild("ToggleButton") as Button;

        if (_passwordBox != null)
        {
            _passwordBox.PasswordChanged += OnInternalPasswordChanged;
            _passwordBox.Password = Password;
        }

        if (_visibleTextBox != null)
        {
            _visibleTextBox.TextChanged += OnVisibleTextChanged;
            _visibleTextBox.Text = Password;
        }

        if (_toggleButton != null)
        {
            _toggleButton.Click += OnToggleButtonClick;
        }

        UpdateVisualState();
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);
        _passwordBox?.Focus();
        UpdateVisualState();
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        UpdateVisualState();
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);
        Focus();
    }

    #endregion

    #region Event Handlers

    private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnPasswordBox passwordBox && passwordBox._passwordBox != null)
        {
            passwordBox._passwordBox.Password = e.NewValue?.ToString() ?? string.Empty;
        }
    }

    private static void OnHasErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnPasswordBox passwordBox)
        {
            passwordBox.UpdateVisualState();
        }
    }

    private static void OnPasswordVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnPasswordBox passwordBox)
        {
            passwordBox.UpdateVisualState();
        }
    }

    private static void OnIsInvalidChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnPasswordBox pb)
        {
            pb.UpdateVisualState();
        }
    }

    private void OnInternalPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (_passwordBox != null)
        {
            Password = _passwordBox.Password;
            if (_visibleTextBox != null)
            {
                _visibleTextBox.TextChanged -= OnVisibleTextChanged;
                _visibleTextBox.Text = Password;
                _visibleTextBox.TextChanged += OnVisibleTextChanged;
            }
            RaiseEvent(new RoutedEventArgs(PasswordChangedEvent));
        }
    }

    private void OnVisibleTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_visibleTextBox != null)
        {
            Password = _visibleTextBox.Text;
            if (_passwordBox != null)
            {
                _passwordBox.PasswordChanged -= OnInternalPasswordChanged;
                _passwordBox.Password = Password;
                _passwordBox.PasswordChanged += OnInternalPasswordChanged;
            }
            RaiseEvent(new RoutedEventArgs(PasswordChangedEvent));
        }
    }

    private void OnToggleButtonClick(object sender, RoutedEventArgs e)
    {
        TogglePasswordVisibility();
    }

    #endregion

    #region Methods

    private void UpdateVisualState()
    {
        // Intentionally left minimal so that XAML style triggers control visual appearance
        // This method can be expanded if code-behind driven visual logic is required.
    }

    /// <summary>
    /// Toggles password visibility
    /// </summary>
    public void TogglePasswordVisibility()
    {
        IsPasswordVisible = !IsPasswordVisible;
    }

    #endregion
}
