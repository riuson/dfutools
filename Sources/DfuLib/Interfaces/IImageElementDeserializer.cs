using System.IO;

namespace DfuLib.Interfaces {
    public interface IImageElementDeserializer {
        IImageElement Read(Stream stream);
    }
}
