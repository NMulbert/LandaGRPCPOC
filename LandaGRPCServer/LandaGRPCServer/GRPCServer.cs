using Grpc.Core;
using System.Net;
using System.Threading.Tasks;
using static Grpc.Core.Server;

namespace LandaGRPCServer
{
    public static class GRPCServer
    {
        private static Server _server;
        public static async Task StartServerAsync(string serviceName, params ServerServiceDefinition[] serverServiceDefinitions)
        {
            _server = new Server();

            foreach (var service in serverServiceDefinitions)
            {
                _server.Services.Add(service);
            }

            GRPCServiceDiscoveryInfo = _server.StartWithDiscovery(serviceName);

            await Task.CompletedTask;
        }
        public static GRPCServiceDiscoveryInfo GRPCServiceDiscoveryInfo { get; private set; }
        public static async Task StopServerAsync()
        {
            await _server.ShutdownAsync();
        }
    }
}
