using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IImageElementDeserializer {
        IImageElement Read(Stream stream);
    }
}
