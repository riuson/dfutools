using System.IO;

namespace DfuSeConvLib.Interfaces {
    public interface IDfuDeserializer {
        IDfu Read(Stream stream);
    }
}
