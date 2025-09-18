using Shadcn.Wpf.ViewModels;

namespace Shadcn.Wpf.Pages;

/// <summary>
/// Interaction logic for RadioButtonPage.xaml
/// </summary>
public partial class RadioButtonPage : System.Windows.Controls.Page
{
    public RadioButtonPage()
    {
        InitializeComponent();
        DataContext = new RadioButtonPageViewModel();
    }
}
