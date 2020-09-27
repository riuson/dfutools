using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuSuffixDeserializer {
        IDfuSuffix Read(Stream stream);
    }
}
