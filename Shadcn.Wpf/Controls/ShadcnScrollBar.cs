using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// Shadcn-styled ScrollBar control
/// </summary>
public class ShadcnScrollBar : ScrollBar
{
    static ShadcnScrollBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnScrollBar), 
            new FrameworkPropertyMetadata(typeof(ShadcnScrollBar)));
    }

    #region ThumbSize Dependency Property

    public static readonly DependencyProperty ThumbSizeProperty =
        DependencyProperty.Register(nameof(ThumbSize), typeof(double), typeof(ShadcnScrollBar),
            new PropertyMetadata(8.0));

    /// <summary>
    /// Gets or sets the size of the scroll thumb
    /// </summary>
    public double ThumbSize
    {
        get => (double)GetValue(ThumbSizeProperty);
        set => SetValue(ThumbSizeProperty, value);
    }

    #endregion

    #region TrackSize Dependency Property

    public static readonly DependencyProperty TrackSizeProperty =
        DependencyProperty.Register(nameof(TrackSize), typeof(double), typeof(ShadcnScrollBar),
            new PropertyMetadata(12.0));

    /// <summary>
    /// Gets or sets the size of the scroll track
    /// </summary>
    public double TrackSize
    {
        get => (double)GetValue(TrackSizeProperty);
        set => SetValue(TrackSizeProperty, value);
    }

    #endregion

    #region AutoHide Dependency Property

    public static readonly DependencyProperty AutoHideProperty =
        DependencyProperty.Register(nameof(AutoHide), typeof(bool), typeof(ShadcnScrollBar),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the scrollbar should auto-hide when not in use
    /// </summary>
    public bool AutoHide
    {
        get => (bool)GetValue(AutoHideProperty);
        set => SetValue(AutoHideProperty, value);
    }

    #endregion
}
