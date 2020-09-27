using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuSuffixDeserializer {
        IDfuSuffix Read(Stream stream);
    }
}
