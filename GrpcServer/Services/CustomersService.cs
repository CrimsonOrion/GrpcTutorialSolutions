using Grpc.Core;

namespace GrpcServer.Services;

public class CustomersService : Customer.CustomerBase
{
    private readonly ILogger<CustomersService> _logger;

    public CustomersService(ILogger<CustomersService> logger) => _logger = logger;

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
    {
        CustomerModel output = new();

        switch (request.UserId)
        {
            case 1:
                output.FirstName = "Jamie";
                output.LastName = "Smith";
                break;
            case 2:
                output.FirstName = "Jane";
                output.LastName = "Doe";
                break;
            default:
                output.FirstName = "Greg";
                output.LastName = "Thomas";
                break;
        }

        _logger.LogInformation("Retrieved Customer {@id}", request.UserId);
        return Task.FromResult(output);
    }

    public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
    {
        List<CustomerModel> customers = new()
        {
            new()
            {
                FirstName = "Tim",
                LastName = "Corey",
                EmailAddress = "tim@iamtimcorey.com",
                Age = 41,
                IsAlive = true
            },
            new()
            {
                FirstName = "Sue",
                LastName = "Storm",
                EmailAddress = "sue@stormy.net",
                Age = 28,
                IsAlive = false
            },
            new()
            {
                FirstName = "Bilbo",
                LastName = "Baggins",
                EmailAddress = "bilbo@middleearth.net",
                Age = 117,
                IsAlive = false
            },
        };

        foreach (CustomerModel? cust in customers)
        {
            _logger.LogInformation("Retrieving customer {@LastName}", cust.LastName);
            await Task.Delay(1000);
            await responseStream.WriteAsync(cust);
        }
        _logger.LogInformation("Finished");
    }
}
