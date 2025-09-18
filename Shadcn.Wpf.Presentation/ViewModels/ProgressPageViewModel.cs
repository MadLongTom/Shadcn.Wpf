using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Services;
using System.Windows;

namespace Shadcn.Wpf.Presentation.ViewModels;

/// <summary>
/// ViewModel for the ProgressPage
/// </summary>
public partial class ProgressPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private double _progressValue = 0;

    [ObservableProperty]
    private bool _isProgressRunning = false;

    [ObservableProperty]
    private string _progressText = "0%";

    [ObservableProperty]
    private double _interactiveProgressValue = 30;

    [ObservableProperty]
    private double _downloadProgressValue = 0;

    [ObservableProperty]
    private bool _isDownloadRunning = false;

    public ProgressPageViewModel(IMessageService messageService)
        : base("Progress Components", "Progress bars and loading indicators")
    {
        _messageService = messageService;
    }

    /// <summary>
    /// Command to start progress simulation
    /// </summary>
    [RelayCommand]
    private async Task StartProgress()
    {
        if (IsProgressRunning) return;

        IsProgressRunning = true;
        ProgressValue = 0;

        try
        {
            for (int i = 0; i <= 100; i += 5)
            {
                ProgressValue = i;
                ProgressText = $"{i}%";
                await Task.Delay(200);
            }

            _messageService.ShowInformation("Progress completed!", "Success");
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Progress failed: {ex.Message}", "Error");
        }
        finally
        {
            IsProgressRunning = false;
        }
    }

    /// <summary>
    /// Command to reset progress
    /// </summary>
    [RelayCommand]
    private void ResetProgress()
    {
        if (IsProgressRunning) return;

        ProgressValue = 0;
        ProgressText = "0%";
    }

    /// <summary>
    /// Command to add 10% to interactive progress
    /// </summary>
    [RelayCommand]
    private void AddProgress()
    {
        InteractiveProgressValue = Math.Min(100, InteractiveProgressValue + 10);
    }

    /// <summary>
    /// Command to reset interactive progress
    /// </summary>
    [RelayCommand]
    private void ResetInteractiveProgress()
    {
        InteractiveProgressValue = 0;
    }

    /// <summary>
    /// Command to start simulated download
    /// </summary>
    [RelayCommand]
    private async Task StartDownload()
    {
        if (IsDownloadRunning) return;

        IsDownloadRunning = true;
        DownloadProgressValue = 0;

        try
        {
            while(DownloadProgressValue < 100)
            {
                DownloadProgressValue += new Random().Next(0, 3);
                if (DownloadProgressValue > 100) DownloadProgressValue = 100;
                await Task.Delay(30);
            }

            _messageService.ShowInformation("Download completed!", "Success");
        }
        catch (Exception ex)
        {
            _messageService.ShowError($"Download failed: {ex.Message}", "Error");
        }
        finally
        {
            IsDownloadRunning = false;
        }
    }
}
