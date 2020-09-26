using DfuSeConvLib.Interfaces;

namespace DfuSeConvLib.Parts {
    internal class ImageElement : IImageElement {
        public uint ElementAddress { get; set; }
        public uint ElementSize { get; set; }
        public byte[] Data { get; set; }
    }
}
