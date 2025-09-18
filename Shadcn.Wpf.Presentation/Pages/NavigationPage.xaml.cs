using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

/// <summary>
/// Interaction logic for NavigationPage.xaml
/// </summary>
public partial class NavigationPage : Page
{
    public NavigationPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<NavigationPageViewModel>();
    }
}
