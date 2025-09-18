using System.Windows.Controls;
using Shadcn.Wpf.Presentation.Pages;

namespace Shadcn.Wpf.Presentation.Services;

/// <summary>
/// Navigation service implementation
/// </summary>
public class NavigationService : INavigationService
{
    private Frame? _frame;

    public event EventHandler<NavigationEventArgs>? NavigationOccurred;

    /// <summary>
    /// Initialize the navigation service with a frame
    /// </summary>
    /// <param name="frame">The frame to use for navigation</param>
    public void Initialize(Frame frame)
    {
        _frame = frame;
    }

    /// <summary>
    /// Navigate to a page by tag
    /// </summary>
    /// <param name="pageTag">The page tag identifier</param>
    public void NavigateToPage(string pageTag)
    {
        if (_frame == null)
            throw new InvalidOperationException("Navigation service not initialized with a frame");

        Page? page = pageTag.ToLower() switch
        {
            "home" => new HomePage(),
            "buttons" => new ButtonsPage(),
            "cards" => new CardsPage(),
            "forms" => new FormsPage(),
            "radiobutton" => new RadioButtonPage(),
            "progress" => new ProgressPage(),
            "typography" => new TypographyPage(),
            "navigation" => new NavigationPage(),
            "tabcontrol" => new TabControlDemoPage(),
            "listbox" => new ListBoxDemoPage(),
            "datepicker" => new DatePickerDemoPage(),
            "toggleswitch" => new ToggleSwitchDemoPage(),
            "about" => new AboutPage(),
            _ => new HomePage()
        };

        if (page != null)
        {
            NavigateToPage(page);
            NavigationOccurred?.Invoke(this, new NavigationEventArgs(pageTag, page));
        }
    }

    /// <summary>
    /// Navigate to a specific page
    /// </summary>
    /// <param name="page">The page to navigate to</param>
    public void NavigateToPage(Page page)
    {
        if (_frame == null)
            throw new InvalidOperationException("Navigation service not initialized with a frame");

        _frame.Navigate(page);
    }
}
