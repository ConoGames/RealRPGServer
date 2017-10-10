using System;
using System.Collections.Concurrent;

namespace FrameworkNamespace
{
    public class AccountManager : OwnerManager
    {
        private ConcurrentDictionary<long, User> userNoDict = new ConcurrentDictionary<long, User>();

		
        public AccountManager()
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

        public bool AddOwner(Owner owner)
        {
            User user = owner as User;

            if (userNoDict.TryAdd(user.UserNo, user) == false)
            {
                Console.WriteLine("already exist user - userNo : " + user.UserNo); //debug

                return false;
            }

            return true;
        }

        public void RemoveOwner(Owner owner)
        {
            User user = owner as User;

            if (userNoDict.TryRemove(user.UserNo, out user) == false)
            {
                Console.WriteLine("already exist user - userNo : " + user.UserNo); //error
            }
        }
    }
}
