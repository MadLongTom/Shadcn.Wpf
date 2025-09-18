using System;

namespace Shadcn.Wpf.Models
{
    /// <summary>
    /// Event arguments for date selection in the calendar
    /// </summary>
    public class DateSelectedEventArgs : EventArgs
    {
        public DateTime? SelectedDate { get; }

        public DateSelectedEventArgs(DateTime? selectedDate)
        {
            SelectedDate = selectedDate;
        }
    }
}
