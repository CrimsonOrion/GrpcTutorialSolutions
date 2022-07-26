using Grpc.Core;
using Grpc.Net.Client;

using ProtoBuf.Grpc.Client;

using ProtoBufNet.Core;

using System.Threading;

namespace ProtoBufNet.Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        GrpcClientFactory.AllowUnencryptedHttp2 = true;
        using var http = GrpcChannel.ForAddress("http://localhost:10042");
        var calculator = http.CreateGrpcService<ICalculator>();
        var result = await calculator.MultiplyAsync(new() { X = 12, Y = 4 });
        Console.WriteLine(result.Result);

        var clock = http.CreateGrpcService<ITimeService>();
        var counter = http.CreateGrpcService<ICounter>();
        using var cancel = new CancellationTokenSource(TimeSpan.FromMinutes(1));
        CallOptions options = new(cancellationToken: cancel.Token);

        try
        {
            await foreach (var time in clock.SubscribeAsync(new(options)))
            {
                Console.WriteLine($"Time time is now: {time.Time}");
                var currentInc = await counter.IncrementAsync(new() { Inc = 1 });
                Console.WriteLine($"Time received {currentInc.Result} times");
            }
        }
        catch (RpcException ex) { Console.WriteLine(ex); }
        catch (OperationCanceledException) { }

        Console.WriteLine("Press [Enter] to exit");
        Console.ReadLine();
    }
}
