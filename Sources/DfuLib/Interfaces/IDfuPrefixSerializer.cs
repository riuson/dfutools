using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuPrefixSerializer {
        uint GetSize();

        void Write(
            Stream stream,
            IDfuPrefix dfuPrefix,
            IDfuImages dfuImages);
    }
}
