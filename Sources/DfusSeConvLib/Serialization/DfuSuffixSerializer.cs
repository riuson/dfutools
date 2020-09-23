using DfuSeConvLib.Helpers;
using DfuSeConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Serialization {
    internal class DfuSuffixSerializer : ISerializer {
        private readonly IDfuSuffix _dfuSuffix;

        public DfuSuffixSerializer(IDfuSuffix dfuSuffix) => this._dfuSuffix = dfuSuffix;

        public uint Size => 16;

        public void Write(Stream stream) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(Convert.ToUInt16(this._dfuSuffix.Device));
                writer.Write(Convert.ToUInt16(this._dfuSuffix.Product));
                writer.Write(Convert.ToUInt16(this._dfuSuffix.Vendor));
                writer.Write(Convert.ToUInt16(this._dfuSuffix.Dfu));
                writer.Write(this._dfuSuffix.DfuSignature.PadRight(3).ToCharArray(), 0, 3);
                writer.Write(Convert.ToByte(this._dfuSuffix.Length));

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
