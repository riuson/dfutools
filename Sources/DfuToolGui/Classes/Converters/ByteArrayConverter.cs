using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DfuToolGui.Classes.Converters {
    public class ByteArrayConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return "[Empty]";
            }

            if (!(value is byte[] bytes)) {
                return "[Invalid Data Type]";
            }

            var hexCodes = bytes
                .Take(1024)
                .Select(x => x.ToString("X2"));

            return string.Join(" ", hexCodes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
