using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// ShadcnTextBlock control with Shadcn design system styling
/// </summary>
public class ShadcnTextBlock : TextBlock
{
    static ShadcnTextBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnTextBlock), 
            new FrameworkPropertyMetadata(typeof(ShadcnTextBlock)));
    }

    #region TextSize Dependency Property
    
    public static readonly DependencyProperty TextSizeProperty =
        DependencyProperty.Register(nameof(TextSize), typeof(TextBlockSize), typeof(ShadcnTextBlock),
            new PropertyMetadata(TextBlockSize.Default));

    /// <summary>
    /// Gets or sets the size of the text block
    /// </summary>
    public TextBlockSize TextSize
    {
        get => (TextBlockSize)GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
    }
    
    #endregion

    #region Variant Dependency Property
    
    public static readonly DependencyProperty VariantProperty =
        DependencyProperty.Register(nameof(Variant), typeof(TextBlockVariant), typeof(ShadcnTextBlock),
            new PropertyMetadata(TextBlockVariant.Default));

    /// <summary>
    /// Gets or sets the variant of the text block
    /// </summary>
    public TextBlockVariant Variant
    {
        get => (TextBlockVariant)GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }
    
    #endregion

    #region IsEmphasized Dependency Property
    
    public static readonly DependencyProperty IsEmphasizedProperty =
        DependencyProperty.Register(nameof(IsEmphasized), typeof(bool), typeof(ShadcnTextBlock),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text is emphasized (bold)
    /// </summary>
    public bool IsEmphasized
    {
        get => (bool)GetValue(IsEmphasizedProperty);
        set => SetValue(IsEmphasizedProperty, value);
    }
    
    #endregion

    #region IsItalic Dependency Property
    
    public static readonly DependencyProperty IsItalicProperty =
        DependencyProperty.Register(nameof(IsItalic), typeof(bool), typeof(ShadcnTextBlock),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text is italic
    /// </summary>
    public bool IsItalic
    {
        get => (bool)GetValue(IsItalicProperty);
        set => SetValue(IsItalicProperty, value);
    }
    
    #endregion

    #region IsUnderlined Dependency Property
    
    public static readonly DependencyProperty IsUnderlinedProperty =
        DependencyProperty.Register(nameof(IsUnderlined), typeof(bool), typeof(ShadcnTextBlock),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text is underlined
    /// </summary>
    public bool IsUnderlined
    {
        get => (bool)GetValue(IsUnderlinedProperty);
        set => SetValue(IsUnderlinedProperty, value);
    }
    
    #endregion

    #region IsStrikethrough Dependency Property
    
    public static readonly DependencyProperty IsStrikethroughProperty =
        DependencyProperty.Register(nameof(IsStrikethrough), typeof(bool), typeof(ShadcnTextBlock),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text has strikethrough
    /// </summary>
    public bool IsStrikethrough
    {
        get => (bool)GetValue(IsStrikethroughProperty);
        set => SetValue(IsStrikethroughProperty, value);
    }
    
    #endregion

    #region TextColor Dependency Property
    
    public static readonly DependencyProperty TextColorProperty =
        DependencyProperty.Register(nameof(TextColor), typeof(Brush), typeof(ShadcnTextBlock),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the custom text color (overrides variant color)
    /// </summary>
    public Brush TextColor
    {
        get => (Brush)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    
    #endregion

    #region LineHeight Dependency Property
    
    public static new readonly DependencyProperty LineHeightProperty =
        DependencyProperty.Register(nameof(LineHeight), typeof(double), typeof(ShadcnTextBlock),
            new PropertyMetadata(double.NaN));

    /// <summary>
    /// Gets or sets the line height
    /// </summary>
    public new double LineHeight
    {
        get => (double)GetValue(LineHeightProperty);
        set => SetValue(LineHeightProperty, value);
    }
    
    #endregion
}

/// <summary>
/// Specifies the size of a ShadcnTextBlock
/// </summary>
public enum TextBlockSize
{
    /// <summary>Extra small text</summary>
    ExtraSmall,
    /// <summary>Small text</summary>
    Small,
    /// <summary>Default text size</summary>
    Default,
    /// <summary>Large text</summary>
    Large,
    /// <summary>Extra large text</summary>
    ExtraLarge,
    /// <summary>Heading 1</summary>
    Heading1,
    /// <summary>Heading 2</summary>
    Heading2,
    /// <summary>Heading 3</summary>
    Heading3,
    /// <summary>Heading 4</summary>
    Heading4
}

/// <summary>
/// Specifies the variant of a ShadcnTextBlock
/// </summary>
public enum TextBlockVariant
{
    /// <summary>Default text color</summary>
    Default,
    /// <summary>Primary text color</summary>
    Primary,
    /// <summary>Secondary/muted text color</summary>
    Secondary,
    /// <summary>Success text color</summary>
    Success,
    /// <summary>Warning text color</summary>
    Warning,
    /// <summary>Destructive/error text color</summary>
    Destructive,
    /// <summary>Accent text color</summary>
    Accent
}
