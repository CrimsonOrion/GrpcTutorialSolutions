using Microsoft.Extensions.DependencyInjection.Extensions;

using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;

using ProtoBufNet.Core;

namespace ProtoBufNet.WebServer;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal; });
        services.TryAddSingleton(BinderConfiguration.Create(binder: new ServiceBinderWithServiceResolutionFromServiceCollection(services)));
        services.AddCodeFirstGrpcReflection();

        services.AddAuthentication(FakeAuthHandler.SchemeName).AddScheme<FakeAuthOptions, FakeAuthHandler>(FakeAuthHandler.SchemeName, options => options.AlwaysAuthenticate = true);
        services.AddAuthorization();
        services.AddSingleton<ICounter, MyCounter>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment nothing)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<ICounter>();
            endpoints.MapGrpcService<MyCalculator>();
            endpoints.MapGrpcService<MyTimeService>();
            endpoints.MapCodeFirstGrpcReflectionService();
        });
    }
}
