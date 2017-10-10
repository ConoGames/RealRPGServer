using System;
using ConoNetworkLibrary;
using FrameworkNamespace;

namespace FrontServer
{
    public class LobbyOwner : ServerOwner
    {
		private string clientIp; ///< ip
		private int clientPort; ///< port
		
		public string ClientIp
		{
			get
			{
				return clientIp;
			}
			set
			{
				clientIp = value;
			}
		}
		public int ClientPort
		{
			get
			{
				return clientPort;
			}
			set
			{
				clientPort = value;
			}
		}

        public LobbyOwner(Session session, long ownerNo) : base(session, ownerNo)
        {
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
