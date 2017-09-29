using System.Collections.Generic;

namespace ConoNetworkLibrary
{
	/**
	@brief
	ConoConnectModule을 관리하는 클래스

	@details
	모든 ConoConnectModule을 관리한다.
	*/
	public class ConoConnectModuleManager
	{
		internal Dictionary<string, ConoConnectModule> moduleDict; ///< 모듈들을 가지고있는 Dictionary

		/**
		@brief
		변수 초기화하는 생성자
		*/
		public ConoConnectModuleManager()
		{
			moduleDict = new Dictionary<string, ConoConnectModule>();
		}

		/**
		@brief
		ConnectModule을 추가하는 함수

		@details
		ConoNetConfig를 받아 ConoConnectModule을 만든다.
		*/
		public ConoConnectModule AddConnectModule(ConoNetConfig netConfig)
		{
			ConoConnectModule connectModule = new ConoConnectModule();

			connectModule.Init(netConfig);

			moduleDict.Add(connectModule.serverModule, connectModule);

			return connectModule;
		}
	}
}
