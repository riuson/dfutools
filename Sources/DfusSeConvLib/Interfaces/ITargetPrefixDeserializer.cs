using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface ITargetPrefixDeserializer {
        ITargetPrefix Read(Stream stream);
    }
}
