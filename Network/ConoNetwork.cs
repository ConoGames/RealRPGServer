using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;

/**
 @brief Netowork 연결을 위한 클래스들을 포함하는 네임스페이스
*/
namespace ConoNetworkLibrary
{
	/**
	@brief
	네트워크 통신을 위한 싱글톤 클래스이다.

	@details
	이 객체를 사용하여 모든 네트워크 작업을 수행한다.
	*/
	public class ConoNetwork
	{
		private static volatile ConoNetwork instance; ///< ConoNetwork를 싱글톤으로 만들기 위한 변수
		private static object syncRoot = new Object(); ///< 멀티쓰레드환경에서 동기화 문제를 해결하기 위한 변수

		ConoConnectModuleManager conoConnectModuleManager; ///< ConnectModule을 관리하기 위한 변수
		//internal ConoConnectPool conoConnectPool; ///< ConoConnect의 할당, 해제 반복을 최소화시키는 풀
		internal ConoBufferPool bufferPool; ///< buffer의 할당, 해제 반복을 최소화시키는 풀

		private bool isRunning; ///< 이미 네트워크 통신을 시작하고 있다면(Run함수가 호출되었다면) true, 아니면 false

		/**
		@brief
		필요한 객체를 생성하는 생성자. 
		*/
		private ConoNetwork()
		{
			conoConnectModuleManager = new ConoConnectModuleManager();

			//conoConnectPool = new ConoConnectPool();
			bufferPool = new ConoBufferPool();

			isRunning = false;
		}

		/**
		@brief
		싱글톤 객체를 부르는 함수
		*/
		public static ConoNetwork Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new ConoNetwork();
					}
				}

				return instance;
			}
		}

		/**
		@brief
		ConoNetwork를 세팅하는 함수

		@details
		네트워크 작업에 필요한 매개변수들을 받아 세팅함.
		한번만 호출해야 됨.

		@param int capacity\n
		다른 서버, 클라이언트 들과 연결하기 위한 connect 최대 갯수.\n
		풀로 원하는 갯수만큼 할당되어 관리함.\n

		@param ConoNetConfig[] netConfigArray\n
		연결등록, 또는 연결요청할 정보들을 매개변수로 전달해줘야 됨.\n

		@return bool
		성공시 true, 실패시 false 반환
		*/
		public bool Init(int capacity, ConoNetConfig[] netConfigArray)
		{
			if (bufferPool.Init(capacity, 1024) == false) // capacity만큼 풀에서 buffer를 만듬.
			{
				return false;
			}


			for (int i = 0; i < netConfigArray.Length; i++) // 받은 netConfig 갯수만큼 ConnectModule을 생성함.
			{
				conoConnectModuleManager.AddConnectModule(netConfigArray[i]);
			}

			return true;
		}

		/**
		@brief
		ConoNetwork를 시작하는 함수.

		@details
		세팅되어있는 네트워크 모듈들을 기반으로 연결등록, 연결요청을 시작함
		한번만 호출해야 됨.
		*/
		public void Run()
		{
			lock (syncRoot)
			{
				if (isRunning == false)
					isRunning = true;
				else
					return;
			}

			foreach (KeyValuePair<string, ConoConnectModule> keyValuePair in conoConnectModuleManager.moduleDict)
			{
				ConoConnectModule connectModule = keyValuePair.Value;

				if (ProcessConnectModule(connectModule) == false)
				{
					//if false, remove connectModule and send false throught IConoNetworkHandler.Connect(null)
				}
			}
		}

		/**
		@brief
		새로운 연결작업을 요청할 때 호출하는 함수

		@details
		네트워크 작업에 필요한 매개변수을 받아 세팅함.
		Run중이라면 바로 네트워크 연결작업을 시작함.

		@param ConoNetConfig netConfig\n
		연결등록, 또는 연결요청할 정보를 매개변수로 전달해줘야 됨.\n

		@return bool
		성공시 true, 실패시 false 반환
		*/
		public bool AddNetConfig(ConoNetConfig netConfig)
		{
			ConoConnectModule connectModule = conoConnectModuleManager.AddConnectModule(netConfig);

			if (isRunning)
			{
				if (ProcessConnectModule(connectModule) == false)
				{
					return false;
				}
			}

			return true;
		}

		/**
		@brief
		받은 ConoConnectModule의 네트워크 작업을 시작함.

		@details
		네트워크 작업에 필요한 매개변수을 받아 세팅함.

		@param ConoNetConfig netConfig\n
		연결등록, 또는 연결요청할 정보를 매개변수로 전달해줘야 됨.\n

		@return bool
		성공시 true, 실패시 false 반환
		*/
		public bool ProcessConnectModule(ConoConnectModule connectModule)
		{
			if (connectModule.serverRule == "server")
			{
				ConoClientConnector clientConnector = new ConoClientConnector();

				if (clientConnector.Init(connectModule) == false)
				{
					return false;
				}

				clientConnector.Connect();
			}
			else if (connectModule.serverRule == "client")
			{
				ConoListener listener = new ConoListener();

				if (listener.Init(connectModule) == false)
				{
					connectModule.networkHandler.Connect(null);

					return false;
				}

				listener.Listen();
			}
			else
			{
				return false;
			}

			return true;
		}
	}
}