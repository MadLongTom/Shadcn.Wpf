using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

/// <summary>
/// Interaction logic for FormsPage.xaml
/// </summary>
public partial class FormsPage : Page
{
    public FormsPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<FormsPageViewModel>();
    }
}
