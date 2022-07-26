using ProtoBuf.Grpc.Configuration;

namespace ProtoBufNet.Core;

[Service]
public interface ICounter
{
    ValueTask<IncrementResult> IncrementAsync(IncrementRequest request);
}
