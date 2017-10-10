using System;
using ConoNetworkLibrary;
using FrameworkNamespace;

namespace FrontServer
{
    public class FrontUser : Account
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

        public FrontUser(Session session, long userNo) : base(session, userNo)
        {
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
