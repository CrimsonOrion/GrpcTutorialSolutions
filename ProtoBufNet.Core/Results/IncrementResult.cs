using ProtoBuf;

namespace ProtoBufNet.Core;

[ProtoContract]
public class IncrementResult
{
    [ProtoMember(1)]
    public int Result { get; set; }
}