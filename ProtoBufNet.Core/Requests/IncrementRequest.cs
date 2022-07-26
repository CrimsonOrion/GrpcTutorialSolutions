using ProtoBuf;

namespace ProtoBufNet.Core;

[ProtoContract]
public class IncrementRequest
{
    [ProtoMember(1)]
    public int Inc { get; set; }
}
