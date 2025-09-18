using Shadcn.Wpf.Presentation.ViewModels;

namespace Shadcn.Wpf.Presentation.Pages;

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
