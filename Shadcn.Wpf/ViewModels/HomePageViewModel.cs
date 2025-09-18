using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.ViewModels;

/// <summary>
/// ViewModel for the HomePage
/// </summary>
public partial class HomePageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    public HomePageViewModel(IMessageService messageService) 
        : base("Welcome to Shadcn.Wpf", "A modern WPF UI library inspired by shadcn/ui")
    {
        _messageService = messageService;
    }

    public override async Task OnLoadedAsync()
    {
        await base.OnLoadedAsync();
        // Perform any initialization when the page loads
    }
}
