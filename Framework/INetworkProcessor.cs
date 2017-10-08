﻿using System;
using ConoNetworkLibrary;
using PacketNameSpace;

namespace FrameworkNamespace
{
    public interface INetworkProcessor
    {
        void Process(Session session, Packet packet);
    }
}
