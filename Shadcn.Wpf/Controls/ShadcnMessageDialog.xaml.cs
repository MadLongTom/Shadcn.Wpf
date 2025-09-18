using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// Custom message dialog with Shadcn styling
/// </summary>
public partial class ShadcnMessageDialog : Window
{
    public enum MessageType
    {
        Information,
        Warning,
        Error,
        Confirmation
    }

    public enum MessageResult
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }

    public MessageResult Result { get; private set; } = MessageResult.None;

    public ShadcnMessageDialog()
    {
        InitializeComponent();
        
        // 窗口加载时播放弹出动画
        Loaded += OnWindowLoaded;
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        MainBorder.Opacity = 0;
        MainBorder.RenderTransform = new ScaleTransform(0.7, 0.7);
        
        
        // 播放弹出动画
        PlayShowAnimation();
    }

    /// <summary>
    /// Show information dialog
    /// </summary>
    public static void ShowInformation(string message, string title = "Information", Window? owner = null)
    {
        var dialog = new ShadcnMessageDialog();
        dialog.SetupDialog(MessageType.Information, title, message, MessageBoxButton.OK);
        if (owner != null)
            dialog.Owner = owner;
        dialog.ShowDialog();
    }

    /// <summary>
    /// Show warning dialog
    /// </summary>
    public static void ShowWarning(string message, string title = "Warning", Window? owner = null)
    {
        var dialog = new ShadcnMessageDialog();
        dialog.SetupDialog(MessageType.Warning, title, message, MessageBoxButton.OK);
        if (owner != null)
            dialog.Owner = owner;
        dialog.ShowDialog();
    }

    /// <summary>
    /// Show error dialog
    /// </summary>
    public static void ShowError(string message, string title = "Error", Window? owner = null)
    {
        var dialog = new ShadcnMessageDialog();
        dialog.SetupDialog(MessageType.Error, title, message, MessageBoxButton.OK);
        if (owner != null)
            dialog.Owner = owner;
        dialog.ShowDialog();
    }

    /// <summary>
    /// Show confirmation dialog
    /// </summary>
    public static bool ShowConfirmation(string message, string title = "Confirmation", Window? owner = null)
    {
        var dialog = new ShadcnMessageDialog();
        dialog.SetupDialog(MessageType.Confirmation, title, message, MessageBoxButton.YesNo);
        if (owner != null)
            dialog.Owner = owner;
        dialog.ShowDialog();
        return dialog.Result == MessageResult.Yes;
    }

    /// <summary>
    /// Setup the dialog with specified parameters
    /// </summary>
    private void SetupDialog(MessageType type, string title, string message, MessageBoxButton buttons)
    {
        Title = title;
        TitleTextBlock.Text = title;
        MessageTextBlock.Text = message;

        // Set icon and color based on message type
        SetupIcon(type);
        
        // Setup buttons
        SetupButtons(buttons);
    }

    /// <summary>
    /// Setup icon based on message type
    /// </summary>
    private void SetupIcon(MessageType type)
    {
        switch (type)
        {
            case MessageType.Information:
                IconTextBlock.Text = "ℹ️";
                IconTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(59, 130, 246)); // Blue
                break;
            case MessageType.Warning:
                IconTextBlock.Text = "⚠️";
                IconTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(245, 158, 11)); // Amber
                break;
            case MessageType.Error:
                IconTextBlock.Text = "❌";
                IconTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(239, 68, 68)); // Red
                break;
            case MessageType.Confirmation:
                IconTextBlock.Text = "❓";
                IconTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(168, 85, 247)); // Purple
                break;
        }
    }

    /// <summary>
    /// Setup buttons based on MessageBoxButton type
    /// </summary>
    private void SetupButtons(MessageBoxButton buttons)
    {
        ButtonPanel.Children.Clear();

        switch (buttons)
        {
            case MessageBoxButton.OK:
                var okButton = CreateButton("OK", MessageResult.OK, isPrimary: true);
                ButtonPanel.Children.Add(okButton);
                okButton.Focus();
                break;

            case MessageBoxButton.YesNo:
                var noButton = CreateButton("No", MessageResult.No);
                var yesButton = CreateButton("Yes", MessageResult.Yes, isPrimary: true);
                
                ButtonPanel.Children.Add(noButton);
                ButtonPanel.Children.Add(yesButton);
                yesButton.Focus();
                break;

            case MessageBoxButton.YesNoCancel:
                var cancelButton = CreateButton("Cancel", MessageResult.Cancel);
                var noButton2 = CreateButton("No", MessageResult.No);
                var yesButton2 = CreateButton("Yes", MessageResult.Yes, isPrimary: true);
                
                ButtonPanel.Children.Add(cancelButton);
                ButtonPanel.Children.Add(noButton2);
                ButtonPanel.Children.Add(yesButton2);
                yesButton2.Focus();
                break;

            case MessageBoxButton.OKCancel:
                var cancelButton2 = CreateButton("Cancel", MessageResult.Cancel);
                var okButton2 = CreateButton("OK", MessageResult.OK, isPrimary: true);
                
                ButtonPanel.Children.Add(cancelButton2);
                ButtonPanel.Children.Add(okButton2);
                okButton2.Focus();
                break;
        }
    }

    /// <summary>
    /// Create a button with specified result
    /// </summary>
    private ShadcnButton CreateButton(string content, MessageResult result, bool isPrimary = false)
    {
        var button = new ShadcnButton
        {
            Content = content,
            Margin = new Thickness(8, 0, 0, 0),
            MinWidth = 80,
            Variant = isPrimary ? ButtonVariant.Default : ButtonVariant.Outline
        };

        button.Click += (s, e) =>
        {
            Result = result;
            DialogResult = result != MessageResult.Cancel && result != MessageResult.No;
            PlayHideAnimation();
        };

        return button;
    }

    /// <summary>
    /// Handle close button click
    /// </summary>
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Result = MessageResult.Cancel;
        DialogResult = false;
        PlayHideAnimation();
    }

    /// <summary>
    /// Handle Escape key
    /// </summary>
    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Escape)
        {
            Result = MessageResult.Cancel;
            DialogResult = false;
            PlayHideAnimation();
        }
        base.OnKeyDown(e);
    }

    /// <summary>
    /// 播放弹出动画
    /// </summary>
    private void PlayShowAnimation()
    {
        var storyboard = (Storyboard)FindResource("ShowAnimation");
        storyboard.Begin();
    }

    /// <summary>
    /// 播放关闭动画
    /// </summary>
    private void PlayHideAnimation()
    {
        if (_isClosing) return; // 防止重复调用
        
        _isClosing = true;
        var storyboard = (Storyboard)FindResource("HideAnimation");
        
        // 移除之前可能存在的事件处理器
        storyboard.Completed -= OnHideAnimationCompleted;
        storyboard.Completed += OnHideAnimationCompleted;
        
        storyboard.Begin();
    }

    private void OnHideAnimationCompleted(object? sender, EventArgs e)
    {
        // 直接关闭窗口，不再触发OnClosing
        try
        {
            Close();
        }
        catch
        {
            // 忽略可能的异常
        }
    }

    /// <summary>
    /// 重写关闭方法以播放动画
    /// </summary>
    private bool _isClosing = false;
    
    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        // 如果已经在播放关闭动画或动画已完成，允许直接关闭
        if (_isClosing)
        {
            base.OnClosing(e);
            return;
        }

        // 取消默认关闭，播放动画
        e.Cancel = true;
        PlayHideAnimation();
    }
}
