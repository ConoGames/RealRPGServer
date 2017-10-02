using System;
using System.Collections.Concurrent;
using ConoNetworkLibrary;

namespace FrameworkNamespace
{
    public class SessionManager
    {
        protected ConcurrentDictionary<String, Session> sessionDict = new ConcurrentDictionary<String, Session>();
        protected ConcurrentQueue<Session> timeoutQueue = new ConcurrentQueue<Session>();

        private long deleteTime;

        public SessionManager(long deleteTime)
        {
            this.deleteTime = deleteTime;
        }

        public Session GetSession(String sessionId)
        {
            Session session = sessionDict[sessionId];

            if (session != null)
            {
                return session;
            }

            return session;
        }

        public Session CreateSession(ConoConnect connect)
        {
            String sessionId = Guid.NewGuid().ToString();

            Session session = new Session(connect, sessionId);

            session.DisconnectTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            if (sessionDict.TryAdd(sessionId, session) == false)
            {
                Console.WriteLine("already exist sessionId - " + sessionId);

                return null;
            }

            return session;
        }

        public Session AddSession(Session session)
        {
            if (sessionDict.TryAdd(session.SessionId, session) == false)
            {
                Console.WriteLine("already exist sessionId - " + session.SessionId);

                return null;
            }

            return session;
        }

        public void DisconnectSession(Session session)
        {
			session.DisconnectTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            timeoutQueue.Enqueue(session);
        }

		public void ProcessDelete()
		{
			//Iterator iterator = timeoutQueue.iterator();
			long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            Session session;

            while(timeoutQueue.TryPeek(out session))
            {
                if (session.DisconnectTime + deleteTime > currentTime)
                {
                    SessionTimeOut(session);
                }
                else
                {
                    break;
                }
            }
		}

		public void SessionTimeOut(Session session)
        {
            Session removeSession;
            if (sessionDict.TryRemove(session.SessionId, out removeSession))
            {
                if(timeoutQueue.TryDequeue(out session))
                {
                    
                }
                else
                {
                    Console.WriteLine("timeout but not exist in timeoutQueue - sessionId : " + session.SessionId);
                }
                
            }
            else
            {
                Console.WriteLine("timeout but not exist in sessionDict - sessionId : " + session.SessionId);
            }
        }
	}
}
