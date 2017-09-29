using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ConoNetworkLibrary
{
	public class ConoConnectPool
	{
		//Stack<ConoConnect> conoConnectPool;

		//public ConoConnectPool()
		//{
		
		//}

		//public bool Init(int capacity)
		//{
		//	conoConnectPool = new Stack<ConoConnect>(capacity);

		//	lock (conoConnectPool)
		//	{
		//		for (int i = 1; i <= capacity; i++)
		//		{
		//			ConoConnect conoConnect = new ConoConnect(i);
		//			SocketAsyncEventArgs sendSocketAsyncEventArgs = new SocketAsyncEventArgs();
		//			sendSocketAsyncEventArgs.SetBuffer(new byte[1024], 0, 1024);

		//			SocketAsyncEventArgs recvSocketAsyncEventArgs = new SocketAsyncEventArgs();
		//			recvSocketAsyncEventArgs.SetBuffer(new byte[1024], 0, 1024);

		//			conoConnect.Init(sendSocketAsyncEventArgs, recvSocketAsyncEventArgs);

		//			conoConnectPool.Push(conoConnect);
		//		}
		//	}

		//	return true;
		//}


		//public void Push(ConoConnect item)
		//{
		//	if (item == null) { throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null"); }

		//	lock (conoConnectPool)
		//	{
		//		conoConnectPool.Push(item);
		//	}
		//}

		//public ConoConnect Pop()
		//{
		//	lock (conoConnectPool)
		//	{
		//		return conoConnectPool.Pop();
		//	}
		//}
        
		//public int Count
		//{
		//	get { return conoConnectPool.Count; }
		//}
	}
}
