using System.IO;

namespace DfuLib.Interfaces {
    public interface IImageElementSerializer {
        uint GetSize(IImageElement imageElement);
        void Write(Stream stream, IImageElement imageElement);
    }
}
