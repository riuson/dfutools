using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuSuffixSerializer {
        uint GetSize();
        void Write(Stream stream, IDfuSuffix dfuSuffix);
    }
}
