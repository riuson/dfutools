using DfuSeConvLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DfuSeConvLib.Serialization {
    internal class TargetPrefixSerializer : ITargetPrefixSerializer {
        public uint GetSize() => 274;

        public void Write(
            Stream stream,
            ITargetPrefix targetPrefix,
            IEnumerable<IImageElement> imageElements) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(targetPrefix.Signature.PadRight(6, '\x0000').ToCharArray(), 0, 6);
                writer.Write(Convert.ToByte(targetPrefix.AlternateSetting));
                writer.Write(targetPrefix.TargetNamed ? 1 : (uint) 0);
                writer.Write(targetPrefix.TargetName.PadRight(255, '\x0000').ToCharArray(), 0, 255);
                writer.Write(Convert.ToUInt32((int) imageElements.Sum(x => 4 + 4 + x.ElementSize)));
                writer.Write(Convert.ToUInt32(imageElements.Count()));
            }
        }
    }
}
