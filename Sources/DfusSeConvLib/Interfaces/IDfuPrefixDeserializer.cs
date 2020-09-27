using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuPrefixDeserializer {
        IDfuPrefix Read(Stream stream);
    }
}
