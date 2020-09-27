using DfuConvLib.Helpers;
using DfuConvLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DfuConvLib.Serialization {
    internal class TargetPrefixSerializer : ITargetPrefixSerializer {
        public uint GetSize() => 274;

        public void Write(
            Stream stream,
            ITargetPrefix targetPrefix,
            IEnumerable<IImageElement> imageElements) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.WriteString(targetPrefix.Signature, 6);
                writer.Write(Convert.ToByte(targetPrefix.AlternateSetting));
                writer.Write(targetPrefix.TargetNamed ? 1 : (uint) 0);
                writer.WriteString(targetPrefix.TargetName, 255);
                writer.Write(Convert.ToUInt32(imageElements.Sum(x => 4 + 4 + x.Data.Length)));
                writer.Write(Convert.ToUInt32(imageElements.Count()));
            }
        }
    }
}
