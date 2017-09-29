﻿using System;
using ConoNetworkLibrary;
using System.Collections.Generic;
using PacketNameSpace;

namespace FrameworkNamespace
{
    public abstract class NetworkHandler : IConoNetworkHandler
    {
        protected Dictionary<int, INetworkProcessor> npDict;

        public NetworkHandler()
        {
            npDict = new Dictionary<int, INetworkProcessor>();
        }

        public void setNetworkProcessor(int cmd, INetworkProcessor networkProcessor)
        {
            npDict.Add(cmd, networkProcessor);
        }

        public abstract void Connect(ConoConnect connect);
        public abstract void Disconnect(ConoConnect connect);
        public abstract void ReceiveData(ConoConnect connect, byte[] data, int dataLen);
        //{
			//byte[] cmdByte = new byte[4];

			//for (int i = 0; i < 4; i++)
			//{
			//	cmdByte[i] = data[i];
			//}

			//int cmd = BitConverter.ToInt32(cmdByte, 0);

			//INetworkProcessor networkProcessor = null;

			//if (npDict.TryGetValue(cmd, out networkProcessor) == false)
			//{
			//	Console.WriteLine("ReceiveData - cmd : " + cmd);

			//	return;
			//}

			//Packet packet = Packet.Deserialize(data, dataLen);

			//networkProcessor.Process(connect, packet);
        //}
    }
}
