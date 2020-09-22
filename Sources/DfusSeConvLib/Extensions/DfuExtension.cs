using DfuSeConvLib.Interfaces;
using System.IO;

namespace DfuSeConvLib.Extensions {
    public static class DfuExtension {
        public static uint CalculateSize(this IDfu dfu) =>
            dfu.Prefix.CalculateSize() + dfu.Images.CalculateSize() + dfu.Suffix.CalculateSize();

        public static void Write(this IDfu dfu, Stream stream) {
            dfu.Prefix.Write(stream, dfu.Images);
            dfu.Images.Write(stream);
            dfu.Suffix.Write(stream);
        }
    }
}
