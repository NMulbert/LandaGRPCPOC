using Grpc.Core;
using System;
using ZooKeeperNet;

namespace LandaGRPCClient
{

    public static class GRPCClient
    {
        public static Channel CreateChannel(string serviceName)
        {
            GrpcServiceDiscoveryInfo grpcServiceDiscoveryInfo;
            using (var zooKeeper = new ZooKeeper("localhost:21811", TimeSpan.FromSeconds(5), null))
            {
                // Perform operations with ZooKeeper
                var grpcServiceDiscoveryInfoBytes = zooKeeper.GetData(serviceName, false, null);

                //parse the byte array to GRPCServiceDiscoveryInfo
                var grpcServiceDiscoveryInfoText = System.Text.Encoding.UTF8.GetString(grpcServiceDiscoveryInfoBytes);
                var grpcServiceDiscoveryInfoParts = grpcServiceDiscoveryInfoText.Split(':');
                grpcServiceDiscoveryInfo = new GrpcServiceDiscoveryInfo
                {
                    IPAddress = grpcServiceDiscoveryInfoParts[0],
                    Port = int.Parse(grpcServiceDiscoveryInfoParts[1])
                };
            }

            Channel channel = new Channel($"{grpcServiceDiscoveryInfo.IPAddress}:{grpcServiceDiscoveryInfo.Port}", ChannelCredentials.Insecure);
            
            return channel;
        }
    }
}
