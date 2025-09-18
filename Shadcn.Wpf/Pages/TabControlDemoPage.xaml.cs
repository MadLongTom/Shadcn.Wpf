using System.Windows;
using System.Windows.Controls;
using Shadcn.Wpf.Controls;

namespace Shadcn.Wpf.Pages;

public partial class TabControlDemoPage : Page
{
    public TabControlDemoPage()
    {
        InitializeComponent();
    }

    private void OnCloseTab(object sender, RoutedEventArgs e)
    {
        if (e.Source is ShadcnTabItem tabItem && tabItem.Parent is ShadcnTabControl tabControl)
        {
            tabControl.Items.Remove(tabItem);
        }
    }

    private void AddTab_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ShadcnTabControl tabControl)
        {
            var newTabItem = new ShadcnTabItem
            {
                Header = $"Tab {tabControl.Items.Count + 1}",
                IsCloseable = true,
                Content = new TextBlock
                {
                    Text = $"This is the content of Tab {tabControl.Items.Count + 1}",
                    Margin = new Thickness(16),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            
            newTabItem.CloseTab += OnCloseTab;
            tabControl.Items.Add(newTabItem);
            newTabItem.IsSelected = true;
        }
    }

    private void RemoveTab_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is ShadcnTabControl tabControl)
        {
            if (tabControl.Items.Count > 1)
            {
                tabControl.Items.RemoveAt(tabControl.Items.Count - 1);
            }
        }
    }
}
