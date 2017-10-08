using System;
using ConoNetworkLibrary;
using FrameworkNamespace;
using PacketNameSpace;

namespace FrontServer
{
    public class LobbyNetworkHandler : NetworkHandler
    {
		public LobbyNetworkHandler()
		{
			setNetworkProcessor((int)FrontLobbyPacket.COMMAND.CONNECT_REQ, new NPLConnectReq());
            setNetworkProcessor((int)FrontLobbyPacket.COMMAND.ENTER_USER_RES, new NPLEnterUserRes());
		}

		public override void Connect(ConoConnect connect)
		{
            String sessionId = Guid.NewGuid().ToString();

            Session session = new Session(connect, sessionId);

			LobbyOwner owner = new LobbyOwner(connect);
			owner.Connect = connect;
			connect.SetOwner(owner);

            FrontLobbyPacket.ConnectResPacket packet = new FrontLobbyPacket.ConnectResPacket();
            packet.serverNo = FrontManager.Instance.OwnerNo;
            packet.sessionId = "0";

            byte[] data = FrontLobbyPacket.Serialize(packet);

			connect.Send(data, data.Length);
		}


        public override void Disconnect(ConoConnect connect)
		{
			throw new NotImplementedException();
		}


		public override void ReceiveData(ConoConnect connect, byte[] data, int dataLen)
		{
			Packet packet = FrontLobbyPacket.Deserialize(data, dataLen);

			int cmd = packet.cmd;

			INetworkProcessor networkProcessor = null;

			if (npDict.TryGetValue(cmd, out networkProcessor) == false)
			{
				Console.WriteLine("ReceiveData - cmd : " + cmd);

				return;
			}

			networkProcessor.Process(connect, packet);
		}
    }
}
