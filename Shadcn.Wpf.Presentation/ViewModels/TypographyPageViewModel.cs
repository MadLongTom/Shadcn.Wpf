using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.Presentation.ViewModels;

/// <summary>
/// ViewModel for the TypographyPage
/// </summary>
public partial class TypographyPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    public TypographyPageViewModel(IMessageService messageService) 
        : base("Typography", "Text styles and formatting options")
    {
        _messageService = messageService;
    }
}
