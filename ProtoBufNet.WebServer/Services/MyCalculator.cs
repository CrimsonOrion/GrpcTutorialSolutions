using ProtoBufNet.Core;

namespace ProtoBufNet.WebServer;

public class MyCalculator : ICalculator
{
    ValueTask<MultiplyResult> ICalculator.MultiplyAsync(MultiplyRequest request)
    {
        MultiplyResult result = new() { Result = request.X * request.Y };
        return new ValueTask<MultiplyResult>(result);
    }
}