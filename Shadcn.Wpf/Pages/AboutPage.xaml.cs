using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.ViewModels;

namespace Shadcn.Wpf.Pages;

/// <summary>
/// Interaction logic for AboutPage.xaml
/// </summary>
public partial class AboutPage : Page
{
    public AboutPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<AboutPageViewModel>();
    }
}
