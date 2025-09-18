using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

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
