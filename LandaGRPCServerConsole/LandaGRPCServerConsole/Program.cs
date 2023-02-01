using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWorld;
using LandaGRPCServer;

namespace LandaGRPCServerConsole
{
    internal class Program
    {
        public static void Main(string[] argv)
        {
            GRPCServer.StartServerAsync("/IPS",Greeter.BindService(new GreeterImpl())).Wait();
            Console.WriteLine($"Server listening on: {GRPCServer.GRPCServiceDiscoveryInfo}");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            GRPCServer.StopServerAsync().Wait();
        }
    }
}
