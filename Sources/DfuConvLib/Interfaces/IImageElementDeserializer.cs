using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IImageElementDeserializer {
        IImageElement Read(Stream stream);
    }
}
