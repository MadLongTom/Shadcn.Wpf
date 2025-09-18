using CommunityToolkit.Mvvm.ComponentModel;

namespace Shadcn.Wpf.ViewModels;

/// <summary>
/// Base class for all page ViewModels
/// </summary>
public abstract partial class BasePageViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isLoading = false;

    [ObservableProperty]
    private string _title = "";

    [ObservableProperty]
    private string _description = "";

    protected BasePageViewModel(string title, string description = "")
    {
        Title = title;
        Description = description;
    }

    /// <summary>
    /// Called when the page is loaded
    /// </summary>
    public virtual async Task OnLoadedAsync()
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// Called when the page is unloaded
    /// </summary>
    public virtual async Task OnUnloadedAsync()
    {
        await Task.CompletedTask;
    }
}
