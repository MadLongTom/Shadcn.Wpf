using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.Presentation.ViewModels;

public partial class ToggleSwitchDemoPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    public ToggleSwitchDemoPageViewModel(IMessageService messageService) 
        : base("Toggle Switch Demo", "A control that allows the user to toggle between checked and not checked.")
    {
        _messageService = messageService;
        UpdateTexts();
    }

    // Basic Usage
    [ObservableProperty]
    private bool _isAirplaneModeEnabled;

    [ObservableProperty]
    private bool _isWiFiEnabled = true;

    [ObservableProperty]
    private string _basicStatusText = string.Empty;

    // With Description
    [ObservableProperty]
    private bool _isMarketingEmailsEnabled;

    [ObservableProperty]
    private bool _isSecurityEmailsEnabled = true;

    // With Custom Text
    [ObservableProperty]
    private bool _isDarkModeEnabled;

    [ObservableProperty]
    private bool _isAutoSaveEnabled = true;

    // States
    [ObservableProperty]
    private bool _hasErrorToggle;

    // Data Binding Example
    [ObservableProperty]
    private bool _isNotificationsEnabled = true;

    [ObservableProperty]
    private bool _isSoundEnabled = true;

    [ObservableProperty]
    private bool _isVibrateEnabled;

    [ObservableProperty]
    private string _notificationSettingsText = string.Empty;

    // Settings Form Example
    [ObservableProperty]
    private bool _isTwoFactorEnabled;

    [ObservableProperty]
    private bool _isEmailNotificationsEnabled = true;

    [ObservableProperty]
    private bool _isPushNotificationsEnabled = true;

    [ObservableProperty]
    private bool _isAnalyticsEnabled;

    partial void OnIsAirplaneModeEnabledChanged(bool value)
    {
        UpdateTexts();
    }

    partial void OnIsWiFiEnabledChanged(bool value)
    {
        UpdateTexts();
    }

    partial void OnIsNotificationsEnabledChanged(bool value)
    {
        if (!value)
        {
            IsSoundEnabled = false;
            IsVibrateEnabled = false;
        }
        UpdateNotificationSettingsText();
    }

    partial void OnIsSoundEnabledChanged(bool value)
    {
        UpdateNotificationSettingsText();
    }

    partial void OnIsVibrateEnabledChanged(bool value)
    {
        UpdateNotificationSettingsText();
    }

    [RelayCommand]
    private void SaveSettings()
    {
        var settings = new List<string>();
        
        if (IsTwoFactorEnabled) settings.Add("Two-factor authentication");
        if (IsEmailNotificationsEnabled) settings.Add("Email notifications");
        if (IsPushNotificationsEnabled) settings.Add("Push notifications");
        if (IsAnalyticsEnabled) settings.Add("Analytics");

        var message = settings.Count > 0 
            ? $"Settings saved successfully!\n\nEnabled features:\n• {string.Join("\n• ", settings)}"
            : "Settings saved successfully!\n\nNo features are currently enabled.";

        _messageService.ShowInformation(message, "Settings Saved");
    }

    [RelayCommand]
    private void ResetSettings()
    {
        IsTwoFactorEnabled = false;
        IsEmailNotificationsEnabled = true;
        IsPushNotificationsEnabled = true;
        IsAnalyticsEnabled = false;

        _messageService.ShowInformation("All settings have been reset to their default values.", "Settings Reset");
    }

    private void UpdateTexts()
    {
        var statuses = new List<string>();
        
        if (IsAirplaneModeEnabled)
            statuses.Add("Airplane mode: ON");
        else
            statuses.Add("Airplane mode: OFF");

        if (IsWiFiEnabled)
            statuses.Add("Wi-Fi: ON");
        else
            statuses.Add("Wi-Fi: OFF");

        BasicStatusText = string.Join(" | ", statuses);
    }

    private void UpdateNotificationSettingsText()
    {
        var settings = new List<string>();

        if (IsNotificationsEnabled)
        {
            settings.Add("Notifications: Enabled");
            
            if (IsSoundEnabled)
                settings.Add("Sound: On");
            else
                settings.Add("Sound: Off");

            if (IsVibrateEnabled)
                settings.Add("Vibrate: On");
            else
                settings.Add("Vibrate: Off");
        }
        else
        {
            settings.Add("Notifications: Disabled");
            settings.Add("Sound: Disabled");
            settings.Add("Vibrate: Disabled");
        }

        NotificationSettingsText = string.Join("\n", settings);
    }
}
