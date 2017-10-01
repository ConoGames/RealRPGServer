using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.IO;


namespace ConoNetworkLibrary
{
	/**
	@brief
	연결과 관련된 모든 정보들을 가지고 있는 클래스

	@details
	연결이 되어 통신가능한 소켓이 생기면 반드시 이 클래스 객체가 만들어짐.\n

	@todo
	전달되는 trimedData를 copy없이 잘 저장하고 빼오도록 하는 것이 좋아보임.
	*/
	public class ConoConnect : IConoConnect
	{
		internal int uniqueKey; ///< 객체를 식별하는 유일한 값.
		internal Socket socket; ///< 실제로 연결된 소켓.
		private IConoOwner owner; ///< ConoNetworkLibrary사용자가 다른 정보들을 등록 할 수 있게 IConoOwner를 만들어줌.
		private byte[] trimedData; ///< 잘려서 온 정보들을 저장하는 변수
		internal SocketAsyncEventArgs sendSocketAsyncEventArgs; ///< 비동기로 데이터를 보내기 위한 비동기소켓
		internal SocketAsyncEventArgs recvSocketAsyncEventArgs; ///< 비동기로 데이터를 받기 위한 비동기소켓

		internal ConoConnectModule connectModule; ///< 나와 연결된 모듈 정보

		/**
		@drief
		owner getter setter
		*/
		public IConoOwner Owner
		{
			get
			{
				return owner;
			}
			set
			{
				owner = value;
			}
		}

		/**
		@brief
		잘린 데이터를 저장함.
		*/
		internal void SetTrimedData(byte[] data, int dataLen)
		{
			if (dataLen == 0)
			{
				trimedData = null;
				return;
			}

			trimedData = new byte[dataLen];

			Array.Copy(data, 0, trimedData, 0, dataLen);
		}

		/**
		@brief
		잘린 데이터를 가져옴.
		*/
		internal byte[] GetTrimedData()
		{
			return trimedData;
		}

		/**
		@brief
		멤버변수를 초기화시키는 생성자
		*/
		public ConoConnect(int uniqueKey)
		{
			this.uniqueKey = uniqueKey;
			recvSocketAsyncEventArgs = new SocketAsyncEventArgs();
			sendSocketAsyncEventArgs = new SocketAsyncEventArgs();
			owner = null;
			trimedData = null;
		}

		/**
		@brief
		멤버변수들을 값을 지정하는 함수
		*/
		public bool Init(Socket socket, ConoConnectModule connectModule)
		{
            this.socket = socket;
			this.connectModule = connectModule;

			recvSocketAsyncEventArgs.SetBuffer(ConoNetwork.Instance.bufferPool.BringBuffer(), 0, ConoNetwork.Instance.bufferPool.bufferSize);
			sendSocketAsyncEventArgs.SetBuffer(ConoNetwork.Instance.bufferPool.BringBuffer(), 0, ConoNetwork.Instance.bufferPool.bufferSize);

			recvSocketAsyncEventArgs.UserToken = this;
			sendSocketAsyncEventArgs.UserToken = this;

			return true;
		}

		/**
		@brief
		상대방에게 데이터를 전달하는 함수.

		@details
		헤더로 데이터의 크기를 앞에 보내고 데이터를 보냄.

		@param byte[] data\n
		전달할 데이터.\n

		@param int dataLen\n
		전달할 데이터의 길이.\n

		@todo
		한번의 socket.send호출로 데이터들을 보내야됨.
		*/
		public void Send(byte[] data, int dataLen)
		{
            Console.WriteLine("send data - " + Encoding.Default.GetString(data));

            byte[] dataLenByte = new byte[4];

            Array.Copy(BitConverter.GetBytes(dataLen), dataLenByte, 4);

            byte[] wholeData = new byte[dataLenByte.Length + data.Length];
            System.Buffer.BlockCopy(dataLenByte, 0, wholeData, 0, dataLenByte.Length);
            System.Buffer.BlockCopy(data, 0, wholeData, dataLenByte.Length, data.Length);

            socket.Send(wholeData, 0, wholeData.Length, SocketFlags.None);
        }

        /**
		@drief
		owner setter
		*/
        public void SetOwner(IConoOwner owner)
		{
			this.owner = owner;
		}

		/**
		@drief
		owner getter		
		*/
		public IConoOwner GetOwner()
		{
			return owner;
		}
    }
}
