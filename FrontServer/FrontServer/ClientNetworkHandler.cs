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

            String sessionId = Guid.NewGuid().ToString();

            Session session = new Session(connect, sessionId);

            if(FrontManager.Instance.GetSessionManager((int)NETWORK_MODULE.NETWORK_MODULE_CLIENT).AddSession(session) == false)
            {
                Console.WriteLine("FrontSingleton.Instance.SessionMgr.AddSession(session)");

                return;
            }

            connect.SetOwner(session);

            ClientFrontPacket.ConnectInfoNotifyPacket packet = new ClientFrontPacket.ConnectInfoNotifyPacket();
            packet.sessionId = session.SessionId;

            byte[] data = ClientFrontPacket.Serialize(packet);

            connect.Send(data, data.Length);
		}

        public override void Disconnect(ConoConnect connect)
        {
            Session session = connect.Owner as Session;

            if(session == null)
            {
                Console.WriteLine("session is null");

                return;
            }

            FrontManager.Instance.GetSessionManager((int)NETWORK_MODULE.NETWORK_MODULE_CLIENT).DisconnectSession(session);

            connect.Owner = null;
        }

        public override void ReceiveData(ConoConnect connect, byte[] data, int dataLen)
        {
            Session session = connect.Owner as Session;

            if (session == null)
            {
                Console.WriteLine("session is null");

                return;
            }

            Packet packet = ClientFrontPacket.Deserialize(data, dataLen);

            int cmd = packet.cmd;

            INetworkProcessor networkProcessor = null;

            if(npDict.TryGetValue(cmd, out networkProcessor) == false)
            {
				Console.WriteLine("ReceiveData - cmd : " + cmd);

                return;
			}

            session.AddRequest(packet);

            while (true)
            {
                packet = session.StartProcessRequest();

                if (packet != null)
                {
                    networkProcessor.Process(session, packet);

                    continue;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
