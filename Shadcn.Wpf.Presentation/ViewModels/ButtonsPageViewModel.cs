using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Services;
using Shadcn.Wpf.Themes;

namespace Shadcn.Wpf.Presentation.ViewModels;

/// <summary>
/// ViewModel for the ButtonsPage
/// </summary>
public partial class ButtonsPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private int _clickCount = 0;

    [ObservableProperty]
    private string _clickCountMessage = "Clicked 0 times";

    [ObservableProperty]
    private bool _isLoading = false;

    public ButtonsPageViewModel(IMessageService messageService)
        : base("Button Components", "Interactive button examples with different styles and states")
    {
        _messageService = messageService;
        UpdateClickCountMessage();
    }

    /// <summary>
    /// Command to handle click counter button click
    /// </summary>
    [RelayCommand]
    private void ClickCounter()
    {
        ClickCount++;
        UpdateClickCountMessage();
    }

    /// <summary>
    /// Command to toggle theme
    /// </summary>
    [RelayCommand]
    private void ToggleTheme()
    {
        try
        {
            ThemeManager.Instance.ToggleTheme();
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Failed to toggle theme: {ex.Message}", "Theme Error");
        }
    }

    /// <summary>
    /// Command to show message
    /// </summary>
    [RelayCommand]
    private void ShowMessage()
    {
        _messageService.ShowInformation("Hello from Shadcn.Wpf!", "Message");
    }

    /// <summary>
    /// Command to simulate async operation
    /// </summary>
    [RelayCommand]
    private async Task AsyncOperation()
    {
        IsLoading = true;
        try
        {
            await Task.Delay(2000); // Simulate work
            _messageService.ShowInformation("Async operation completed!", "Success");
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Async operation failed: {ex.Message}", "Error");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Command to show confirmation dialog
    /// </summary>
    [RelayCommand]
    private void ShowConfirmation()
    {
        var result = _messageService.ShowConfirmation("Are you sure you want to proceed?", "Confirmation");
        if (result)
        {
            _messageService.ShowInformation("You clicked Yes!", "Confirmed");
        }
        else
        {
            _messageService.ShowInformation("You clicked No!", "Cancelled");
        }
    }

    /// <summary>
    /// Command to reset counter
    /// </summary>
    [RelayCommand]
    private void ResetCounter()
    {
        ClickCount = 0;
        UpdateClickCountMessage();
    }

    /// <summary>
    /// Update click count message
    /// </summary>
    private void UpdateClickCountMessage()
    {
        ClickCountMessage = $"Clicked {ClickCount} times";
    }
}
