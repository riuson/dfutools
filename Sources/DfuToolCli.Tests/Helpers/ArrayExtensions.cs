using DfuLib.Helpers;
using System;

namespace DfuToolCli.Tests.Helpers {
    internal static class ArrayExtensions {
        public static void UpdateChecksum(this byte[] array) {
            var crc32 = Crc32.ComputeAll(array, 0, Convert.ToUInt32(array.Length - 4));
            Array.Copy(
                BitConverter.GetBytes(crc32 ^ 0xffffffffu),
                0,
                array,
                array.Length - 4,
                4);
        }

        public static void WriteInteger(this byte[] array, int index, uint value) {
            var bytes = BitConverter.GetBytes(value);
            Array.Copy(
                BitConverter.GetBytes(value),
                0,
                array,
                index,
                bytes.Length);
        }

        public static void WriteInteger(this byte[] array, int index, ushort value) {
            var bytes = BitConverter.GetBytes(value);
            Array.Copy(
                BitConverter.GetBytes(value),
                0,
                array,
                index,
                bytes.Length);
        }
    }
}
