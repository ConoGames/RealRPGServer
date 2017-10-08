using System;
using ConoNetworkLibrary;
using System.Collections.Generic;
using PacketNameSpace;
using ConoDBLibrary;

namespace FrameworkNamespace
{
    public class Session : IConoOwner, IConoDBUser
	{
		protected String sessionId;
		protected BaseUser user;
		protected IConoConnect connect;
        protected Queue<Packet> toSendQueue;

		protected bool isProcessing;
        protected Queue<Packet> toProcessQueue;
		
        protected int sendPacketCount;
		protected int receivePacketCount;

        protected long disconnectTime;

        protected object processLock;

		//public abstract void delete();

        public Session(IConoConnect connect, String sessionId)
		{
            this.sessionId = sessionId;
            user = null;
            this.connect = connect;

            toSendQueue = new Queue<Packet>();
        
            isProcessing = false;
            toProcessQueue = new Queue<Packet>();

            sendPacketCount = 0;
            receivePacketCount = 0;

            disconnectTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            processLock = new object();
		}

        public String SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

		public IConoConnect Connect
		{
			get { return connect; }
			set { connect = value; }
		}

		public BaseUser User
		{
            get { return user; }
			set { user = value; }
		}

		public long DisconnectTime
		{
            get { return disconnectTime; }
            set { disconnectTime = value; }
		}

        public bool IsProcessing
        {
            get { return isProcessing; }
        }

		public bool StartProcessRequest(Packet packet)
		{
            lock (processLock)
            {
                if (isProcessing || toProcessQueue.Count != 0)
                {
                    toProcessQueue.Enqueue(packet);

                    return false;
                }
                else
                {
                    isProcessing = true;

                    return true;
                }
            }
		}

        public void FinishProcessRequest()
        {
            Packet packet = null;

            lock (processLock)
            {
                isProcessing = false;
            }
        }


        public Packet StartProcessRequest()
        {
            Packet packet = null;

            lock (processLock)
            {
                if(IsProcessing)
                {
                    return null;
                }
                else
                {
                    if(toProcessQueue.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        packet = toProcessQueue.Dequeue();
                    }
                }
            }

            return packet;
        }
    }
}
