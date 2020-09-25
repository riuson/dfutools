using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuImageSerializer {
        uint GetSize(IDfuImage dfuImage);
        void Write(Stream stream, IDfuImage dfuImage);
    }
}
