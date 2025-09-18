using System.Windows;
using Shadcn.Wpf.Controls;

namespace Shadcn.Wpf.Services;

/// <summary>
/// Message service implementation using custom Shadcn message dialogs
/// </summary>
public class MessageService : IMessageService
{
    /// <summary>
    /// Get the current active window to use as owner
    /// </summary>
    private static Window? GetOwnerWindow()
    {
        return Application.Current?.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive) 
               ?? Application.Current?.MainWindow;
    }

    /// <summary>
    /// Show an information message
    /// </summary>
    public void ShowInformation(string message, string title = "Information")
    {
        var owner = GetOwnerWindow();
        ShadcnMessageDialog.ShowInformation(message, title, owner);
    }

    /// <summary>
    /// Show a warning message
    /// </summary>
    public void ShowWarning(string message, string title = "Warning")
    {
        var owner = GetOwnerWindow();
        ShadcnMessageDialog.ShowWarning(message, title, owner);
    }

    /// <summary>
    /// Show an error message
    /// </summary>
    public void ShowError(string message, string title = "Error")
    {
        var owner = GetOwnerWindow();
        ShadcnMessageDialog.ShowError(message, title, owner);
    }

    /// <summary>
    /// Show a confirmation dialog
    /// </summary>
    public bool ShowConfirmation(string message, string title = "Confirmation")
    {
        var owner = GetOwnerWindow();
        return ShadcnMessageDialog.ShowConfirmation(message, title, owner);
    }
}
