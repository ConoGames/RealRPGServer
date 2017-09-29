using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Configuration;
using LitJson;

namespace PacketNameSpace
{
    public class ClientFrontPacket
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

			if (cmd == (int)COMMAND.CONNECT_INFO_NOTIFY)
			{
				packet = JsonMapper.ToObject<ConnectInfoNotifyPacket>(jsonString);
			}
            else if (cmd == (int)COMMAND.LOGIN_REQ)
			{
                packet = JsonMapper.ToObject<LoginReqPacket>(jsonString);
			}
			else if (cmd == (int)COMMAND.LOGIN_RES)
			{
				packet = JsonMapper.ToObject<LoginResPacket>(jsonString);
			}
			else
            {
                packet = null;
            }

            return packet;
        }

        public enum COMMAND
        {
            CONNECT_INFO_NOTIFY = 0,

            LOGIN_REQ,
            LOGIN_RES,

            COMMAND_COUNT,
        }

        [Serializable]
        public class ConnectInfoNotifyPacket : Packet
		{
			public string sessionId;

            public ConnectInfoNotifyPacket() : base((int)COMMAND.CONNECT_INFO_NOTIFY) { }
		}

        [Serializable]
        public class LoginReqPacket : Packet
        {
            public string loginToken;

            public LoginReqPacket() : base((int)COMMAND.LOGIN_REQ) { }
        }

        [Serializable]
		public class LoginResPacket : Packet
		{
			public long userNo;

            public string ip;
            public int port;

            public LoginResPacket() : base((int)COMMAND.LOGIN_RES) { }
		}
    }
}
