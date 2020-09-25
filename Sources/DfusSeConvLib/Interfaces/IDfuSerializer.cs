using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuSerializer {
        uint GetSize(IDfu dfu);
        void Write(Stream stream, IDfu dfu);
    }
}
