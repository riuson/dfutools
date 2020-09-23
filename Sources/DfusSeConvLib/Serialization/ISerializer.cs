using System.IO;

namespace DfuSeConvLib.Serialization {
    public interface ISerializer {
        uint Size { get; }
        void Write(Stream stream);
    }
}
