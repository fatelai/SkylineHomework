using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;

GrpcChannel channel = null;
Greeter.GreeterClient client = null;
initGrpc();

//await GrpcBaseAsync();
//await GrpcServerSideAsync();
//await GrpcClientSideAsync();
await GrpcServerClientAsync();

Console.Read();

void initGrpc()
{
    channel = GrpcChannel.ForAddress("https://localhost:7213");
    client = new Greeter.GreeterClient(channel);
}

async Task GrpcBaseAsync()
{
    var reply = await client.SayHelloAsync(
                      new HelloRequest { Name = "GreeterClient" });

    Console.Write(reply);
}

async Task GrpcClientSideAsync()
{
    var call = client.SayHelloByStream();

    List<HelloRequest> ServerStreamData = new List<HelloRequest>()
    {
        new HelloRequest { Name = "GreeterClient1" },
        new HelloRequest { Name = "GreeterClient2" },
        new HelloRequest { Name = "GreeterClient3" },
        new HelloRequest { Name = "GreeterClient4" }
    };

    foreach (var request in ServerStreamData)
    {
        await call.RequestStream.WriteAsync(request);
    }

    // Complete the stream
    await call.RequestStream.CompleteAsync();

    Console.Write(await call.ResponseAsync);
}

async Task GrpcServerSideAsync()
{
    var responseStream = client.GetHelloStream(new HelloRequest() {  Name = "test" });

    await foreach (var response in responseStream.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine(response.Message);
    }
}

async Task GrpcServerClientAsync()
{
    List<HelloRequest> ServerStreamData = new List<HelloRequest>()
    {
        new HelloRequest { Name = "GreeterClient1" },
        new HelloRequest { Name = "GreeterClient2" },
        new HelloRequest { Name = "GreeterClient3" },
        new HelloRequest { Name = "GreeterClient4" }
    };

    var call = client.HelloStream();

    foreach (var request in ServerStreamData)
    {
        await call.RequestStream.WriteAsync(request);
    }

    await foreach (var response in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine(response.Message);
    }
}
