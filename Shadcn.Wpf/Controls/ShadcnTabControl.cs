using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Shadcn.Wpf.Controls;

public enum TabVariant
{
    Default,
    Pills
}

public enum TabSize
{
    Default,
    Small,
    Large
}

public class ShadcnTabControl : TabControl
{
    private ContentPresenter? _contentPresenter;
    private Storyboard? _fadeInStoryboard;
    private Storyboard? _fadeOutStoryboard;
    private bool _isAnimating = false;
    private object? _pendingSelectedItem;

    public static readonly DependencyProperty VariantProperty =
        DependencyProperty.Register(nameof(Variant), typeof(TabVariant), typeof(ShadcnTabControl),
            new PropertyMetadata(TabVariant.Default, OnVariantChanged));

    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(TabSize), typeof(ShadcnTabControl),
            new PropertyMetadata(TabSize.Default, OnSizeChanged));

    public static readonly DependencyProperty EnableAnimationProperty =
        DependencyProperty.Register(nameof(EnableAnimation), typeof(bool), typeof(ShadcnTabControl),
            new PropertyMetadata(true));

    public TabVariant Variant
    {
        get => (TabVariant)GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    public TabSize Size
    {
        get => (TabSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public bool EnableAnimation
    {
        get => (bool)GetValue(EnableAnimationProperty);
        set => SetValue(EnableAnimationProperty, value);
    }

    static ShadcnTabControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnTabControl), 
            new FrameworkPropertyMetadata(typeof(ShadcnTabControl)));
    }

    public ShadcnTabControl()
    {
        InitializeAnimations();
    }

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        if (!EnableAnimation || _contentPresenter == null || _isAnimating)
        {
            base.OnSelectionChanged(e);
            return;
        }

        if (e.RemovedItems.Count > 0 && e.AddedItems.Count > 0)
        {
            // Store the current selection to restore it temporarily
            var oldSelectedItem = e.RemovedItems[0];
            _pendingSelectedItem = e.AddedItems[0];
            
            // Temporarily revert to old selection for animation
            _isAnimating = true;
            SelectedItem = oldSelectedItem;
            
            // Start animation sequence
            AnimateTabChange();
            
            // Don't call base method to prevent double selection change
            return;
        }
        
        base.OnSelectionChanged(e);
    }

    private void InitializeAnimations()
    {
        // Fade out animation
        _fadeOutStoryboard = new Storyboard();
        var fadeOutAnimation = new DoubleAnimation
        {
            From = 1.0,
            To = 0.0,
            Duration = new Duration(TimeSpan.FromMilliseconds(100)),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath("Opacity"));
        _fadeOutStoryboard.Children.Add(fadeOutAnimation);

        // Fade in animation
        _fadeInStoryboard = new Storyboard();
        var fadeInAnimation = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = new Duration(TimeSpan.FromMilliseconds(150)),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
        };
        Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));
        _fadeInStoryboard.Children.Add(fadeInAnimation);

        // Setup animation completion handlers
        _fadeOutStoryboard.Completed += OnFadeOutCompleted;
        _fadeInStoryboard.Completed += OnFadeInCompleted;
    }

    private void OnFadeOutCompleted(object? sender, EventArgs e)
    {
        if (_pendingSelectedItem != null)
        {
            // Now change the selection to the new item
            var itemToSelect = _pendingSelectedItem;
            _pendingSelectedItem = null;
            
            // Change selection
            SelectedItem = itemToSelect;
            
            // Start fade in
            if (_contentPresenter != null)
            {
                Storyboard.SetTarget(_fadeInStoryboard, _contentPresenter);
                _fadeInStoryboard.Begin();
            }
            else
            {
                _isAnimating = false;
            }
        }
    }

    private void OnFadeInCompleted(object? sender, EventArgs e)
    {
        _isAnimating = false;
    }

    private void AnimateTabChange()
    {
        if (_contentPresenter == null) return;
        
        // Set target for fade out animation
        Storyboard.SetTarget(_fadeOutStoryboard, _contentPresenter);
        _fadeOutStoryboard.Begin();
    }

    private static void OnVariantChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnTabControl tabControl)
        {
            tabControl.UpdateVariantStyle();
        }
    }

    private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnTabControl tabControl)
        {
            tabControl.UpdateSizeStyle();
        }
    }

    private void UpdateVariantStyle()
    {
        // Style updates will be handled by triggers in the style
    }

    private void UpdateSizeStyle()
    {
        // Style updates will be handled by triggers in the style
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        
        // Get the content presenter for animations
        _contentPresenter = GetTemplateChild("PART_SelectedContentHost") as ContentPresenter;
        
        if (_contentPresenter != null && EnableAnimation)
        {
            // Set initial opacity
            _contentPresenter.Opacity = 1.0;
        }
    }
}

public class ShadcnTabItem : TabItem
{
    private Border? _mainBorder;
    private Storyboard? _hoverInStoryboard;
    private Storyboard? _hoverOutStoryboard;
    private Storyboard? _selectedStoryboard;

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(ShadcnTabItem),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IsCloseableProperty =
        DependencyProperty.Register(nameof(IsCloseable), typeof(bool), typeof(ShadcnTabItem),
            new PropertyMetadata(false));

    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsCloseable
    {
        get => (bool)GetValue(IsCloseableProperty);
        set => SetValue(IsCloseableProperty, value);
    }

    static ShadcnTabItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnTabItem), 
            new FrameworkPropertyMetadata(typeof(ShadcnTabItem)));
    }

    public ShadcnTabItem()
    {
        InitializeAnimations();
        MouseEnter += OnMouseEnter;
        MouseLeave += OnMouseLeave;
    }

    private void InitializeAnimations()
    {
        // Hover in animation
        _hoverInStoryboard = new Storyboard();
        var hoverInAnimation = new DoubleAnimation
        {
            To = 1.0,
            Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        Storyboard.SetTargetProperty(hoverInAnimation, new PropertyPath("Background.Opacity"));
        _hoverInStoryboard.Children.Add(hoverInAnimation);

        // Hover out animation
        _hoverOutStoryboard = new Storyboard();
        var hoverOutAnimation = new DoubleAnimation
        {
            To = 0.0,
            Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        Storyboard.SetTargetProperty(hoverOutAnimation, new PropertyPath("Background.Opacity"));
        _hoverOutStoryboard.Children.Add(hoverOutAnimation);

        // Selected animation with scale
        _selectedStoryboard = new Storyboard();
        var scaleTransform = new ScaleTransform(1.0, 1.0);
        var scaleXAnimation = new DoubleAnimation
        {
            From = 0.95,
            To = 1.0,
            Duration = new Duration(TimeSpan.FromMilliseconds(150)),
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
        };
        var scaleYAnimation = new DoubleAnimation
        {
            From = 0.95,
            To = 1.0,
            Duration = new Duration(TimeSpan.FromMilliseconds(150)),
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
        };
        Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));
        Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));
        _selectedStoryboard.Children.Add(scaleXAnimation);
        _selectedStoryboard.Children.Add(scaleYAnimation);
    }

    private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!IsSelected && _mainBorder != null)
        {
            Storyboard.SetTarget(_hoverInStoryboard, _mainBorder);
            _hoverInStoryboard.Begin();
        }
    }

    private void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!IsSelected && _mainBorder != null)
        {
            Storyboard.SetTarget(_hoverOutStoryboard, _mainBorder);
            _hoverOutStoryboard.Begin();
        }
    }

    protected override void OnSelected(RoutedEventArgs e)
    {
        base.OnSelected(e);
        
        if (_mainBorder != null)
        {
            Storyboard.SetTarget(_selectedStoryboard, _mainBorder);
            _selectedStoryboard.Begin();
        }
    }

    public static readonly RoutedEvent CloseTabEvent =
        EventManager.RegisterRoutedEvent(nameof(CloseTab), RoutingStrategy.Bubble, 
            typeof(RoutedEventHandler), typeof(ShadcnTabItem));

    public event RoutedEventHandler CloseTab
    {
        add => AddHandler(CloseTabEvent, value);
        remove => RemoveHandler(CloseTabEvent, value);
    }

    protected virtual void OnCloseTab()
    {
        RaiseEvent(new RoutedEventArgs(CloseTabEvent, this));
    }

    public void RaiseCloseTabEvent()
    {
        OnCloseTab();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        
        // Get the main border for animations
        _mainBorder = GetTemplateChild("mainBorder") as Border;
        
        if (_mainBorder != null)
        {
            // Set render transform for scale animations
            _mainBorder.RenderTransform = new ScaleTransform(1.0, 1.0);
            _mainBorder.RenderTransformOrigin = new Point(0.5, 0.5);
        }
        
        // Find and handle close button click
        if (GetTemplateChild("PART_CloseButton") is Button closeButton)
        {
            closeButton.Click += (s, e) => OnCloseTab();
        }
    }
}
