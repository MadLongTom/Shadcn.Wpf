using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.ViewModels;

namespace Shadcn.Wpf.Pages;

public partial class ProgressPage : Page
{
    public ProgressPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<ProgressPageViewModel>();
    }
}
