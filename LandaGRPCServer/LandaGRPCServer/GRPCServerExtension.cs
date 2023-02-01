using Grpc.Core;
using System;
using System.Linq;
using ZooKeeperNet;

namespace LandaGRPCServer
{
    public static class GRPCServerExtension
    {
        public static GRPCServiceDiscoveryInfo StartWithDiscovery(this Server grpcServer, string zooKeeperPath)
        {
            grpcServer.Ports.Add(new ServerPort("localhost", ServerPort.PickUnused, ServerCredentials.Insecure));
            grpcServer.Start();

            var chosenPort = grpcServer.Ports.First();

            var grpcServiceDiscoveryInfo = new GRPCServiceDiscoveryInfo
            {
                IPAddress = chosenPort.Host,
                Port = chosenPort.BoundPort
            };
            //convert the grpcServiceDiscoveryInfo in to byte array
            var grpcServiceDiscoveryInfoBytes = System.Text.Encoding.UTF8.GetBytes(grpcServiceDiscoveryInfo.ToString());

            using (var zooKeeper = new ZooKeeper("localhost:21811", TimeSpan.FromSeconds(100), null))
            {
                //check if the node exist
                if (zooKeeper.Exists(zooKeeperPath, false) == null)
                {
                    //create the node
                    zooKeeper.Create(zooKeeperPath, grpcServiceDiscoveryInfoBytes, Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
                }
                else
                {
                    //update the node
                    zooKeeper.SetData(zooKeeperPath, grpcServiceDiscoveryInfoBytes, -1);
                }
            }
            return grpcServiceDiscoveryInfo;
        }
    }
}
