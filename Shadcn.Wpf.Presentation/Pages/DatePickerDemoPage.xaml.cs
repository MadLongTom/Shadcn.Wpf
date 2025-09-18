using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

/// <summary>
/// Interaction logic for DatePickerDemoPage.xaml
/// </summary>
public partial class DatePickerDemoPage : Page
{
    public DatePickerDemoPage()
    {
        InitializeComponent();
        
        // Set the DataContext to the ViewModel using DI
        DataContext = Ioc.Default.GetService<DatePickerDemoPageViewModel>();
    }
}
