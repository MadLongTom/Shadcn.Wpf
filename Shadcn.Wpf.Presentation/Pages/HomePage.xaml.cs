using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

/// <summary>
/// Interaction logic for HomePage.xaml
/// </summary>
public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<HomePageViewModel>();
    }
}
