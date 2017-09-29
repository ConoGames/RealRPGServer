using System;
using ConoNetworkLibrary;
using FrameworkNamespace;

namespace FrontServer
{
    public class FrontUser : Owner
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

        public FrontUser(IConoConnect connect) : base(connect)
        {
        }

        public override void delete()
        {
            throw new NotImplementedException();
        }
    }
}
