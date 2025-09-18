using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

/// <summary>
/// Interaction logic for ToggleSwitchDemoPage.xaml
/// </summary>
public partial class ToggleSwitchDemoPage : Page
{
    public ToggleSwitchDemoPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<ToggleSwitchDemoPageViewModel>();
    }
}
