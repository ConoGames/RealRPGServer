using System;


namespace FrameworkNamespace
{
    public abstract class Owner
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

        public Owner(Session session)
        {
            this.session = session;
        }

        public abstract void Delete();
    }
}