using Grpc.Core;
using GrpcService;
using System.Text.Json;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        //Client-side
        public override async Task<HelloReply> SayHelloByStream(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            List<string> responseData = new();

            while (await requestStream.MoveNext()) {
              HelloRequest request = requestStream.Current;

                responseData.Add(request.Name);

                Console.WriteLine(request.Name);
            }

            return new HelloReply()
            {
                Message =
                JsonSerializer.Serialize(responseData)
            };
        }
        public override async Task GetHelloStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            await responseStream.WriteAsync(new HelloReply() { Message = request.Name + "1"});
            await responseStream.WriteAsync(new HelloReply() { Message = request.Name + "2" });
            await responseStream.WriteAsync(new HelloReply() { Message = request.Name + "3" });
        }

        public override async Task HelloStream(IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                HelloRequest request = requestStream.Current;

                await responseStream.WriteAsync(new HelloReply() { Message = request.Name + "1" });

                Console.WriteLine(request.Name);
            }
        }

    }
}