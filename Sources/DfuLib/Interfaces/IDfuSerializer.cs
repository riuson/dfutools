using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuSerializer {
        uint GetSize(IDfu dfu);
        void Write(Stream stream, IDfu dfu);
    }
}
