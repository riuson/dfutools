using DfuSeConvLib.Interfaces;
using System;

namespace DfuSeConvLib.Parts {
    public class ImageElement : IImageElement {
        public uint ElementAddress { get; set; }
        public uint ElementSize => Convert.ToUInt32(this.Data.Length);
        public byte[] Data { get; set; }
    }
}
