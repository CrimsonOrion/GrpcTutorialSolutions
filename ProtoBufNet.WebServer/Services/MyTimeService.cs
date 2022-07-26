
using ProtoBuf.Grpc;

using ProtoBufNet.Core;

namespace ProtoBufNet.WebServer;

public class MyTimeService : ITimeService
{
    public IAsyncEnumerable<TimeResult> SubscribeAsync(CallContext context = default) => SubscribeAsyncImpl(context.CancellationToken);

    private static async IAsyncEnumerable<TimeResult> SubscribeAsyncImpl(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            yield return new TimeResult { Time = DateTime.UtcNow, Id = Guid.NewGuid() };
        }
    }
}
