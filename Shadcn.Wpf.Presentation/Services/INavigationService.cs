using System.Windows.Controls;

namespace Shadcn.Wpf.Presentation.Services;

/// <summary>
/// Interface for navigation service
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigate to a page by tag
    /// </summary>
    /// <param name="pageTag">The page tag identifier</param>
    void NavigateToPage(string pageTag);
    
    /// <summary>
    /// Navigate to a specific page
    /// </summary>
    /// <param name="page">The page to navigate to</param>
    void NavigateToPage(Page page);
    
    /// <summary>
    /// Event raised when navigation occurs
    /// </summary>
    event EventHandler<NavigationEventArgs>? NavigationOccurred;
}

/// <summary>
/// Navigation event arguments
/// </summary>
public class NavigationEventArgs : EventArgs
{
    public string PageTag { get; }
    public Page Page { get; }
    
    public NavigationEventArgs(string pageTag, Page page)
    {
        PageTag = pageTag;
        Page = page;
    }
}
