using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient1;

GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7274");

Greeter.GreeterClient client = new Greeter.GreeterClient(channel);
HelloReply reply = await client.SayHelloAsync( new HelloRequest { Name = "GreeterClient" });

Console.WriteLine( $"挨拶：{reply.Message}" );
Console.WriteLine( "何かキーを押すと終了します。" );
Console.ReadKey();