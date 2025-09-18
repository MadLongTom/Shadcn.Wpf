using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.ViewModels;

namespace Shadcn.Wpf.Pages;

/// <summary>
/// Interaction logic for CardsPage.xaml
/// </summary>
public partial class CardsPage : Page
{
    public CardsPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<CardsPageViewModel>();
    }
}
