using System;
using ConoNetworkLibrary;
using PacketNameSpace;
using FrameworkNamespace;

namespace FrontServer
{
    public class ClientNetworkHandler : NetworkHandler
    {
        public ClientNetworkHandler()
        {
            setNetworkProcessor((int)ClientFrontPacket.COMMAND.LOGIN_REQ, new NPCLoginReq());
        }

        public override void Connect(ConoConnect connect)
        {
            if(connect == null)
            {
                Console.WriteLine("connect null");

                return;
            }

			FrontUser user = new FrontUser(connect);
			user.Connect = connect;
			connect.SetOwner(user);

            ClientFrontPacket.ConnectInfoNotifyPacket packet = new ClientFrontPacket.ConnectInfoNotifyPacket();
            packet.sessionId = user.SessionId;

            byte[] data = ClientFrontPacket.Serialize(packet);

            connect.Send(data, data.Length);
		}

        public override void Disconnect(ConoConnect connect)
        {
            throw new NotImplementedException();
        }

        public override void ReceiveData(ConoConnect connect, byte[] data, int dataLen)
        {
            Packet packet = ClientFrontPacket.Deserialize(data, dataLen);

            int cmd = packet.cmd;

            INetworkProcessor networkProcessor = null;

            if(npDict.TryGetValue(cmd, out networkProcessor) == false)
            {
				Console.WriteLine("ReceiveData - cmd : " + cmd);

                return;
			}

            networkProcessor.Process(connect, packet);
		}
    }
}
