using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace ProtoBufNet.Core;

[Service]
public interface ITimeService
{
    [Operation]
    IAsyncEnumerable<TimeResult> SubscribeAsync(CallContext context = default);
}
