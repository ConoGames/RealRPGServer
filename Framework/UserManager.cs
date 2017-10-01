using System;
using FrameworkNamespace;
using System.Collections.Concurrent;

namespace FrontServer
{
    public class UserManager
    {
        private ConcurrentDictionary<long, User> userNoDict = new ConcurrentDictionary<long, User>();
        private ConcurrentDictionary<string, User> nicknameDict = new ConcurrentDictionary<string, User>();

        private Object dictLock = new Object();
        /**
		@brief
		필요한 객체를 생성하는 생성자.
		*/
        public UserManager()
        {
            
        }

		public User GetUserByNickname(string nickname)
		{
            User user;
            if (nicknameDict.TryGetValue(nickname, out user) == false)
            {
                Console.WriteLine("not exist user - nickname : " + nickname);

                return null;
            }

            return user;
   		}

        public User CreateUser(Session session, long userNo, string nickname)
        {
            User user = new User(session, userNo, nickname);

            if (userNoDict.TryAdd(userNo, user) == false)
            {
                Console.WriteLine("already exist user - userNo : " + userNo); //debug

                return null;
            }

            if (nicknameDict.TryAdd(nickname, user) == false)
            {
                Console.WriteLine("already exist user - nickname : " + nickname); //error

                userNoDict.TryRemove(userNo, out user);

                return null;
            }

            return user;
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
