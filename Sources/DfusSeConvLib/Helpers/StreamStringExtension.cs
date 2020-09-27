using System.IO;
using System.Linq;

namespace DfuSeConvLib.Helpers {
    internal static class StreamStringExtension {
        public static string ReadString(this BinaryReader reader, int length) {
            var chars = reader.ReadChars(length);
            chars = chars.TakeWhile(x => x != '\x00').ToArray();
            var result = new string(chars, 0, chars.Length);
            return result;
        }

        public static void WriteString(this BinaryWriter writer, string value, int length) {
            var str = value.PadRight(length, '\x00');
            writer.Write(str.ToCharArray(), 0, length);
        }
    }
}
