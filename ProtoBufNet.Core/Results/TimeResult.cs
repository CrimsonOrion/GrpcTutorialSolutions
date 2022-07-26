using ProtoBuf;

namespace ProtoBufNet.Core;

[ProtoContract]
public class TimeResult
{
    [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
    public DateTime Time { get; set; }
    [ProtoMember(2)]
    public Guid Id { get; set; }
}
