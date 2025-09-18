using System.Windows;
using System.Windows.Controls;

namespace Shadcn.Wpf.Controls;

public class ShadcnTextBox : TextBox
{
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(ShadcnTextBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty IsInvalidProperty =
        DependencyProperty.Register(nameof(IsInvalid), typeof(bool), typeof(ShadcnTextBox),
            new PropertyMetadata(false));

    public static readonly DependencyProperty HelperTextProperty =
        DependencyProperty.Register(nameof(HelperText), typeof(string), typeof(ShadcnTextBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.Register(nameof(HasError), typeof(bool), typeof(ShadcnTextBox),
            new PropertyMetadata(false));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public bool IsInvalid
    {
        get => (bool)GetValue(IsInvalidProperty);
        set => SetValue(IsInvalidProperty, value);
    }

    public string HelperText
    {
        get => (string)GetValue(HelperTextProperty);
        set => SetValue(HelperTextProperty, value);
    }

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    static ShadcnTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnTextBox), new FrameworkPropertyMetadata(typeof(ShadcnTextBox)));
    }
}
