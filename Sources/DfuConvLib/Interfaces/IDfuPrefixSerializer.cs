using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuPrefixSerializer {
        uint GetSize();

        void Write(
            Stream stream,
            IDfuPrefix dfuPrefix,
            IDfuImages dfuImages);
    }
}
