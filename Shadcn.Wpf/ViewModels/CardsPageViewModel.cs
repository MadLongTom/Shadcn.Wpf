using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.ViewModels;

/// <summary>
/// ViewModel for the CardsPage
/// </summary>
public partial class CardsPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private string _cardTitle = "Sample Card";

    [ObservableProperty]
    private string _cardDescription = "This is a sample card component showing how cards work in Shadcn.Wpf";

    [ObservableProperty]
    private bool _isDarkMode = false;

    public CardsPageViewModel(IMessageService messageService) 
        : base("Card Components", "Explore different card layouts and styles")
    {
        _messageService = messageService;
    }

    /// <summary>
    /// Command to toggle dark mode
    /// </summary>
    [RelayCommand]
    private void ToggleDarkMode()
    {
        IsDarkMode = !IsDarkMode;
        // You can add logic here to actually change the theme
        _messageService.ShowInformation($"Dark mode {(IsDarkMode ? "enabled" : "disabled")}", "Theme");
    }
}
