using System.Windows;
using System.Windows.Controls;

namespace Shadcn.Wpf.Controls;

public enum ListBoxVariant
{
    Default,
    Outline,
    Ghost
}

public enum ListBoxSize
{
    Default,
    Small,
    Large
}

public class ShadcnListBox : ListBox
{
    static ShadcnListBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnListBox), new FrameworkPropertyMetadata(typeof(ShadcnListBox)));
    }

    public static readonly DependencyProperty VariantProperty =
        DependencyProperty.Register(nameof(Variant), typeof(ListBoxVariant), typeof(ShadcnListBox), 
            new FrameworkPropertyMetadata(ListBoxVariant.Default, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(ListBoxSize), typeof(ShadcnListBox), 
            new FrameworkPropertyMetadata(ListBoxSize.Default, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ShadcnListBox), 
            new FrameworkPropertyMetadata(new CornerRadius(6), FrameworkPropertyMetadataOptions.AffectsRender));

    public ListBoxVariant Variant
    {
        get => (ListBoxVariant)GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    public ListBoxSize Size
    {
        get => (ListBoxSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}

public class ShadcnListBoxItem : ListBoxItem
{
    static ShadcnListBoxItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnListBoxItem), new FrameworkPropertyMetadata(typeof(ShadcnListBoxItem)));
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ShadcnListBoxItem), 
            new FrameworkPropertyMetadata(new CornerRadius(4), FrameworkPropertyMetadataOptions.AffectsRender));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}
