using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// ShadcnProgressBar control with Shadcn design system styling
/// </summary>
public class ShadcnProgressBar : ProgressBar
{

    static ShadcnProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnProgressBar), 
            new FrameworkPropertyMetadata(typeof(ShadcnProgressBar)));
    }

    #region Size Dependency Property
    
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(ProgressBarSize), typeof(ShadcnProgressBar),
            new PropertyMetadata(ProgressBarSize.Default));

    /// <summary>
    /// Gets or sets the size of the progress bar
    /// </summary>
    public ProgressBarSize Size
    {
        get => (ProgressBarSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
    
    #endregion

    #region Variant Dependency Property
    
    public static readonly DependencyProperty VariantProperty =
        DependencyProperty.Register(nameof(Variant), typeof(ProgressBarVariant), typeof(ShadcnProgressBar),
            new PropertyMetadata(ProgressBarVariant.Default));

    /// <summary>
    /// Gets or sets the variant of the progress bar
    /// </summary>
    public ProgressBarVariant Variant
    {
        get => (ProgressBarVariant)GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }
    
    #endregion

    #region IsIndeterminate Dependency Property
    
    public static new readonly DependencyProperty IsIndeterminateProperty =
        DependencyProperty.Register(nameof(IsIndeterminate), typeof(bool), typeof(ShadcnProgressBar),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the progress bar is in indeterminate mode
    /// </summary>
    public new bool IsIndeterminate
    {
        get => (bool)GetValue(IsIndeterminateProperty);
        set => SetValue(IsIndeterminateProperty, value);
    }
    
    #endregion

    #region ShowPercentage Dependency Property
    
    public static readonly DependencyProperty ShowPercentageProperty =
        DependencyProperty.Register(nameof(ShowPercentage), typeof(bool), typeof(ShadcnProgressBar),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether to show percentage text
    /// </summary>
    public bool ShowPercentage
    {
        get => (bool)GetValue(ShowPercentageProperty);
        set => SetValue(ShowPercentageProperty, value);
    }
    
    #endregion

    #region PercentageFormat Dependency Property
    
    public static readonly DependencyProperty PercentageFormatProperty =
        DependencyProperty.Register(nameof(PercentageFormat), typeof(string), typeof(ShadcnProgressBar),
            new PropertyMetadata("{0:F0}%"));

    /// <summary>
    /// Gets or sets the format string for percentage display
    /// </summary>
    public string PercentageFormat
    {
        get => (string)GetValue(PercentageFormatProperty);
        set => SetValue(PercentageFormatProperty, value);
    }
    
    #endregion

    #region Label Dependency Property
    
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(ShadcnProgressBar),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the label text for the progress bar
    /// </summary>
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
    
    #endregion

    #region CornerRadius Dependency Property
    
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ShadcnProgressBar),
            new PropertyMetadata(new CornerRadius(8)));

    /// <summary>
    /// Gets or sets the corner radius of the progress bar
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    #endregion

    #region IsAnimated Dependency Property
    
    public static readonly DependencyProperty IsAnimatedProperty =
        DependencyProperty.Register(nameof(IsAnimated), typeof(bool), typeof(ShadcnProgressBar),
            new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets whether progress changes should be animated
    /// </summary>
    public bool IsAnimated
    {
        get => (bool)GetValue(IsAnimatedProperty);
        set => SetValue(IsAnimatedProperty, value);
    }
    
    #endregion

    #region AnimationDuration Dependency Property
    
    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(ShadcnProgressBar),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(500))));

    /// <summary>
    /// Gets or sets the duration of progress animations
    /// </summary>
    public Duration AnimationDuration
    {
        get => (Duration)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }
    
    #endregion

    private static readonly DependencyPropertyKey PercentagePropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(Percentage), typeof(double), typeof(ShadcnProgressBar),
            new PropertyMetadata(0.0));

    public static readonly DependencyProperty PercentageProperty = PercentagePropertyKey.DependencyProperty;

    /// <summary>
    /// Gets the percentage value (0-100) based on current Value, Minimum, and Maximum
    /// </summary>
    public double Percentage
    {
        get => (double)GetValue(PercentageProperty);
        private set => SetValue(PercentagePropertyKey, value);
    }

    protected override void OnValueChanged(double oldValue, double newValue)
    {
        base.OnValueChanged(oldValue, newValue);
        UpdatePercentage();
    }


    protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
    {
        base.OnMinimumChanged(oldMinimum, newMinimum);
        UpdatePercentage();
    }

    protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
    {
        base.OnMaximumChanged(oldMaximum, newMaximum);
        UpdatePercentage();
    }

    private void UpdatePercentage()
    {
        if (Maximum == Minimum)
        {
            Percentage = 0;
        }
        else
        {
            Percentage = ((Value - Minimum) / (Maximum - Minimum)) * 100;
        }
    }
}

/// <summary>
/// Specifies the size of a ShadcnProgressBar
/// </summary>
public enum ProgressBarSize
{
    /// <summary>Small progress bar</summary>
    Small,
    /// <summary>Default progress bar size</summary>
    Default,
    /// <summary>Large progress bar</summary>
    Large
}

/// <summary>
/// Specifies the variant of a ShadcnProgressBar
/// </summary>
public enum ProgressBarVariant
{
    /// <summary>Default progress bar color</summary>
    Default,
    /// <summary>Primary progress bar color</summary>
    Primary,
    /// <summary>Secondary progress bar color</summary>
    Secondary,
    /// <summary>Success progress bar color</summary>
    Success,
    /// <summary>Warning progress bar color</summary>
    Warning,
    /// <summary>Destructive progress bar color</summary>
    Destructive,
    /// <summary>Accent progress bar color</summary>
    Accent
}
