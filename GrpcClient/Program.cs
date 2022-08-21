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

            var clientRequested = new CustomerLookupModel { UserId =  1 };

            var reply = await customerClient.GetCustomerInfoAsync(clientRequested);
                
            Console.WriteLine("Reply message {0}", reply.ToString());

            Console.ReadLine();
        }
    }
}