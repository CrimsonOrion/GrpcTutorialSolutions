using ProtoBuf;

using System.Runtime.Serialization;

namespace ProtoBufNet.Core;

[ProtoContract]
public class MultiplyRequest
{
    [ProtoMember(1)]
    public int X { get; set; }

    [ProtoMember(2)]
    public int Y { get; set; }
}
