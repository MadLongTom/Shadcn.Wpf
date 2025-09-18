using CommunityToolkit.Mvvm.ComponentModel;

namespace Shadcn.Wpf.Presentation.ViewModels;

/// <summary>
/// ViewModel for the RadioButtonPage
/// </summary>
public partial class RadioButtonPageViewModel : BasePageViewModel
{
    public RadioButtonPageViewModel() 
        : base("RadioButton", "A set of checkable buttons where no more than one can be checked at a time")
    {
    }
}
