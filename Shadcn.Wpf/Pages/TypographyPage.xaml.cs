using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.ViewModels;

namespace Shadcn.Wpf.Pages;

/// <summary>
/// Interaction logic for TypographyPage.xaml
/// </summary>
public partial class TypographyPage : Page
{
    public TypographyPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<TypographyPageViewModel>();
    }
}
