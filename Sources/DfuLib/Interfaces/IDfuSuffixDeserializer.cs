using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuSuffixDeserializer {
        IDfuSuffix Read(Stream stream);
    }
}
