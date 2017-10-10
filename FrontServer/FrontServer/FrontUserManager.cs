using System;
using FrameworkNamespace;
using System.Collections.Generic;

namespace FrontServer
{
    public class FrontUserManager : AccountManager
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
            return AddOwner(user);
		}

		public void RemoveFrontUser(FrontUser user)
		{
            RemoveOwner(user);
		}

    }
}
