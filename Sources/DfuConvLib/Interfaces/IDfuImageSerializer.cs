using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuImageSerializer {
        uint GetSize(IDfuImage dfuImage);
        void Write(Stream stream, IDfuImage dfuImage);
    }
}
