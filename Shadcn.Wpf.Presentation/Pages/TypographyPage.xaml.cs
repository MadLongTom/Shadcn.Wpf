using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

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
