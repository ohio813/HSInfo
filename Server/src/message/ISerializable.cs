
using System.IO;

namespace HSInfo {
    public interface ISerializable {
        void Serialize(BinaryWriter w);
        void Deserialize(BinaryReader r);
    }
}
