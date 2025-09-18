using System.Windows;
using System.Windows.Controls;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// A styled checkbox control that matches the Shadcn design system
/// </summary>
public class ShadcnCheckBox : CheckBox
{
    static ShadcnCheckBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnCheckBox), 
            new FrameworkPropertyMetadata(typeof(ShadcnCheckBox)));
    }

    /// <summary>
    /// Gets or sets the size of the checkbox
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(CheckBoxSize), typeof(ShadcnCheckBox),
            new PropertyMetadata(CheckBoxSize.Default));

    public CheckBoxSize Size
    {
        get => (CheckBoxSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the checkbox has an error state
    /// </summary>
    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.Register(nameof(HasError), typeof(bool), typeof(ShadcnCheckBox),
            new PropertyMetadata(false));

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    /// <summary>
    /// Gets or sets the description text displayed below the checkbox
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(ShadcnCheckBox),
            new PropertyMetadata(string.Empty));

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
}

/// <summary>
/// Defines the available sizes for the ShadcnCheckBox
/// </summary>
public enum CheckBoxSize
{
    Small,
    Default,
    Large
}
