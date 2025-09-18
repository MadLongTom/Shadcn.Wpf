using System.Windows;
using System.Windows.Media.Animation;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// Attached properties for animations
/// </summary>
public static class AnimationProperties
{
    #region AnimationWidth Attached Property
    
    /// <summary>
    /// Gets or sets the animation width value used for width animations
    /// </summary>
    public static readonly DependencyProperty AnimationWidthProperty =
        DependencyProperty.RegisterAttached(
            "AnimationWidth",
            typeof(double),
            typeof(AnimationProperties),
            new PropertyMetadata(0.0, OnAnimationWidthChanged));

    public static double GetAnimationWidth(DependencyObject obj)
    {
        return (double)obj.GetValue(AnimationWidthProperty);
    }

    public static void SetAnimationWidth(DependencyObject obj, double value)
    {
        obj.SetValue(AnimationWidthProperty, value);
    }

    private static void OnAnimationWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element)
        {
            element.Width = (double)e.NewValue;
        }
    }

    #endregion

    #region AnimationOpacity Attached Property
    
    /// <summary>
    /// Gets or sets the animation opacity value used for opacity animations
    /// </summary>
    public static readonly DependencyProperty AnimationOpacityProperty =
        DependencyProperty.RegisterAttached(
            "AnimationOpacity",
            typeof(double),
            typeof(AnimationProperties),
            new PropertyMetadata(1.0, OnAnimationOpacityChanged));

    public static double GetAnimationOpacity(DependencyObject obj)
    {
        return (double)obj.GetValue(AnimationOpacityProperty);
    }

    public static void SetAnimationOpacity(DependencyObject obj, double value)
    {
        obj.SetValue(AnimationOpacityProperty, value);
    }

    private static void OnAnimationOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element)
        {
            element.Opacity = (double)e.NewValue;
        }
    }

    #endregion

    #region AnimationScale Attached Property
    
    /// <summary>
    /// Gets or sets the animation scale value used for scale animations
    /// </summary>
    public static readonly DependencyProperty AnimationScaleProperty =
        DependencyProperty.RegisterAttached(
            "AnimationScale",
            typeof(double),
            typeof(AnimationProperties),
            new PropertyMetadata(1.0));

    public static double GetAnimationScale(DependencyObject obj)
    {
        return (double)obj.GetValue(AnimationScaleProperty);
    }

    public static void SetAnimationScale(DependencyObject obj, double value)
    {
        obj.SetValue(AnimationScaleProperty, value);
    }

    #endregion

    #region AnimationTranslateX Attached Property
    
    /// <summary>
    /// Gets or sets the animation translate X value used for translation animations
    /// </summary>
    public static readonly DependencyProperty AnimationTranslateXProperty =
        DependencyProperty.RegisterAttached(
            "AnimationTranslateX",
            typeof(double),
            typeof(AnimationProperties),
            new PropertyMetadata(0.0));

    public static double GetAnimationTranslateX(DependencyObject obj)
    {
        return (double)obj.GetValue(AnimationTranslateXProperty);
    }

    public static void SetAnimationTranslateX(DependencyObject obj, double value)
    {
        obj.SetValue(AnimationTranslateXProperty, value);
    }

    #endregion

    #region IsPressed Attached Property
    
    /// <summary>
    /// Gets or sets whether the element is in pressed state for animation purposes
    /// </summary>
    public static readonly DependencyProperty IsPressedProperty =
        DependencyProperty.RegisterAttached(
            "IsPressed",
            typeof(bool),
            typeof(AnimationProperties),
            new PropertyMetadata(false));

    public static bool GetIsPressed(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsPressedProperty);
    }

    public static void SetIsPressed(DependencyObject obj, bool value)
    {
        obj.SetValue(IsPressedProperty, value);
    }

    #endregion

    #region IsHovered Attached Property
    
    /// <summary>
    /// Gets or sets whether the element is in hovered state for animation purposes
    /// </summary>
    public static readonly DependencyProperty IsHoveredProperty =
        DependencyProperty.RegisterAttached(
            "IsHovered",
            typeof(bool),
            typeof(AnimationProperties),
            new PropertyMetadata(false));

    public static bool GetIsHovered(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsHoveredProperty);
    }

    public static void SetIsHovered(DependencyObject obj, bool value)
    {
        obj.SetValue(IsHoveredProperty, value);
    }

    #endregion
}
