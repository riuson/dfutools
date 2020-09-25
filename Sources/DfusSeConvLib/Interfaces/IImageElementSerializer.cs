using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IImageElementSerializer {
        uint GetSize(IImageElement imageElement);
        void Write(Stream stream, IImageElement imageElement);
    }
}
