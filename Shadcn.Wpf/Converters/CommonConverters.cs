using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Shadcn.Wpf.Converters;

public class NullToVisibilityConverter : IValueConverter
{
    public static readonly NullToVisibilityConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return Visibility.Collapsed;

        if (value is string str && string.IsNullOrEmpty(str))
            return Visibility.Collapsed;

        return Visibility.Visible;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class BooleanToVisibilityConverter : IValueConverter
{
    public static readonly BooleanToVisibilityConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible;
        }
        return false;
    }
}

public class InverseBooleanConverter : IValueConverter
{
    public static readonly InverseBooleanConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return true;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return false;
    }
}

public class BooleanToBrushConverter : IValueConverter
{
    public static readonly BooleanToBrushConverter Instance = new();
    
    public Brush TrueBrush { get; set; } = Brushes.Green;
    public Brush FalseBrush { get; set; } = Brushes.Red;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? TrueBrush : FalseBrush;
        }
        return FalseBrush;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class BooleanToDoubleConverter : IValueConverter
{
    public static readonly BooleanToDoubleConverter Instance = new();
    
    public double TrueValue { get; set; } = 1.0;
    public double FalseValue { get; set; } = 0.0;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            // Check if we need to invert the result
            if (parameter?.ToString()?.ToLower() == "inverse")
            {
                return boolValue ? FalseValue : TrueValue;
            }
            
            // Check if parameter is a number for rotation
            if (parameter != null && double.TryParse(parameter.ToString(), out double paramValue))
            {
                return boolValue ? paramValue : 0.0;
            }
            
            return boolValue ? TrueValue : FalseValue;
        }
        return FalseValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return Math.Abs(doubleValue - TrueValue) < Math.Abs(doubleValue - FalseValue);
        }
        return false;
    }
}

public class AnimationWidthConverter : IValueConverter
{
    public static readonly AnimationWidthConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isCollapsed)
        {
            if (parameter is string paramStr && double.TryParse(paramStr, out double paramValue))
            {
                return isCollapsed ? 60.0 : paramValue; // Default collapsed width is 60
            }
            return isCollapsed ? 60.0 : 250.0; // Default values
        }
        return 250.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ScaleConverter : IValueConverter
{
    public static readonly ScaleConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? 0.95 : 1.0; // Scale down when pressed
        }
        return 1.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StringLengthConverter : IValueConverter
{
    public static readonly StringLengthConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            return str.Length;
        }
        return 0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class EmailValidationConverter : IValueConverter
{
    public static readonly EmailValidationConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isValid)
        {
            return isValid ? "Email format is valid" : "Please enter a valid email address";
        }
        return "Email validation status unknown";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StringNullOrEmptyConverter : IValueConverter
{
    public static readonly StringNullOrEmptyConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            return !string.IsNullOrEmpty(str);
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IsNotNullConverter : IValueConverter
{
    public static readonly IsNotNullConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value != null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IsNotNaNConverter : IValueConverter
{
    public static readonly IsNotNaNConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return !double.IsNaN(doubleValue);
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ProgressWidthConverter : IMultiValueConverter
{
    public static readonly ProgressWidthConverter Instance = new();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length >= 4 &&
            values[0] is double value &&
            values[1] is double minimum &&
            values[2] is double maximum &&
            values[3] is double trackWidth)
        {
            if (maximum == minimum) return 0.0;
            
            var percentage = (value - minimum) / (maximum - minimum);
            percentage = Math.Max(0, Math.Min(1, percentage)); // Clamp between 0 and 1
            
            var result = trackWidth * percentage;
            
            // Debug output
            System.Diagnostics.Debug.WriteLine($"ProgressWidthConverter: Value={value}, Min={minimum}, Max={maximum}, TrackWidth={trackWidth}, Result={result}");
            
            return result;
        }
        
        System.Diagnostics.Debug.WriteLine($"ProgressWidthConverter: Invalid values - Length={values?.Length}");
        return 0.0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StringToVisibilityConverter : IValueConverter
{
    public static readonly StringToVisibilityConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str && !string.IsNullOrEmpty(str))
        {
            return Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Common converters for Shadcn.Wpf
/// </summary>
public static class CommonConverters
{
    public static readonly NullToVisibilityConverter NullToVisibilityConverter = NullToVisibilityConverter.Instance;
    public static readonly BooleanToVisibilityConverter BooleanToVisibilityConverter = BooleanToVisibilityConverter.Instance;
    public static readonly StringToVisibilityConverter StringToVisibilityConverter = StringToVisibilityConverter.Instance;
    public static readonly InverseBooleanConverter InverseBooleanConverter = InverseBooleanConverter.Instance;
    public static readonly BooleanToBrushConverter BooleanToBrushConverter = BooleanToBrushConverter.Instance;
    public static readonly StringLengthConverter StringLengthConverter = StringLengthConverter.Instance;
    public static readonly EmailValidationConverter EmailValidationConverter = EmailValidationConverter.Instance;
    public static readonly StringNullOrEmptyConverter StringNullOrEmptyConverter = StringNullOrEmptyConverter.Instance;
    public static readonly IsNotNullConverter IsNotNullConverter = IsNotNullConverter.Instance;
    public static readonly IsNotNaNConverter IsNotNaNConverter = IsNotNaNConverter.Instance;
    public static readonly ProgressWidthConverter ProgressWidthConverter = ProgressWidthConverter.Instance;
}
