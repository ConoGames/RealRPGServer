using System;
using ConoNetworkLibrary;
using PacketNameSpace;
using FrameworkNamespace;

namespace FrontServer
{
    public class NPLConnectReq : INetworkProcessor
    {
        public NPLConnectReq()
        {
        }

        public void Process(Session session, Packet packet)
        {
            FrontLobbyPacket.ConnectReqPacket crPacket = (FrontLobbyPacket.ConnectReqPacket)packet;

            long serverNo = crPacket.serverNo;
            string sessionId = crPacket.sessionId;
            string ip = crPacket.clientIp;
            int port = crPacket.clientPort;

            LobbyOwner owner = (LobbyOwner)connect.GetOwner();

            owner.ClientIp = ip;
            owner.ClientPort = port;

            FrontManager.Instance.GetOwnerManager((int)NETWORK_MODULE.NETWORK_MODULE_LOBBY).AddConnectOwner(serverNo, owner);
        }
    }
}
