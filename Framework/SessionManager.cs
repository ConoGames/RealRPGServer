using System;
using System.Collections.Generic;
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

        public void DisconnectSession(Session session)
        {
			session.DisconnectTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            timeoutQueue.Enqueue(session);
        }

		public void processDelete()
		{
			//Iterator iterator = timeoutQueue.iterator();
			long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            Session session;

            while(timeoutQueue.TryPeek(out session))
            {
                if (session.DisconnectTime + deleteTime > currentTime)
                {
                    Session removeSession;
                    if (sessionDict.TryRemove(session.SessionId, out removeSession))
                    {
                        timeoutQueue.TryDequeue(out session);

                        sessionTimeOut(session);
                    }
                    else
                    {
                        Console.WriteLine("timeout but not exist in sessionDict - sessionId : " + session.SessionId);
                        return;
                    }
                }
                else
                {
                    break;
                }
            }
		}

		public void sessionTimeOut(Session session)
        {
            
        }
	}
}
