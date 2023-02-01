using System;
using HelloWorld;
using LandaGRPCClient;

namespace LandaGRPCClientConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = GRPCClient.CreateChannel("/IPS");

            var client = new Greeter.GreeterClient(channel);

            string user = "you";
            var reply = client.SayHello(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }

    }
}
