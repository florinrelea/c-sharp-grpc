using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            } else
            { 
                output.FirstName = "Greg";
                output.LastName = "Thomas";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Florin1",
                    LastName = "lnFlo",
                    EmailAddress = "florin@example1.com",
                    Age = 21,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Florin2",
                    LastName = "lnflo2",
                    EmailAddress = "florin@example2.com",
                    Age = 20,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Florin3",
                    LastName = "lnflo3",
                    EmailAddress = "florin@example3.com",
                    Age = 20,
                    IsAlive = true
                }
            };

            foreach (var cust in customers)
            {
                await Task.Delay(3000);
                await responseStream.WriteAsync(cust);
            }

            Console.WriteLine("Finsihed task on server.");
        }
    }
}
