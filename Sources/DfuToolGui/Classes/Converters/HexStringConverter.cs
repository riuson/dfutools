using System;
using System.Globalization;
using System.Windows.Data;

namespace DfuToolGui.Classes.Converters {
    public class HexStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var digits = System.Convert.ToInt32(parameter);
            var format = $"x{digits}";

            switch (value) {
                case int v: {
                    return v.ToString(format);
                }
                case uint v: {
                    return v.ToString(format);
                }
                case byte v: {
                    return v.ToString(format);
                }
                default: {
                    throw new ArgumentException("Unknown type of value.", nameof(value));
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string str) {
                if (targetType == typeof(int)) {
                    return int.Parse(str, NumberStyles.HexNumber);
                }

                if (targetType == typeof(uint)) {
                    return uint.Parse(str, NumberStyles.HexNumber);
                }

                if (targetType == typeof(byte)) {
                    return byte.Parse(str, NumberStyles.HexNumber);
                }

                throw new ArgumentException("Unknown targetType.");
            }

            throw new ArgumentException("Unknown type of value.", nameof(value));
        }
    }
}
