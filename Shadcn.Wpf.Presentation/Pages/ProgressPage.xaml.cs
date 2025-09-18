using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

public partial class ProgressPage : Page
{
    public ProgressPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<ProgressPageViewModel>();
    }
}
