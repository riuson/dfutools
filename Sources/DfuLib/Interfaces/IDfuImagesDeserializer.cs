using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuImagesDeserializer {
        IDfuImages Read(Stream stream, int targets);
    }
}
