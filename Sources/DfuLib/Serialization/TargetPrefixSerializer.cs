﻿using DfuLib.Helpers;
using DfuLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DfuLib.Serialization {
    internal class TargetPrefixSerializer : ITargetPrefixSerializer {
        public uint GetSize() => 274;

        public void Write(
            Stream stream,
            ITargetPrefix targetPrefix,
            IEnumerable<IImageElement> imageElements) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.WriteString(targetPrefix.Signature, 6);
                writer.Write(Convert.ToByte(targetPrefix.TargetId));
                writer.Write(targetPrefix.IsTargetNamed ? 1 : (uint) 0);
                writer.WriteString(targetPrefix.TargetName, 255);
                writer.Write(Convert.ToUInt32(imageElements.Sum(x => 4 + 4 + x.Data.Length)));
                writer.Write(Convert.ToUInt32(imageElements.Count()));
            }
        }
    }
}
