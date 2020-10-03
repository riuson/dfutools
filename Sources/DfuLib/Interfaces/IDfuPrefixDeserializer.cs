using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuPrefixDeserializer {
        IDfuPrefix Read(Stream stream);
    }
}
