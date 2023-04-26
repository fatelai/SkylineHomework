using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcService;

using var channel = GrpcChannel.ForAddress("https://localhost:7213");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });

Console.Write(reply);

Console.Read();