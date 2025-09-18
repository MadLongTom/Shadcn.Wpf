using System.Windows;
using System.Windows.Controls.Primitives;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// A styled toggle switch control that matches the Shadcn design system
/// </summary>
public class ShadcnToggleSwitch : ToggleButton
{
    static ShadcnToggleSwitch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnToggleSwitch), 
            new FrameworkPropertyMetadata(typeof(ShadcnToggleSwitch)));
    }

    /// <summary>
    /// Gets or sets the size of the toggle switch
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(ToggleSwitchSize), typeof(ShadcnToggleSwitch),
            new PropertyMetadata(ToggleSwitchSize.Default));

    public ToggleSwitchSize Size
    {
        get => (ToggleSwitchSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the toggle switch has an error state
    /// </summary>
    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.Register(nameof(HasError), typeof(bool), typeof(ShadcnToggleSwitch),
            new PropertyMetadata(false));

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets the description text displayed below the toggle switch
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(ShadcnToggleSwitch),
            new PropertyMetadata(string.Empty));

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed when the toggle switch is in the "on" state
    /// </summary>
    public static readonly DependencyProperty OnTextProperty =
        DependencyProperty.Register(nameof(OnText), typeof(string), typeof(ShadcnToggleSwitch),
            new PropertyMetadata(string.Empty));

    public string OnText
    {
        get => (string)GetValue(OnTextProperty);
        set => SetValue(OnTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed when the toggle switch is in the "off" state
    /// </summary>
    public static readonly DependencyProperty OffTextProperty =
        DependencyProperty.Register(nameof(OffText), typeof(string), typeof(ShadcnToggleSwitch),
            new PropertyMetadata(string.Empty));

    public string OffText
    {
        get => (string)GetValue(OffTextProperty);
        set => SetValue(OffTextProperty, value);
    }
}

/// <summary>
/// Defines the available sizes for the ShadcnToggleSwitch
/// </summary>
public enum ToggleSwitchSize
{
    Small,
    Default,
    Large
}
