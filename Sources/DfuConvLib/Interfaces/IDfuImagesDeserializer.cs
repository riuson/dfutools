using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuImagesDeserializer {
        IDfuImages Read(Stream stream, int targets);
    }
}
