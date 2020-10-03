using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuImageSerializer {
        uint GetSize(IDfuImage dfuImage);
        void Write(Stream stream, IDfuImage dfuImage);
    }
}
