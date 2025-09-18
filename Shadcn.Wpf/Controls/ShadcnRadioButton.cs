using System.Windows;
using System.Windows.Controls;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// A styled radio button control that matches the Shadcn design system
/// </summary>
public class ShadcnRadioButton : RadioButton
{
    static ShadcnRadioButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnRadioButton), 
            new FrameworkPropertyMetadata(typeof(ShadcnRadioButton)));
    }

    /// <summary>
    /// Gets or sets the size of the radio button
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(RadioButtonSize), typeof(ShadcnRadioButton),
            new PropertyMetadata(RadioButtonSize.Default));

    public RadioButtonSize Size
    {
        get => (RadioButtonSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the radio button has an error state
    /// </summary>
    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.Register(nameof(HasError), typeof(bool), typeof(ShadcnRadioButton),
            new PropertyMetadata(false));

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets the description text displayed below the radio button
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(ShadcnRadioButton),
            new PropertyMetadata(string.Empty));

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
}

/// <summary>
/// Defines the available sizes for the ShadcnRadioButton
/// </summary>
public enum RadioButtonSize
{
    Small,
    Default,
    Large
}
