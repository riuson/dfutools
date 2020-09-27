using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuImageDeserializer {
        IDfuImage Read(Stream stream);
    }
}
