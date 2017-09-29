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
	ConoConnectModule의 정보를 기반으로 다른 서버들에게 연결을 요청 위한 클래스임.
	*/
	public class ConoClientConnector : ConoConnector
	{
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
            
			return true;
		}

		/**
		@brief
		연결요청을 하는 함수.

		@details
		연결요청을 하고 연결요청의 결과값을 receiveComplete함수로 넘겨줌.
		*/
		public void Connect()
		{
			ConoConnect connect = connectModule.CreateConnect(socket);

			if (connect == null)
			{
				Console.WriteLine("conoConnect");
				return;
			}

			connect.recvSocketAsyncEventArgs.Completed +=  new EventHandler<SocketAsyncEventArgs>(receiveComplete);

			bool pending = true;
			try
			{
				connect.recvSocketAsyncEventArgs.RemoteEndPoint = endpoint;
				pending = socket.ConnectAsync(connect.recvSocketAsyncEventArgs);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}


			if (!pending)
			{
                receiveComplete(null, connect.recvSocketAsyncEventArgs);
			}
		}

		/**
		@brief
		연결요청이 완료되거나 데이터통신이 왔을 때 이 함수가 호출됨.

		@details
		연결요청에 대한 결과값에 따라 처리함. 데이터통신은 startreceive를 호출해줌.
		*/
		void receiveComplete(object sender, SocketAsyncEventArgs e)
		{
            if (e.LastOperation == SocketAsyncOperation.Connect)
            {
                if (e.SocketError == SocketError.Success)
                {
                    

                    ConoConnect connect = e.UserToken as ConoConnect;

                    connectModule.networkHandler.Connect(connect);

                    startReceive(connect);
                }
                else
                {
                    Console.WriteLine("Failed to connect with server.");

                    IConoNetworkHandler networkHandler = connectModule.networkHandler;

                    //ToDo. remove connectModule, clientConnector, connect

                    networkHandler.Connect(null);
                }
            }
            else
            {
                receiveData(null, e);
            }
		}
	}
}