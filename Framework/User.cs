using System;

namespace FrameworkNamespace
{
    public abstract class User : BaseUser
    {
        protected long userNo;
        protected String nickname;

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

        public String Nickname
        {
            get
            {
                return nickname;
            }

            set
            {
                nickname = value;
            }
        }

        public User(Session session, long userNo, String nickname) : base(session)
        {
            this.userNo = userNo;
            this.nickname = nickname;
        }
    }
}
