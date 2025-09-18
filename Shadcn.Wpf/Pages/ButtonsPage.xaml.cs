using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.ViewModels;

namespace Shadcn.Wpf.Pages;

/// <summary>
/// Interaction logic for ButtonsPage.xaml
/// </summary>
public partial class ButtonsPage : Page
{
    public ButtonsPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<ButtonsPageViewModel>();
    }
}
