namespace LandaGRPCClient
{
    class GrpcServiceDiscoveryInfo
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public override string ToString()
        {
            return $"{IPAddress}:{Port}";
        }
    }
}
