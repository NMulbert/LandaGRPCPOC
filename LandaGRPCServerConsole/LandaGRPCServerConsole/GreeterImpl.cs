using System.Threading.Tasks;
using Grpc.Core;
using HelloWorld;

namespace LandaGRPCServerConsole
{
    class GreeterImpl : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            System.Console.WriteLine($"Client Connected From {context.Peer}");
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
