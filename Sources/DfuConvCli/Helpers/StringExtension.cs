using System;
using System.Globalization;

namespace DfuConvCli.Helpers {
    public static class StringExtension {
        public static int ToInt32(this string str, int min = int.MinValue, int max = int.MaxValue) {
            if (str.ToLower().StartsWith("0x")) {
                if (int.TryParse(str.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture,
                    out var result)) {
                    if (result >= min && result <= max) {
                        return result;
                    }
                }
            } else {
                if (int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
                    if (result >= min && result <= max) {
                        return result;
                    }
                }
            }

            throw new ArgumentException(
                $"Integer number not recognized in '{str}' or value out of range {min}...{max}.");
        }

        public static uint ToUInt32(this string str, uint min = uint.MinValue, uint max = uint.MaxValue) {
            if (str.ToLower().StartsWith("0x")) {
                if (uint.TryParse(str.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture,
                    out var result)) {
                    if (result >= min && result <= max) {
                        return result;
                    }
                }
            } else {
                if (uint.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
                    if (result >= min && result <= max) {
                        return result;
                    }
                }
            }

            throw new ArgumentException(
                $"Integer number not recognized in '{str}' or value out of range {min}...{max}.");
        }
    }
}
