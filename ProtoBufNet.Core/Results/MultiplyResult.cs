using ProtoBuf;

namespace ProtoBufNet.Core;

[ProtoContract]
public class MultiplyResult
{
    [ProtoMember(1)]
    public int Result { get; set; }
}