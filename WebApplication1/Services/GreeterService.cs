using Grpc.Core;
using WebApplication1; // Protos 名前空間

namespace WebApplication1.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        // SayHello RPC メソッドの実装
        public override Task<HelloReply> SayHello( HelloRequest request, ServerCallContext context )
        {
            return Task.FromResult( new HelloReply
            {
                Message = $"Hello {request.Name}"
            } );
        }
    }
}
