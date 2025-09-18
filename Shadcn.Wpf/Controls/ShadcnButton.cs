using System.Windows;
using System.Windows.Controls;

namespace Shadcn.Wpf.Controls;

public enum ButtonVariant
{
    Default,
    Primary,
    Destructive,
    Outline,
    Secondary,
    Ghost,
    Link
}

public enum ButtonSize
{
    Default,
    Small,
    Large,
    Icon
}

public class ShadcnButton : Button
{
    public static readonly DependencyProperty VariantProperty =
        DependencyProperty.Register(nameof(Variant), typeof(ButtonVariant), typeof(ShadcnButton),
            new PropertyMetadata(ButtonVariant.Default, OnVariantChanged));

    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(ButtonSize), typeof(ShadcnButton),
            new PropertyMetadata(ButtonSize.Default, OnSizeChanged));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(ShadcnButton),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IsLoadingProperty =
        DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(ShadcnButton),
            new PropertyMetadata(false, OnIsLoadingChanged));

    public ButtonVariant Variant
    {
        get => (ButtonVariant)GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    public ButtonSize Size
    {
        get => (ButtonSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    static ShadcnButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnButton), new FrameworkPropertyMetadata(typeof(ShadcnButton)));
    }

    private static void OnVariantChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnButton button)
        {
            button.UpdateButtonStyle();
        }
    }

    private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnButton button)
        {
            button.UpdateButtonStyle();
        }
    }

    private static void OnIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnButton button)
        {
            button.IsEnabled = !(bool)e.NewValue;
        }
    }

    private void UpdateButtonStyle()
    {
        // The style will be applied through the template
        InvalidateVisual();
    }
}
