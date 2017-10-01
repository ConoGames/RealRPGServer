using System;

namespace FrameworkNamespace
{
    public abstract class BaseUser
    {
        protected Session session;

        public Session Session
        {
            get
            {
                return session;
            }

            set
            {
                session = value;
            }
        }

        public BaseUser(Session session)
        {
            this.session = session;
        }
    }
}
