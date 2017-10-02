using System;
using System.Collections.Concurrent;

namespace FrameworkNamespace
{
    public class UserManager
    {
        private ConcurrentDictionary<long, User> userNoDict = new ConcurrentDictionary<long, User>();
        private ConcurrentDictionary<string, User> nicknameDict = new ConcurrentDictionary<string, User>();

        //private Object dictLock = new Object();
        /**
		@brief
		필요한 객체를 생성하는 생성자.
		*/
        public UserManager()
        {
            
        }

        public User GetUserByUserNo(long userNo)
        {
            User user;
            if (userNoDict.TryGetValue(userNo, out user) == false)
            {
                Console.WriteLine("not exist user - userNo : " + userNo);

                return null;
            }

            return user;
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
            //User user = new User(session, userNo, nickname);

            //if (userNoDict.TryAdd(userNo, user) == false)
            //{
            //    Console.WriteLine("already exist user - userNo : " + userNo); //debug

            //    return null;
            //}

            //if (nicknameDict.TryAdd(nickname, user) == false)
            //{
            //    Console.WriteLine("already exist user - nickname : " + nickname); //error

            //    userNoDict.TryRemove(userNo, out user);

            //    return null;
            //}

            //return user;

            return null;
        }

        public bool AddUser(User user)
        {
            if (userNoDict.TryAdd(user.UserNo, user) == false)
            {
                Console.WriteLine("already exist user - userNo : " + user.UserNo); //debug

                return false;
            }

            if (nicknameDict.TryAdd(user.Nickname, user) == false)
            {
                Console.WriteLine("already exist user - nickname : " + user.Nickname); //error

                userNoDict.TryRemove(user.UserNo, out user);

                return false;
            }

            return true;
        }

        //public void removeFrontUser(string loginToken)
        //{
        //          if (loginTokenDict.Remove(loginToken) == false)
        //	{
        //		Console.WriteLine("removeUser");
        //	}
        //}
    }
}
