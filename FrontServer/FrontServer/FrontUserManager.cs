using System;
using FrameworkNamespace;
using System.Collections.Generic;

namespace FrontServer
{
    public class FrontUserManager : UserManager
    {
		/**
		@brief
		필요한 객체를 생성하는 생성자. 
		*/
        public FrontUserManager()
        {
            
        }

        public bool AddFrontUser(FrontUser user)
		{
            return AddUser(user);
		}

		public void removeFrontUser(FrontUser user)
		{
            removeUser(user);
		}

    }
}
