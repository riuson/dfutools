using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuSuffixSerializer {
        uint GetSize();
        void Write(Stream stream, IDfuSuffix dfuSuffix);
    }
}
