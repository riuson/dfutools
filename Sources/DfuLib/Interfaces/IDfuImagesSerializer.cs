using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuImagesSerializer {
        uint GetSize(IDfuImages dfuImages);
        void Write(Stream stream, IDfuImages dfuImages);
    }
}
