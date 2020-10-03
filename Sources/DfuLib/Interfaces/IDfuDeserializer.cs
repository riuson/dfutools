using System.IO;

namespace DfuLib.Interfaces {
    public interface IDfuDeserializer {
        IDfu Read(Stream stream);
    }
}
