﻿using DfuSeConvLib.Parts;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Extensions {
    public static class DfuPrefixExtension {
        public static uint CalculateSize(this DfuPrefix dfuPrefix) => 16;

        public static void Write(this DfuPrefix dfuPrefix, Stream stream, DfuImages dfuImages) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(dfuPrefix.Signature.PadRight(5, '\x0000').ToCharArray(), 0, 5);
                writer.Write(Convert.ToByte(dfuPrefix.Version));

                uint totalLength = 11;

                foreach (var image in dfuImages.Images) {
                    totalLength += image.CalculateSize();
                }

                writer.Write(totalLength);
                writer.Write(Convert.ToByte(dfuImages.Images.Count));
            }
        }
    }
}
