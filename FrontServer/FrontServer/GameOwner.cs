using System;
using ConoNetworkLibrary;
using FrameworkNamespace;

namespace FrontServer
{
	public class GameOwner : Owner
	{
		public GameOwner(Session session) : base(session)
		{
		}

		public override void Delete()
		{
			throw new NotImplementedException();
		}
	}
}
