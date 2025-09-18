using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.ViewModels;

/// <summary>
/// ViewModel for the AboutPage
/// </summary>
public partial class AboutPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private string _version = "1.0.0";

    [ObservableProperty]
    private string _description = "Shadcn.Wpf is a modern WPF UI library that brings the elegance and simplicity of shadcn/ui to the WPF ecosystem.";

    [ObservableProperty]
    private List<string> _features = new();

    public AboutPageViewModel(IMessageService messageService) 
        : base("About Shadcn.Wpf", "Learn more about this project")
    {
        _messageService = messageService;
        InitializeFeatures();
    }

    /// <summary>
    /// Command to show more info
    /// </summary>
    [RelayCommand]
    private void ShowMoreInfo()
    {
        _messageService.ShowInformation(
            "Shadcn.Wpf provides beautifully designed components that you can copy and paste into your apps. " +
            "Accessible. Customizable. Open Source.",
            "More Information");
    }

    /// <summary>
    /// Command to open GitHub repository
    /// </summary>
    [RelayCommand]
    private void OpenGitHub()
    {
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/your-repo/shadcn.wpf",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Failed to open GitHub: {ex.Message}", "Error");
        }
    }

    private void InitializeFeatures()
    {
        Features = new List<string>
        {
            "🎨 Modern and elegant design",
            "🔧 Highly customizable components",
            "♿ Accessible by default",
            "📱 Responsive layouts",
            "🎭 Multiple themes support",
            "⚡ High performance",
            "📦 Easy to integrate",
            "🔄 MVVM pattern support"
        };
    }
}
