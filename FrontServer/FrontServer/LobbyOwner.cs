using System;
using ConoNetworkLibrary;
using FrameworkNamespace;

namespace FrontServer
{
    public class LobbyOwner : Owner
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

        public LobbyOwner(IConoConnect connect) : base(connect)
        {
        }

        public override void delete()
        {
            throw new NotImplementedException();
        }
    }
}
