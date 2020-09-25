using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuImagesSerializer {
        uint GetSize(IDfuImages dfuImages);
        void Write(Stream stream, IDfuImages dfuImages);
    }
}
