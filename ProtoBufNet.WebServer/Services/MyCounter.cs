
using Microsoft.AspNetCore.Authorization;

using ProtoBufNet.Core;

namespace ProtoBufNet.WebServer;

[Authorize]
public class MyCounter : ICounter
{
    private int _counter = 0;
    private readonly object counterLock = new();

    ValueTask<IncrementResult> ICounter.IncrementAsync(IncrementRequest request)
    {
        lock (counterLock)
        {
            _counter += request.Inc;
            IncrementResult result = new() { Result = _counter };
            return new ValueTask<IncrementResult>(result);
        }
    }
}