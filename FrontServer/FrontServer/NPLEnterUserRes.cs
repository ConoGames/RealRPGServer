using System;
using ConoNetworkLibrary;
using PacketNameSpace;
using FrameworkNamespace;

namespace FrontServer
{
	public class NPLEnterUserRes : INetworkProcessor
	{
		public NPLEnterUserRes()
		{
		}

		public void Process(IConoConnect connect, Packet packet)
		{
            FrontLobbyPacket.EnterUserResPacket eurPacket = (FrontLobbyPacket.EnterUserResPacket)packet;

            long userNo = eurPacket.userNo;

            FrontUser user = (FrontUser)FrontManager.Instance.GetOwnerManager((int)NETWORK_MODULE.NETWORK_MODULE_CLIENT).GetOwner(userNo);

            if(user == null)
            {
                Console.WriteLine("user null - userNo : " + userNo);
            }

            ClientFrontPacket.LoginResPacket lrPacket = new ClientFrontPacket.LoginResPacket();
            lrPacket.userNo = eurPacket.userNo;
			lrPacket.ip = ((LobbyOwner)connect.GetOwner()).ClientIp;
            lrPacket.port = ((LobbyOwner)connect.GetOwner()).ClientPort;

            byte[] data = ClientFrontPacket.Serialize(lrPacket);

            user.Connect.Send(data, data.Length);
		}
	}
}
