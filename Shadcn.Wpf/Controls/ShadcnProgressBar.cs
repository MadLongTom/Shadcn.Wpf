using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// ShadcnProgressBar control with Shadcn design system styling
/// </summary>
public class ShadcnProgressBar : ProgressBar
{
    private double _pendingTargetValue;
    private bool _isAnimating;
    private bool _hasPendingAnimation;

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
        
        // 如果不是通过动画设置的值，更新 _pendingTargetValue
        if (!_isAnimating)
        {
            _pendingTargetValue = newValue;
        }
        
        UpdatePercentage();
    }

    /// <summary>
    /// 以动画方式设置进度条值
    /// </summary>
    public void AnimateToValue(double targetValue)
    {
        _pendingTargetValue = targetValue;

        if (!IsAnimated || Math.Abs(Value - targetValue) < 0.01)
        {
            Value = targetValue;
            return;
        }

        // 如果当前正在动画，标记有待处理的动画
        if (_isAnimating)
        {
            _hasPendingAnimation = true;
            return;
        }

        StartAnimation(targetValue);
    }

    private void StartAnimation(double targetValue)
    {
        var animation = new DoubleAnimation
        {
            From = Value,
            To = targetValue,
            Duration = new Duration(TimeSpan.FromMilliseconds(300)),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        animation.Completed += OnAnimationCompleted;

        _isAnimating = true;
        BeginAnimation(ValueProperty, animation);
    }

    private void OnAnimationCompleted(object? sender, EventArgs e)
    {
        _isAnimating = false;
        
        // 确保最终值是正确的
        Value = _pendingTargetValue;

        // 如果有待处理的动画，启动它
        if (_hasPendingAnimation)
        {
            _hasPendingAnimation = false;
            StartAnimation(_pendingTargetValue);
        }
    }

    /// <summary>
    /// 获取当前目标值（可能正在动画中）
    /// </summary>
    public double GetTargetValue()
    {
        // 如果正在动画或有待处理的动画，返回目标值
        // 否则返回当前值
        return (_isAnimating || _hasPendingAnimation) ? _pendingTargetValue : Value;
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
