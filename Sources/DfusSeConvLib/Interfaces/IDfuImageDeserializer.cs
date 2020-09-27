using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuImageDeserializer {
        IDfuImage Read(Stream stream);
    }
}
