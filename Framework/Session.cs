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
            if (connect != null)
            {
                connect.SetOwner(this);
            }

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

		public bool startProcessRequest(Packet packet)
		{
            lock (processLock)
            {
                if (isProcessing || toProcessQueue.Count != 0)
                {
                    if(isProcessing && toProcessQueue.Count != 0)
                    {
                        Console.WriteLine("is processing");
                    }

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

		public Packet finishProcessRequest()
		{
            Packet packet = null;

            lock (processLock)
            {
                if (toProcessQueue.Count == 0)
                {
					isProcessing = false;
				}
                else
                {
                    packet = toProcessQueue.Dequeue();
                }
            }

			return packet;
        }
	}
}
