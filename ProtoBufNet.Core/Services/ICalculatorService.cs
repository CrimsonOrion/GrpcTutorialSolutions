
using System.ServiceModel;

namespace ProtoBufNet.Core;

[ServiceContract(Name = "Hyper.Calculator")]
public interface ICalculator
{
    ValueTask<MultiplyResult> MultiplyAsync(MultiplyRequest request);
}
