using System;
using ConoNetworkLibrary;
using FrameworkNamespace;

namespace FrontServer
{
    public class FrontUser : User
    {
        private string loginToken;

        public string LoginToken
        {
            get
            {
                return loginToken;
            }

            set
            {
                loginToken = value;
            }
        }

        public FrontUser(Session session, long userNo, String nickname) : base(session, userNo, nickname)
        {
        }

        public override void delete()
        {
            throw new NotImplementedException();
        }
    }
}
