﻿using System;
using ConoNetworkLibrary;
using PacketNameSpace;

namespace FrameworkNamespace
{
    public interface INetworkProcessor
    {
        void Process(IConoConnect connect, Packet packet);
    }
}
