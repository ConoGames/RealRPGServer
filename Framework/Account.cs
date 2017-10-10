using System;

namespace FrameworkNamespace
{
    public abstract class Account : Owner
    {
        protected long userNo;

        public long UserNo
        {
            get
            {
                return userNo;
            }

            set
            {
                userNo = value;
            }
        }

        public Account(Session session, long userNo) : base(session)
        {
            this.userNo = userNo;
        }
    }
}
