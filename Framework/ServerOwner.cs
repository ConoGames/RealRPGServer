using System;
using ConoNetworkLibrary;
using System.Collections.Generic;
using PacketNameSpace;
using ConoDBLibrary;

namespace FrameworkNamespace
{
    public class ServerOwner : Owner
    {
        protected long ownerNo;

        public override void Delete()
        {

        }

        public ServerOwner(Session session, long ownerNo) : base(session)
        {
            this.ownerNo = ownerNo;
        }

        public long OwnerNo
        {
            get { return ownerNo; }
            set { ownerNo = value; }
        }
    }   
}
