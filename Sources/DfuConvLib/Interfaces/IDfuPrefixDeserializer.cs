using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuPrefixDeserializer {
        IDfuPrefix Read(Stream stream);
    }
}
