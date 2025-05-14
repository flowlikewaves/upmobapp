using System.Globalization;

namespace Mobappg4v2.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                return status.ToLower() switch
                {
                    "pending" => Color.FromArgb("#FF9500"),    // Orange
                    "processing" => Color.FromArgb("#5856D6"),  // Purple
                    "shipped" => Color.FromArgb("#007AFF"),     // Blue
                    "delivered" => Color.FromArgb("#34C759"),   // Green
                    "cancelled" => Color.FromArgb("#FF3B30"),   // Red
                    _ => Color.FromArgb("#8E8E93")             // Gray (default)
                };
            }
            return Color.FromArgb("#8E8E93");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 