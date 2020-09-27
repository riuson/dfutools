using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuSerializer {
        uint GetSize(IDfu dfu);
        void Write(Stream stream, IDfu dfu);
    }
}
