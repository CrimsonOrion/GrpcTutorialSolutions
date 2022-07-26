using Microsoft.AspNetCore;

namespace ProtoBufNet.WebServer;
public class Program
{
    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IWebHostBuilder CreateHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureKestrel(options =>
            {
                options.ListenLocalhost(10042, listenOptions =>
                {
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                });
            })
            .UseStartup<Startup>();
}
