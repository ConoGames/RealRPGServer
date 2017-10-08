using FrameworkNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontServer
{
    class FrontSingleton
    {
        private SessionManager sessionMgr;
        private FrontUserManager userMgr;

        private static volatile FrontSingleton instance;
        private static object syncRoot = new Object();

        private FrontSingleton()
        {
            sessionMgr = new SessionManager(30000);
            userMgr = new FrontUserManager();
        }

        public FrontUserManager UserMgr
        {
            get { return userMgr; }
            set { userMgr = value; }
        }

        public SessionManager SessionMgr
        {
            get { return sessionMgr; }
            set { sessionMgr = value; }
        }


        public static FrontSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FrontSingleton();
                    }
                }

                return instance;
            }
        }
    }
}
