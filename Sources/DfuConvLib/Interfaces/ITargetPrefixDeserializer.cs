using System.IO;

namespace DfuConvLib.Interfaces {
    public interface ITargetPrefixDeserializer {
        ITargetPrefix Read(Stream stream);
    }
}
