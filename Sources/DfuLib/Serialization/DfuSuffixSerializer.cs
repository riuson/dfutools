using DfuLib.Helpers;
using DfuLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuLib.Serialization {
    internal class DfuSuffixSerializer : IDfuSuffixSerializer {
        public uint GetSize() => 16;

        public void Write(Stream stream, IDfuSuffix dfuSuffix) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(Convert.ToUInt16(dfuSuffix.Device));
                writer.Write(Convert.ToUInt16(dfuSuffix.Product));
                writer.Write(Convert.ToUInt16(dfuSuffix.Vendor));
                writer.Write(Convert.ToUInt16(dfuSuffix.Dfu));
                writer.WriteString(dfuSuffix.DfuSignature, 3);
                writer.Write(Convert.ToByte(dfuSuffix.Length));

                var position = stream.Position;
                stream.Seek(0, SeekOrigin.Begin);

                var buffer = new byte[4096];
                var crc32 = Crc32.Init();

                while (stream.CanRead) {
                    var readed = stream.Read(buffer, 0, buffer.Length);

                    if (readed > 0) {
                        crc32 = Crc32.ComputePart(crc32, buffer, 0, Convert.ToUInt32(readed));
                    } else {
                        break;
                    }
                }

                crc32 = Crc32.Finish(crc32);
                crc32 ^= 0xffffffffu;

                stream.Seek(position, SeekOrigin.Begin);

                writer.Write(crc32);
            }
        }
    }
}
