using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shadcn.Wpf.Models;

namespace Shadcn.Wpf.Controls;

/// <summary>
/// Custom calendar control for ShadcnDatePicker
/// </summary>
public class ShadcnCalendar : Control
{
    static ShadcnCalendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadcnCalendar), 
            new FrameworkPropertyMetadata(typeof(ShadcnCalendar)));
    }

    public ShadcnCalendar()
    {
        DisplayDate = DateTime.Today;
        GenerateCalendarDays();
    }

    #region Dependency Properties

    /// <summary>
    /// Gets or sets the currently displayed month/year
    /// </summary>
    public static readonly DependencyProperty DisplayDateProperty =
        DependencyProperty.Register(nameof(DisplayDate), typeof(DateTime), typeof(ShadcnCalendar),
            new PropertyMetadata(DateTime.Today, OnDisplayDateChanged));

    public DateTime DisplayDate
    {
        get => (DateTime)GetValue(DisplayDateProperty);
        set => SetValue(DisplayDateProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected date
    /// </summary>
    public static readonly DependencyProperty SelectedDateProperty =
        DependencyProperty.Register(nameof(SelectedDate), typeof(DateTime?), typeof(ShadcnCalendar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedDateChanged));

    public DateTime? SelectedDate
    {
        get => (DateTime?)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show week numbers
    /// </summary>
    public static readonly DependencyProperty ShowWeekNumbersProperty =
        DependencyProperty.Register(nameof(ShowWeekNumbers), typeof(bool), typeof(ShadcnCalendar),
            new PropertyMetadata(false, OnShowWeekNumbersChanged));

    public bool ShowWeekNumbers
    {
        get => (bool)GetValue(ShowWeekNumbersProperty);
        set => SetValue(ShowWeekNumbersProperty, value);
    }

    /// <summary>
    /// Gets or sets the calendar days
    /// </summary>
    public static readonly DependencyProperty CalendarDaysProperty =
        DependencyProperty.Register(nameof(CalendarDays), typeof(List<CalendarDay>), typeof(ShadcnCalendar),
            new PropertyMetadata(new List<CalendarDay>()));

    public List<CalendarDay> CalendarDays
    {
        get => (List<CalendarDay>)GetValue(CalendarDaysProperty);
        set => SetValue(CalendarDaysProperty, value);
    }

    /// <summary>
    /// Gets or sets the display month text
    /// </summary>
    public static readonly DependencyProperty DisplayMonthProperty =
        DependencyProperty.Register(nameof(DisplayMonth), typeof(string), typeof(ShadcnCalendar),
            new PropertyMetadata(string.Empty));

    public string DisplayMonth
    {
        get => (string)GetValue(DisplayMonthProperty);
        set => SetValue(DisplayMonthProperty, value);
    }

    #endregion

    #region Events

    /// <summary>
    /// Event raised when a date is selected
    /// </summary>
    public event EventHandler<DateSelectedEventArgs>? DateSelected;

    /// <summary>
    /// Event raised when the calendar should be closed
    /// </summary>
    public event EventHandler? CloseRequested;

    #endregion

    #region Commands

    private ICommand? _previousMonthCommand;
    public ICommand PreviousMonthCommand => _previousMonthCommand ??= new RelayCommand(PreviousMonth);

    private ICommand? _nextMonthCommand;
    public ICommand NextMonthCommand => _nextMonthCommand ??= new RelayCommand(NextMonth);

    private ICommand? _selectDateCommand;
    public ICommand SelectDateCommand => _selectDateCommand ??= new RelayCommand<CalendarDay>(SelectDate);

    private ICommand? _todayCommand;
    public ICommand TodayCommand => _todayCommand ??= new RelayCommand(SelectToday);

    #endregion

    #region Private Methods

    private static void OnDisplayDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnCalendar calendar)
        {
            calendar.UpdateDisplayMonth();
            calendar.GenerateCalendarDays();
        }
    }

    private static void OnSelectedDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnCalendar calendar)
        {
            calendar.GenerateCalendarDays();
        }
    }

    private static void OnShowWeekNumbersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShadcnCalendar calendar)
        {
            calendar.GenerateCalendarDays();
        }
    }

    private void UpdateDisplayMonth()
    {
        DisplayMonth = DisplayDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture);
    }

    private void GenerateCalendarDays()
    {
        // Update the display month text
        UpdateDisplayMonth();
        
        var days = new List<CalendarDay>();
        var firstDayOfMonth = new DateTime(DisplayDate.Year, DisplayDate.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        
        // Get the first day of the week to display (might be from previous month)
        var startDate = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek);
        
        // Generate 6 weeks (42 days) to ensure we cover the entire month
        for (int i = 0; i < 42; i++)
        {
            var currentDate = startDate.AddDays(i);
            var isCurrentMonth = currentDate.Month == DisplayDate.Month;
            var isToday = currentDate.Date == DateTime.Today;
            var isSelected = SelectedDate?.Date == currentDate.Date;
            var isWeekend = currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday;
            
            var weekNumber = ShowWeekNumbers ? GetWeekOfYear(currentDate) : 0;

            days.Add(new CalendarDay
            {
                Date = currentDate,
                Day = currentDate.Day,
                IsCurrentMonth = isCurrentMonth,
                IsToday = isToday,
                IsSelected = isSelected,
                IsWeekend = isWeekend,
                WeekNumber = weekNumber
            });
        }

        CalendarDays = days;
    }

    private int GetWeekOfYear(DateTime date)
    {
        var culture = CultureInfo.CurrentCulture;
        return culture.Calendar.GetWeekOfYear(date, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
    }

    private void PreviousMonth()
    {
        DisplayDate = DisplayDate.AddMonths(-1);
    }

    private void NextMonth()
    {
        DisplayDate = DisplayDate.AddMonths(1);
    }

    private void SelectDate(CalendarDay? day)
    {
        if (day == null) return;

        SelectedDate = day.Date;
        DateSelected?.Invoke(this, new DateSelectedEventArgs(day.Date));
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    private void SelectToday()
    {
        var today = DateTime.Today;
        DisplayDate = today;
        SelectedDate = today;
        DateSelected?.Invoke(this, new DateSelectedEventArgs(today));
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}

/// <summary>
/// Represents a day in the calendar
/// </summary>
public class CalendarDay : ObservableObject
{
    private DateTime _date;
    private int _day;
    private bool _isCurrentMonth;
    private bool _isToday;
    private bool _isSelected;
    private bool _isWeekend;
    private int _weekNumber;

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public int Day
    {
        get => _day;
        set => SetProperty(ref _day, value);
    }

    public bool IsCurrentMonth
    {
        get => _isCurrentMonth;
        set => SetProperty(ref _isCurrentMonth, value);
    }

    public bool IsToday
    {
        get => _isToday;
        set => SetProperty(ref _isToday, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public bool IsWeekend
    {
        get => _isWeekend;
        set => SetProperty(ref _isWeekend, value);
    }

    public int WeekNumber
    {
        get => _weekNumber;
        set => SetProperty(ref _weekNumber, value);
    }
}

/// <summary>
/// Event arguments for date selection
/// </summary>
public class DateSelectedEventArgs : EventArgs
{
    public DateTime SelectedDate { get; }

    public DateSelectedEventArgs(DateTime selectedDate)
    {
        SelectedDate = selectedDate;
    }
}
