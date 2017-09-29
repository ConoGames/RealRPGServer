using System;
using FrameworkNamespace;

namespace FrontServer
{
    public class FrontManager : ObjectManager
    {
		private static volatile FrontManager instance; ///< ConoNetwork를 싱글톤으로 만들기 위한 변수
		private static object syncRoot = new Object(); ///< 멀티쓰레드환경에서 동기화 문제를 해결하기 위한 변수

		/**
        @brief
        필요한 객체를 생성하는 생성자. 
        */
        private FrontManager()
		{
            ownerManagers = new OwnerManager[(int)NETWORK_MODULE.NETWORK_MODULE_COUNT];
			ownerManagers[(int)NETWORK_MODULE.NETWORK_MODULE_LOBBY] = new OwnerManager(300000);
			ownerManagers[(int)NETWORK_MODULE.NETWORK_MODULE_GAME] = new OwnerManager(300000);
			ownerManagers[(int)NETWORK_MODULE.NETWORK_MODULE_CLIENT] = new FrontUserManager(300000);
		}

		/**
        @brief
        싱글톤 객체를 부르는 함수
        */
		public static FrontManager Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new FrontManager();
					}
				}

				return instance;
			}
		}
    }
}
