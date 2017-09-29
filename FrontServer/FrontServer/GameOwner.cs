using System;
using ConoNetworkLibrary;
using FrameworkNamespace;

namespace FrontServer
{
	public class GameOwner : Owner
	{
		public GameOwner(IConoConnect connect) : base(connect)
		{
		}

		public override void delete()
		{
			throw new NotImplementedException();
		}
	}
}
