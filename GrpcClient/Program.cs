using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7049");

            var client = new Greeter.GreeterClient(channel);

            var input = new HelloRequest { Name = "Tim" };

            var reply = await client.SayHelloAsync(input);

            Console.WriteLine("Reply message {0}", reply.Message);

            Console.ReadLine();
        }
    }
}