using System.IO;

namespace DfuConvLib.Interfaces {
    public interface IDfuDeserializer {
        IDfu Read(Stream stream);
    }
}
