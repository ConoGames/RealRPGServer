using System;
namespace ConoNetworkLibrary
{
	/**
	@brief
	연결하고 싶은 클라이언트, 서버의 정보를 담는 클래스

	@details
	클래스를 사용하여 연결할 대상들의 정보를 객체화 시킨 후, ConoNetwork에게 전송하면 됨.
	*/
    public class ConoNetConfig
    {
        private string ip; ///< ip
        private int port; ///< port
		private string serverModule; ///< 서버모듈을 지정해 ConnectModule을 찾을 수 있음.(모듈명이 겹치면 안됨)
		private string serverRule; ///< 연결요청인지 연결등록인지 구분하는 변수. "server"로 등록하면 연결요청한다는 의미, "client"로 등록하면 연결등록을 한다는 의미.
        private IConoNetworkHandler networkHandler; ///< 연결된 소켓들의 통신을 이 객체를 통해 전달됨.

        public string Ip
        {
            get
            { 
                return ip; 
            }
            set
            {
                ip = value;
            }
        }
		public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        public string ServerModule
        {
            get
            {
                return serverModule;
            }
            set
            {
                serverModule = value;
            }
        }
        public string ServerRule
        {
            get
            {
                return serverRule;
            }
            set
            {
                serverRule = value;
            }
        }
        public IConoNetworkHandler NetworkHandler
        {
            get
            {
                return networkHandler;
            }
            set
            {
                networkHandler = value;
            }
        }

		/**
		@brief
		클래스 내부 변수를 초기화 시키는 함수

		@details
		받은 정보들을 클래스 변수들로 세팅한다.
		*/
		public bool Init(string ip, int port, string serverModule, string serverRule, IConoNetworkHandler networkHandler)
		{
            this.ip = ip;
			this.port = port;
			this.serverModule = serverModule;
			this.serverRule = serverRule;
			this.networkHandler = networkHandler;

			return true;
		}
	}
}
