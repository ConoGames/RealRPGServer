using System;
using ConoNetworkLibrary;
using ConoDBLibrary;
using PacketNameSpace;
using FrameworkNamespace;

namespace FrontServer
{
    public class NPCLoginReq : INetworkProcessor
    {
        public NPCLoginReq()
        {
        }

        public void Process(Session session, Packet packet)
        {
			ClientFrontPacket.LoginReqPacket loginReqPacket = (ClientFrontPacket.LoginReqPacket)packet;

			string loginToken = loginReqPacket.loginToken;

			DJLogin job = new DJLogin(session);
            job.LoginToken = loginToken;

			ConoDB.Instance.ProcessQuery(job);
        }
    }
}
