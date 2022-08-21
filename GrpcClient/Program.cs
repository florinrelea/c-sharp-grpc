using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer.Protos;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7049");

            //var client = new Greeter.GreeterClient(channel);

            //var input = new HelloRequest { Name = "Tim" };

            //var reply = await client.SayHelloAsync(input);

            var customerClient = new Customer.CustomerClient(channel);

            //var clientRequested = new CustomerLookupModel { UserId =  1 };

            //var reply = await customerClient.GetCustomerInfoAsync(clientRequested);

            var cts = new CancellationTokenSource();

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest())) 
            {
                 while (await call.ResponseStream.MoveNext(cts.Token))
                 {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine("Current customer {0}", currentCustomer.ToString());
                }
            }

            Console.WriteLine("The client finished.");

            Console.ReadLine();
        }
    }
}