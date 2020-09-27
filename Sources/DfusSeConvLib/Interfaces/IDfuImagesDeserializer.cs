using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuImagesDeserializer {
        IDfuImages Read(Stream stream, int targets);
    }
}
