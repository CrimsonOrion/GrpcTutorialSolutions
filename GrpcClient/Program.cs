using Grpc.Core;
using Grpc.Net.Client;

using GrpcServer;

namespace GrpcClient;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //HelloRequest request = new() { Name = "Jim" };
        GrpcChannel? channel = GrpcChannel.ForAddress("https://localhost:7141");
        //Greeter.GreeterClient client = new(channel);

        //var reply = await client.SayHelloAsync(request);

        //Console.WriteLine(reply.Message);

        Customer.CustomerClient client = new(channel);

        CustomerLookupModel request = new() { UserId = 1 };

        CustomerModel? reply = await client.GetCustomerInfoAsync(request);

        Console.WriteLine($"{reply.FirstName} {reply.LastName}");

        Console.WriteLine();
        Console.WriteLine("New Customer List:");
        Console.WriteLine();

        using (AsyncServerStreamingCall<CustomerModel>? call = client.GetNewCustomers(new NewCustomerRequest()))
        {
            while (await call.ResponseStream.MoveNext())
            {
                CustomerModel? currentCustomer = call.ResponseStream.Current;

                Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress}");
            }
        }

        _ = Console.ReadLine();
    }
}
