using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuSuffixSerializer {
        uint GetSize();
        void Write(Stream stream, IDfuSuffix dfuSuffix);
    }
}
