using System;
using System.Collections.Generic;

namespace FrameworkNamespace
{
    public class OwnerManager
    {
        //private static volatile UserManager instance; ///< ConoNetwork를 싱글톤으로 만들기 위한 변수

		protected Dictionary<long, Owner> ownerDict = new Dictionary<long, Owner>();
        protected Queue<Owner> timeoutQueue = new Queue<Owner>();

    //    private long currentSessionTime;
        private long deleteTime;

    //    protected Verticle verticle;

        public OwnerManager(long deleteTime)
        {
            this.deleteTime = deleteTime;
        }

        public Owner GetOwner(long ownerNo)
        {
            Owner owner = ownerDict[ownerNo];

            if (owner != null)
            {
                return owner;
            }

            return null;
        }

        public Owner GetFreeOwner()
        {
            if(ownerDict.Count == 0)
            {
                return null;
            }

            var e = ownerDict.GetEnumerator();
            e.MoveNext();
			
            var anElement = e.Current;

            return anElement.Value;
        }

        public bool AddConnectOwner(long ownerNo, Owner owner)
        {
            owner.DisconnectTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            if (ownerDict.ContainsKey(ownerNo) == false)
            {
                ownerDict.Add(ownerNo, owner);

                return true;
			}
            else
            {
                if (ownerDict[ownerNo] == owner)
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

        public bool AddDisconnectOwner(long ownerNo, String sessionId, Owner owner)
        {
			owner.DisconnectTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

			if (ownerDict.ContainsKey(ownerNo) == false)
			{
				ownerDict.Add(ownerNo, owner);



				return true;
			}
			else
			{
				if (ownerDict[ownerNo] == owner)
				{
                    timeoutQueue.Enqueue(owner);

					return true;
				}
				else
				{
					// Todo. dup user exist ( process it )
					Console.WriteLine("AddDisconnectUser");

					return false;
				}
			}
        }


        //public User getConnectUser(long userNo)
        //{
        //    User user = userMap.get(userNo);

        //    if (user == null || sessionMap.get(user.getSessionId()) != null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return user;
        //    }
        //}

        public void removeOwner(long ownerNo)
        {
            if(ownerDict.Remove(ownerNo) == false)
            {
                Console.WriteLine("removeUser");
            }

        }

		public void processDelete()
		{
			//Iterator iterator = timeoutQueue.iterator();
			long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            Owner owner;

			do
            {
                owner = timeoutQueue.Peek();

                if(owner.DisconnectTime + deleteTime > currentTime)
                {
                    sessionTimeOut(owner);

                    timeoutQueue.Dequeue();

                    removeOwner(owner.OwnerNo);
                }
                else
                {
                    break;
                }

			} while (owner != null);
		}

		public void sessionTimeOut(Owner owner)
        {
            
        }


		//    public User getDisconnectUser(String sessionId)
		//    {
		//        return sessionMap.get(sessionId);
		//    }

		//    public void removeDisconnectUser(String sessionId)
		//    {
		//        sessionMap.remove(sessionId);
		//    }

		//    public void deleteAll()
		//    {
		//        Iterator<Long> iterator = userMap.keySet().iterator();

		//        List<User> list = new ArrayList<>();

		//        while (iterator.hasNext())
		//        {
		//            User user = userMap.get(iterator.next());

		//            list.add(user);
		//        }

		//        for (User user : list)
		//        {
		//            disconnect(user);
		//            sessionTimeOut(user);
		//        }

		//        timeoutQueue.clear();
		//        //        userMap.clear();
		//        sessionMap.clear();
		//    }



		//    @Override
		//public void disconnect(User user)
		//    {

		//        user.disconnect();

		//        setDisconnectUser(user);

		//        ConnClientImpl connClient = user.getConnClient();

		//        if (connClient != null)
		//        {
		//            ((ConnClientImpl)connClient).setBaseUser(null);
		//            user.setConnClient(null);
		//        }
		//    }

		//    @Override
		//public User connect(Connect connClient)
		//    {

		//        String sessionId = ((ConnClientImpl)connClient).getSessionId();

		//        User user = getDisconnectUser(sessionId);

		//        if (user != null)
		//        {
		//            user.connect();

		//            setConnectUser(user);

		//            user.setConnClient(connClient);
		//            ((ConnClientImpl)connClient).setBaseUser(user);
		//        }

		//        return user;
		//    }

		//    @Override
		//public Object call() throws Exception
		//    {
		//        processDelete();
		//    return null;
		//}

		//public void deleteUser(User user)
		//{

		//    ConnClientImpl connClient = user.getConnClient();

		//    if (connClient != null)
		//    {
		//        connClient.setBaseUser(null);
		//        user.setConnClient(null);
		//    }

		//    sessionMap.remove(user.getSessionId());
		//    userMap.remove(user.getUserNo());

		//    user.delete();
		//}


		//public boolean request(User user, JsonObject jsonObject)
		//{

		//    String reqType = jsonObject.getString("req_type");

		//    UserProcess userProcess = userProcessMap.get(reqType);

		//    if (userProcess == null)
		//    {

		//        FileLog.logger.error("invalid req_type - " + reqType);

		//        return false;

		//    }
		//    else
		//    {
		//        if (user.startProcessRequest(jsonObject))
		//        {
		//            user.plusReceivePacketCount();

		//            try
		//            {

		//                userProcess.process(user, jsonObject);
		//            }
		//            catch (Exception e)
		//            {
		//                FileLog.ErrorLog(user.getUserNo(), e.toString());
		//            }
		//        }
		//        else
		//        {

		//        }
		//    }

		//    return true;
		//}

		//public void finishRequest(User user)
		//{
		//JsonObject jsonObject = user.finishProcessRequest();

		//if (jsonObject != null)
		//{
		//    String reqType = jsonObject.getString("req_type");

		//    UserProcess userProcess = userProcessMap.get(reqType);

		//    if (userProcess == null)
		//    {

		//        FileLog.logger.error("invalid req_type - " + reqType);

		//        return;

		//    }
		//    else
		//    {
		//        if (user.startProcessRequest(jsonObject))
		//        {
		//            userProcess.process(user, jsonObject);

		//        }
		//        else
		//        {
		//            FileLog.logger.error("???");
		//        }
		//    }
		//}
	}
}
