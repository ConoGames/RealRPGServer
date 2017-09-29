using System.Collections.Generic;
using System.Net.Sockets;


namespace ConoNetworkLibrary
{
	/**
	@brief
	연결할 네트워크 정보와 연결 후에 필요한 것들을 가지고 있는 클래스.

	@details
	전달받은 ConoNetConfig의 정보를 저장함.\n
	ConoNetConfig와 1:1 매칭됨.
	*/
	public class ConoConnectModule
	{
		internal string ip; ///< ip
		internal int port; ///< port
		internal string serverModule; ///< 서버모듈을 지정해 ConnectModule을 찾을 수 있음.(모듈명이 겹치면 안됨)
		internal string serverRule; ///< 연결요청인지 연결등록인지 구분하는 변수. "server"로 등록하면 연결요청한다는 의미, "client"로 등록하면 연결등록을 한다는 의미.

		internal IConoNetworkHandler networkHandler; ///< 연결된 소켓들의 통신을 이 객체를 통해 전달됨.

		internal Dictionary<int, ConoConnect> connectDict; ///< 연결된 소켓들을 관리함. (Listen시에는 상대방의 연결요청으로 인해 생성된 소켓, Connect시에는 연결요청된 소켓)

		/**
		@brief
		클래스 내부 변수를 초기화 시키는 함수

		@details
		ConoNetConfig 정보들을 클래스 변수들로 세팅한다.
		*/
		public virtual bool Init(ConoNetConfig netConfig)
		{
			ip = netConfig.Ip;
			port = netConfig.Port;
			serverModule = netConfig.ServerModule;
			serverRule = netConfig.ServerRule;
			networkHandler = netConfig.NetworkHandler;
			connectDict = new Dictionary<int, ConoConnect>();

			return true;
		}

		/**
		@brief
		ConoConnect를 생성
		*/
		internal ConoConnect CreateConnect(Socket socket)
		{
			ConoConnect connect = new ConoConnect(0);
			if (connect.Init(socket, this) == false)
			{
				return null;
			}

			return connect;
		}

	}
}
