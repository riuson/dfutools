using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuImageDeserializer {
        IDfuImage Read(Stream stream);
    }
}
