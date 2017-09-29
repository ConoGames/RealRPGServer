using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Configuration;
using LitJson;

namespace PacketNameSpace
{
    public class ClientLobbyPacket
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

			Packet packet = null;

			//if (cmd == (int)COMMAND.CONNECT_INFO_NOTIFY)
			//{
			//  packet = JsonMapper.ToObject<ConnectInfoNotifyPacket>(jsonString);
			//}
			//else if (cmd == (int)COMMAND.LOGIN_REQ)
			//{
			//  packet = JsonMapper.ToObject<LoginReqPacket>(jsonString);
			//}
			//else if (cmd == (int)COMMAND.LOGIN_RES)
			//{
			//  packet = JsonMapper.ToObject<LoginResPacket>(jsonString);
			//}
			//else
			//{
			//  packet = null;
			//}

			return packet;
		}





        public enum COMMAND
		{
			CONNECT_REQ = 0,
            CONNECT_RES,

			START_ADVENTURE_REQ,
			START_ADVENTURE_RES,

			COMMAND_COUNT,
		}

		[Serializable]
		public class ConnectReqPacket : Packet
		{
			public long userNo;
			public string sessionId;

			public ConnectReqPacket() : base((int)COMMAND.CONNECT_REQ) { }
		}

		[Serializable]
		public class ConnectResPacket : Packet
		{
			public string nickname;

			public int exp;

			public int jam;
			public int gold;
			public int mileage;

			public int advTicket;
			public long advTicketUseTime;

            public ConnectResPacket() : base((int)COMMAND.CONNECT_RES) { }
		}


		[Serializable]
		public class StartAdventureReqPacket : Packet
		{
            public int stageNum;

            public StartAdventureReqPacket() : base((int)COMMAND.START_ADVENTURE_REQ) { }
		}

		[Serializable]
		public class StartAdventureResPacket : Packet
		{
			public string ip;
			public int port;

            public StartAdventureResPacket() : base((int)COMMAND.START_ADVENTURE_RES) { }
		}
    }
}
