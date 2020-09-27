using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IImageElementSerializer {
        uint GetSize(IImageElement imageElement);
        void Write(Stream stream, IImageElement imageElement);
    }
}
