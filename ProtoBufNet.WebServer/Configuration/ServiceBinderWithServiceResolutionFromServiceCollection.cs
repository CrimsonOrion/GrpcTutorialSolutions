
using ProtoBuf.Grpc.Configuration;

using System.Reflection;

namespace ProtoBufNet.WebServer;

internal class ServiceBinderWithServiceResolutionFromServiceCollection : ServiceBinder
{
    private readonly IServiceCollection _services;

    public ServiceBinderWithServiceResolutionFromServiceCollection(IServiceCollection services) => _services = services;

    public override IList<object> GetMetadata(MethodInfo method, Type contractType, Type serviceType)
    {
        Type? resolvedServiceType = serviceType;
        if (serviceType.IsInterface)
        {
            resolvedServiceType = _services.SingleOrDefault(_ => _.ServiceType == serviceType)?.ImplementationType ?? serviceType;
        }
        return base.GetMetadata(method, contractType, resolvedServiceType);
    }
}