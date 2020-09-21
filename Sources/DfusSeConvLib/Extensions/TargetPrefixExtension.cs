using DfuSeConvLib.Parts;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DfuSeConvLib.Extensions {
    public static class TargetPrefixExtension {
        public static uint CalculateSize(this TargetPrefix prefix) => 274;

        public static void Write(this TargetPrefix targetPrefix, Stream stream, DfuImage dfuImage) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(targetPrefix.Signature.PadRight(6, '\x0000').ToCharArray(), 0, 6);
                writer.Write(Convert.ToByte(targetPrefix.AlternateSetting));
                writer.Write(targetPrefix.TargetNamed ? 1 : (uint) 0);
                writer.Write(targetPrefix.TargetName.PadRight(255, '\x0000').ToCharArray(), 0, 255);
                writer.Write(Convert.ToUInt32(dfuImage.ImageElements.Sum(x => x.CalculateSize())));
                writer.Write(Convert.ToUInt32(dfuImage.ImageElements.Count));
            }
        }
    }
}
