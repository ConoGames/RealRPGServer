using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Configuration;
using LitJson;

namespace PacketNameSpace
{
    public class FrontLobbyPacket
    {
		public static byte[] Serialize(Packet packet)
		{
			string jsonString = JsonMapper.ToJson(packet);
            byte[] data = Encoding.UTF8.GetBytes(jsonString);

			return data;
		}


		public static Packet Deserialize(byte[] data, int dataLen)
		{
			string jsonString = Encoding.Default.GetString(data, 0, dataLen);

			JsonData jData = JsonMapper.ToObject(jsonString);

			int cmd = int.Parse(jData["cmd"].ToString());

			Packet packet;

            if (cmd == (int)COMMAND.CONNECT_REQ)
            {
                packet = JsonMapper.ToObject<ConnectReqPacket>(jsonString);
            }
            else if (cmd == (int)COMMAND.CONNECT_RES)
            {
                packet = JsonMapper.ToObject<ConnectResPacket>(jsonString);
            }
            else if (cmd == (int)COMMAND.ENTER_USER_REQ)
            {
                packet = JsonMapper.ToObject<EnterUserReqPacket>(jsonString);
            }
            else if (cmd == (int)COMMAND.ENTER_USER_RES)
            {
                packet = JsonMapper.ToObject<EnterUserResPacket>(jsonString);
            }
            else
            {
                packet = null;
            }

			return packet;
		}

		public enum COMMAND
		{
			CONNECT_REQ,
            CONNECT_RES,

			ENTER_USER_REQ,
			ENTER_USER_RES,
		}

        [Serializable]
		public class ConnectReqPacket : Packet
		{
			public long serverNo;
			public string sessionId;

			public string clientIp;
            public int clientPort;

            public ConnectReqPacket() : base((int)COMMAND.CONNECT_REQ) { }

		}

        [Serializable]
		public class ConnectResPacket : Packet
		{
			public long serverNo;
			public string sessionId;

			public ConnectResPacket() : base((int)COMMAND.CONNECT_RES) { }

		}

        [Serializable]
		public class EnterUserReqPacket : Packet
		{
			public long userNo;
			public string sessionId;

			public EnterUserReqPacket() : base((int)COMMAND.ENTER_USER_REQ) { }
		}

        [Serializable]
		public class EnterUserResPacket : Packet
		{
            public long userNo;

			public EnterUserResPacket() : base((int)COMMAND.ENTER_USER_RES) { }
		}

    }
}
