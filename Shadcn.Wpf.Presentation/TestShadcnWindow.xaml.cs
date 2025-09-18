using System.Windows;
using Shadcn.Wpf.Controls;

namespace Shadcn.Wpf.Presentation;

public partial class TestShadcnWindow : ShadcnWindow
{
    public TestShadcnWindow()
    {
        InitializeComponent();
    }

    private void OnNewWindowClick(object sender, RoutedEventArgs e)
    {
        var newWindow = new TestShadcnWindow
        {
            Title = "新建 ShadcnWindow",
            Width = 600,
            Height = 400,
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
        newWindow.Show();
    }

    private void OnToggleTitleBarClick(object sender, RoutedEventArgs e)
    {
        ShowTitleBar = !ShowTitleBar;
        ShowTitleBarCheckBox.IsChecked = ShowTitleBar;
    }

    private void OnToggleShadowClick(object sender, RoutedEventArgs e)
    {
        EnableDropShadow = !EnableDropShadow;
        EnableDropShadowCheckBox.IsChecked = EnableDropShadow;
    }

    private void OnShowTitleBarChanged(object sender, RoutedEventArgs e)
    {
        if (ShowTitleBarCheckBox.IsChecked.HasValue)
        {
            ShowTitleBar = ShowTitleBarCheckBox.IsChecked.Value;
        }
    }

    private void OnShowSystemButtonsChanged(object sender, RoutedEventArgs e)
    {
        if (ShowSystemButtonsCheckBox.IsChecked.HasValue)
        {
            ShowSystemButtons = ShowSystemButtonsCheckBox.IsChecked.Value;
        }
    }

    private void OnShowIconChanged(object sender, RoutedEventArgs e)
    {
        if (ShowIconCheckBox.IsChecked.HasValue)
        {
            ShowIcon = ShowIconCheckBox.IsChecked.Value;
        }
    }

    private void OnEnableDropShadowChanged(object sender, RoutedEventArgs e)
    {
        if (EnableDropShadowCheckBox.IsChecked.HasValue)
        {
            EnableDropShadow = EnableDropShadowCheckBox.IsChecked.Value;
        }
    }
}
