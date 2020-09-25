using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuPrefixSerializer {
        uint GetSize();

        void Write(
            Stream stream,
            IDfuPrefix dfuPrefix,
            IDfuImages dfuImages);
    }
}
