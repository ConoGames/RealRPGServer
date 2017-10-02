using System;
using FrameworkNamespace;
using System.Collections.Generic;

namespace FrontServer
{
    public class FrontUserManager : UserManager
    {
        private Dictionary<string, FrontUser> loginTokenDict = new Dictionary<string, FrontUser>();

		/**
		@brief
		필요한 객체를 생성하는 생성자. 
		*/
        public FrontUserManager()
        {
            
        }

		public Owner GetFrontUser(string loginToken)
		{
            if (loginTokenDict.ContainsKey(loginToken) == false)
            {
                return null;
            }

            return loginTokenDict[loginToken];
   		}

        public bool AddFrontUser(string loginToken, FrontUser user)
		{
			user.DisconnectTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            if (loginTokenDict.ContainsKey(loginToken) == false)
			{
				loginTokenDict.Add(loginToken, user);

				return true;
			}
			else
			{
                if (loginTokenDict[loginToken] == user)
				{
					return true;
				}
				else
				{
					// Todo. dup user exist ( process it )
					Console.WriteLine("AddConnectUser");

					return false;
				}
			}
		}

		public void removeFrontUser(string loginToken)
		{
            if (loginTokenDict.Remove(loginToken) == false)
			{
				Console.WriteLine("removeUser");
			}

		}

    }
}
