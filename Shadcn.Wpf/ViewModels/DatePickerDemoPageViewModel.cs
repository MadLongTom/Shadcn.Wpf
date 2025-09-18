using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Services;

namespace Shadcn.Wpf.ViewModels;

/// <summary>
/// ViewModel for the DatePickerDemoPage
/// </summary>
public partial class DatePickerDemoPageViewModel : BasePageViewModel
{
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private DateTime? _selectedDate;

    [ObservableProperty]
    private DateTime? _startDate;

    [ObservableProperty]
    private DateTime? _endDate;

    [ObservableProperty]
    private DateTime? _reviewDate;

    [ObservableProperty]
    private bool _hasError;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private List<string> _recentDates = new();

    public DatePickerDemoPageViewModel(IMessageService messageService) 
        : base("DatePicker Demo", "Explore different DatePicker variants and configurations")
    {
        _messageService = messageService;
        InitializeData();
    }

    /// <summary>
    /// Command to clear a specific date picker
    /// </summary>
    [RelayCommand]
    private void ClearDate(string dateType)
    {
        switch (dateType?.ToLower())
        {
            case "selected":
                SelectedDate = null;
                break;
            case "start":
                StartDate = null;
                break;
            case "end":
                EndDate = null;
                break;
            case "review":
                ReviewDate = null;
                break;
        }
        
        _messageService.ShowInformation($"{dateType} date cleared successfully.", "Date Cleared");
    }

    /// <summary>
    /// Command to set date to today
    /// </summary>
    [RelayCommand]
    private void SetToday(string dateType)
    {
        var today = DateTime.Today;
        
        switch (dateType?.ToLower())
        {
            case "selected":
                SelectedDate = today;
                break;
            case "start":
                StartDate = today;
                break;
            case "end":
                EndDate = today;
                break;
            case "review":
                ReviewDate = today;
                break;
        }
        
        _messageService.ShowInformation($"{dateType} date set to today ({today:yyyy-MM-dd}).", "Date Set");
    }

    /// <summary>
    /// Command to validate date range
    /// </summary>
    [RelayCommand]
    private void ValidateDateRange()
    {
        HasError = false;
        ErrorMessage = string.Empty;

        if (StartDate.HasValue && EndDate.HasValue)
        {
            if (StartDate.Value > EndDate.Value)
            {
                HasError = true;
                ErrorMessage = "Start date cannot be after end date";
                _messageService.ShowError(ErrorMessage, "Validation Error");
                return;
            }

            if (ReviewDate.HasValue && (ReviewDate.Value < StartDate.Value || ReviewDate.Value > EndDate.Value))
            {
                HasError = true;
                ErrorMessage = "Review date must be between start and end dates";
                _messageService.ShowError(ErrorMessage, "Validation Error");
                return;
            }
        }

        if (EndDate.HasValue && EndDate.Value < DateTime.Today)
        {
            HasError = true;
            ErrorMessage = "End date cannot be in the past";
            _messageService.ShowError(ErrorMessage, "Validation Error");
            return;
        }

                _messageService.ShowInformation("Date validation passed!", "Validation Success");
    }

    /// <summary>
    /// Command to submit the form
    /// </summary>
    [RelayCommand]
    private void Submit()
    {
        // Validate first
        ValidateDateRange();
        
        if (!HasError)
        {
            var message = "Form submitted successfully!\n\n";
            
            if (StartDate.HasValue)
                message += $"Start Date: {StartDate.Value:yyyy-MM-dd}\n";
            if (EndDate.HasValue)
                message += $"End Date: {EndDate.Value:yyyy-MM-dd}\n";
            if (ReviewDate.HasValue)
                message += $"Review Date: {ReviewDate.Value:yyyy-MM-dd}\n";
                
            _messageService.ShowInformation(message, "Form Submitted");
            
            // Add to recent dates
            AddToRecentDates();
        }
    }

    /// <summary>
    /// Command to cancel the form
    /// </summary>
    [RelayCommand]
    private void Cancel()
    {
        StartDate = null;
        EndDate = null;
        ReviewDate = null;
        HasError = false;
        ErrorMessage = string.Empty;
        
        _messageService.ShowInformation("Form cancelled and cleared.", "Form Cancelled");
    }

    /// <summary>
    /// Command to show date picker features
    /// </summary>
    [RelayCommand]
    private void ShowFeatures()
    {
        var features = "DatePicker Features:\n\n" +
                      "• Multiple variants (Default, Outline, Ghost)\n" +
                      "• Different sizes (Small, Medium, Large)\n" +
                      "• Label and helper text support\n" +
                      "• Error state with custom messages\n" +
                      "• Clearable functionality\n" +
                      "• Calendar icon toggle\n" +
                      "• Week numbers display\n" +
                      "• Keyboard navigation\n" +
                      "• MVVM pattern support";
                      
        _messageService.ShowInformation(features, "DatePicker Features");
    }

    /// <summary>
    /// Command to reset all dates to default values
    /// </summary>
    [RelayCommand]
    private void ResetToDefaults()
    {
        SelectedDate = DateTime.Today;
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(30);
        ReviewDate = DateTime.Today.AddDays(15);
        HasError = false;
        ErrorMessage = string.Empty;
        
        _messageService.ShowInformation("All dates reset to default values.", "Reset Complete");
    }

    /// <summary>
    /// Handle date selection changes
    /// </summary>
    partial void OnSelectedDateChanged(DateTime? value)
    {
        if (value.HasValue)
        {
            // Clear any error state when a valid date is selected
            if (HasError)
            {
                HasError = false;
                ErrorMessage = string.Empty;
            }
            
            // Add to recent dates
            AddToRecentDates();
        }
    }

    partial void OnStartDateChanged(DateTime? value)
    {
        // Auto-validate when start date changes
        if (value.HasValue && EndDate.HasValue && value.Value > EndDate.Value)
        {
            HasError = true;
            ErrorMessage = "Start date cannot be after end date";
        }
        else if (HasError && ErrorMessage.Contains("Start date"))
        {
            HasError = false;
            ErrorMessage = string.Empty;
        }
    }

    partial void OnEndDateChanged(DateTime? value)
    {
        // Auto-validate when end date changes
        if (value.HasValue && StartDate.HasValue && value.Value < StartDate.Value)
        {
            HasError = true;
            ErrorMessage = "End date cannot be before start date";
        }
        else if (HasError && ErrorMessage.Contains("End date"))
        {
            HasError = false;
            ErrorMessage = string.Empty;
        }
    }

    private void InitializeData()
    {
        // Initialize with some sample dates
        SelectedDate = DateTime.Today;
        RecentDates = new List<string>
        {
            DateTime.Today.ToString("yyyy-MM-dd"),
            DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"),
            DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd"),
            DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd")
        };
    }

    private void AddToRecentDates()
    {
        var recentList = RecentDates.ToList();
        
        if (SelectedDate.HasValue)
        {
            var dateStr = SelectedDate.Value.ToString("yyyy-MM-dd");
            if (!recentList.Contains(dateStr))
            {
                recentList.Insert(0, dateStr);
                if (recentList.Count > 10) // Keep only last 10 dates
                {
                    recentList.RemoveAt(recentList.Count - 1);
                }
                RecentDates = recentList;
            }
        }
    }
}
