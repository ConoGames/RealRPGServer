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
		protected Owner owner;
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
            owner = null;
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

		public Owner Owner
		{
            get { return owner; }
			set { owner = value; }
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

        public void AddRequest(Packet packet)
        {
            lock (processLock)
            {
                toProcessQueue.Enqueue(packet);
            }
        }

        public void FinishProcessRequest()
        {
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

                        isProcessing = true;
                    }
                }
            }

            return packet;
        }
    }
}
