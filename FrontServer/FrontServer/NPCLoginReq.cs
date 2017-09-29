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

        public void Process(IConoConnect connect, Packet packet)
        {
            FrontUser user = (FrontUser)connect.GetOwner();

            if (user == null)
            {
                Console.WriteLine("???");
                return;
            }

            if (user.startProcessRequest(packet) == false)
            {
                Console.WriteLine("????");
            }
            else
            {
				ClientFrontPacket.LoginReqPacket loginReqPacket = (ClientFrontPacket.LoginReqPacket)packet;

				string loginToken = loginReqPacket.loginToken;

                FrontManager.Instance.GetOwnerManager((int)NETWORK_MODULE.NETWORK_MODULE_CLIENT).GetOwner();

				DJLogin job = new DJLogin(user);
                job.LoginToken = loginToken;
                job.SessionId = user.SessionId;

				ConoDB.Instance.ProcessQuery(job);
            }
        }
    }
}
