using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuImagesSerializer {
        uint GetSize(IDfuImages dfuImages);
        void Write(Stream stream, IDfuImages dfuImages);
    }
}
