using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConoNetworkLibrary
{
	/**
	@brief
	연결등록을 하는 클래스.

	@details
	ConoConnectModule의 정보를 기반으로 다른 클라이언트의 연결을 받기 위한 클래스임.
	*/
	public class ConoListener : ConoConnector
	{
		SocketAsyncEventArgs socketAsyncEventArgs; ///< 연결요청을 비동기로 받기위한 비동기소켓

		AutoResetEvent flowControlEvent; ///< 연결요청이 오기전까지 wait를 하기 위한 변수

		Thread thread; ///< DoListen()함수를 돌리기 위한 쓰레드 객체

		/**
		@brief
		클래스 초기화 함수

		@details
		ConoConnectModule을 받아 네트워크 연결 전 단계까지 해놓음.
		*/
		public override bool Init(ConoConnectModule connectModule)
		{
			if (base.Init(connectModule) == false)
			{
				Console.WriteLine("init fail");
				return false;
			}

			try
			{
				socket.Bind(endpoint);
				socket.Listen(100);

				socketAsyncEventArgs = new SocketAsyncEventArgs();
				socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptCompleted);

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

				return false;
			}

			return true;
		}

		/**
		@brief
		상대방의 연결을 받는 작업을 하는 쓰레드 만드는 함수

		@details
		쓰레드 만드는 이유 : 몇몇 OS에서의 버그가 있었음.\n
		참고자료 - http://stackoverflow.com/questions/12464185/windows-8-net-tcp-acceptasync-callback-not-firing-blocked-by-console-readlin
		*/
		public void Listen()
		{
			try
			{
				thread = new Thread(DoListen);
				thread.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
        
		/**
		@brief
		상대방의 연결을 받는 등록을 하는 함수

		@details
		상대방의 연결요청을 async하게 받아서 AcceptCompleted함수로 넘겨준다.
		*/
		void DoListen()
		{
			flowControlEvent = new AutoResetEvent(false);

			while (true)
			{
				socketAsyncEventArgs.AcceptSocket = null;

				bool pending = true;
				try
				{

					pending = socket.AcceptAsync(socketAsyncEventArgs);
					Console.WriteLine("acceptAsync");
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					continue;
				}
                
				if (!pending)
				{
                    AcceptCompleted(null, socketAsyncEventArgs);
				}

				this.flowControlEvent.WaitOne(); //wait until call Set();
			}
		}
        
		/**
		@brief
		상대방의 연결요청이 왔을 때 호출되는 함수

		@details
		ConoConnect 객체를 만들고 데이터 받을 준비를 함.\n
		*/
		void AcceptCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.Success)
			{
				Socket clientSocket = e.AcceptSocket;

				this.flowControlEvent.Set();

				ConoConnect connect = connectModule.CreateConnect(clientSocket);

				if (connect == null)
				{
					Console.WriteLine("conoConnect");
					return;
				}

                connect.recvSocketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(receiveData);

				//bool pending = true;
				//try
				//{
				//	connect.recvSocketAsyncEventArgs.RemoteEndPoint = endpoint;
				//	pending = socket.ConnectAsync(connect.recvSocketAsyncEventArgs);
				//}
				//catch (Exception e)
				//{
				//	Console.WriteLine(e.Message);
				//	return;
				//}

				connectModule.networkHandler.Connect(connect);

				startReceive(connect);
			}
			else
			{
				Console.WriteLine("Failed to accept client.");
			}
		}

	}
}