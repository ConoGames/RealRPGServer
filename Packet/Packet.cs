﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Text;

namespace PacketNameSpace
{
    [Serializable]
    public abstract class Packet
	{
		public int cmd;

		public Packet(int cmd)
		{
			this.cmd = cmd;
		}


   //     public abstract int Serialize(byte[] data);

   //     public abstract Packet Deserialize(byte[] data);

   //     public int GetCommand(byte[] cmdData)
   //     {
			//byte[] byteCmd = new byte[4];

			//for (int i = 0; i < cmdData; i++)
			//{
			//	byteCmd[i] = cmdData[i];
			//}

			//// If the system architecture is little-endian (that is, little end first),
			//// reverse the byte array.
			//if (BitConverter.IsLittleEndian)
			//	Array.Reverse(byteCmd);

			//int i = BitConverter.ToInt32(byteCmd, 0);
			
        //    Console.WriteLine("int: {0}", i);

        //    return i;
        //}
	}

	//public struct StringData : Packet
	//{
	//	private int stringLen;
	//	private string myString;

	//	public string SD
	//	{
	//		get
	//		{
	//			return myString;
	//		}

	//		set
	//		{
	//			myString = value;
	//			stringLen = Encoding.UTF8.GetByteCount(myString);
	//		}
	//	}


	//	public override int Serialize(byte[] data)
	//	{
	//		Array dataArray = new Array();

 //           byte[] intBytes = BitConverter.GetBytes(stringLen);
	//		if (BitConverter.IsLittleEndian)
	//			Array.Reverse(intBytes);
	//		data = intBytes;

 //           byte[] stringByte = BitConverter.GetBytes(myString);

 //           byte[] result = intBytes + stringByte;

	//		return sizeof(int);
	//	}

	//	public override Packet Deserialize(byte[] data)
	//	{
	//		return sizeof(int);
	//	}



	//	public int Length()
	//	{
	//		return sizeof(int) + stringLen;
	//	}


	//}

}
