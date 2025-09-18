namespace Shadcn.Wpf.Services;

/// <summary>
/// Interface for message service
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Show an information message
    /// </summary>
    void ShowInformation(string message, string title = "Information");
    
    /// <summary>
    /// Show a warning message
    /// </summary>
    void ShowWarning(string message, string title = "Warning");
    
    /// <summary>
    /// Show an error message
    /// </summary>
    void ShowError(string message, string title = "Error");
    
    /// <summary>
    /// Show a confirmation dialog
    /// </summary>
    bool ShowConfirmation(string message, string title = "Confirmation");
}
