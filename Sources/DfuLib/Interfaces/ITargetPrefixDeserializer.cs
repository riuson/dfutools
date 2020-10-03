using System.IO;

namespace DfuLib.Interfaces {
    public interface ITargetPrefixDeserializer {
        ITargetPrefix Read(Stream stream);
    }
}
