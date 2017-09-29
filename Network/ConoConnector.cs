using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

namespace ConoNetworkLibrary
{
	/**
	@brief
	연결등록, 요청을 수행하는 클래스

	@details
	ConoNetConfig를 통해 들어온 정보들을 실질적으로 네트워크 작업을 한다.\n
	ConoConnectModule과 1:1매칭됨.\n
	이 클래스를 상속받아 연결을 등록하는 ConoListener와 연결을 요청하는 ConoClientConnector로 나뉜다.
	*/
	public abstract class ConoConnector
	{
		internal Socket socket; ///< 연결등록, 요청을 위한 socket변수
		internal ConoConnectModule connectModule; ///< ConnectModule을 관리하기 위한 변수
		internal IPEndPoint endpoint; ///< 걍 아이피 포트 등록할 때 쓰는 거임

		/**
		@brief
		클래스 초기화 함수

		@details
		ConoConnectModule을 받아 네트워크 연결 전 단계까지 해놓음.\n
		*/
		public virtual bool Init(ConoConnectModule connectModule)
		{
			this.connectModule = connectModule;
			socket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			IPAddress address;

			if (connectModule.ip == "0.0.0.0")
			{
				address = IPAddress.Any;
			}
			else
			{
				address = IPAddress.Parse(connectModule.ip);
			}

			endpoint = new IPEndPoint(address, connectModule.port);

			return true;
		}

		/**
		@brief
		연결 이후나 데이터를 한번 읽은 이후 이 함수를 호출해 다른 컴퓨터로부터 올 데이터를 기다린다고 설정함.

		@details
		비동기로 상대방의 정보를 기다림.\n

		@todo
		stack overflow 발생 가능성 있음. check!
		*/
		protected void startReceive(ConoConnect connect)
		{
			bool pending = connect.socket.ReceiveAsync(connect.recvSocketAsyncEventArgs);
			if (!pending)
			{
                receiveData(null, connect.recvSocketAsyncEventArgs);
			}
		}

		/**
		@brief
		데이터를 받았을 때 이 함수를 호출함.

		@details
		데이터가 짤려 오거나 여러개로 붙어오거나 하는 것들을 하나의 완전한 데이터로 만듬.\n
		첫 데이터의 4바이트는 데이터 크기를 나타냄.\n
		처리 이후에는 다시 startReceive를 호출해 다음 데이터를 기다림.
		*/
		protected void receiveData(object sender, SocketAsyncEventArgs e)
		{
            ConoConnect connect = e.UserToken as ConoConnect;

            if (e.SocketError == SocketError.Success)
			{
				byte[] trimedData = connect.GetTrimedData();

				byte[] wholeByte = null;

				int wholeByteLen = 0;

				if (trimedData == null) //저장된 버퍼가 존재하지 않을 때
                {

					wholeByte = e.Buffer;

					wholeByteLen = e.BytesTransferred;

				}
                else //버퍼에 저장된 것이 있을 때
                {

					wholeByte = new byte[e.BytesTransferred + trimedData.Length];

					for (int i = 0; i < e.BytesTransferred + trimedData.Length; ++i)
					{
						wholeByte[i] = i < trimedData.Length ? trimedData[i] : e.Buffer[i - trimedData.Length];
					}

					wholeByteLen = wholeByte.Length;
				}

				while (true)
				{
					if (e.BytesTransferred < 4) // 길이값을 나타내는 문자열보다 작은 양의 데이터가 왔을 때
                    {
						connect.SetTrimedData(wholeByte, wholeByteLen);

						break;
					}

					//첫 4바이트를 잘라서 데이터의 크기를 확인
					byte[] bytes = new byte[4];

					for (int i = 0; i < 4; i++)
					{
						bytes[i] = wholeByte[i];
					}

					int dataSize = BitConverter.ToInt32(bytes, 0);


					int receivedDataSize = wholeByteLen - 4;


					if (receivedDataSize == dataSize) // 날아온 데이터와 받아야 할 데이터의 크기가 일치할 때
					{

						byte[] data = new byte[receivedDataSize];

						for (int i = 0; i < receivedDataSize; i++)
						{
							data[i] = wholeByte[i + 4];
						}

						Console.WriteLine("receive data - " + Encoding.Default.GetString(data, 0, dataSize));

						connect.connectModule.networkHandler.ReceiveData(connect, data, dataSize);

						connect.SetTrimedData(null, 0);


						break;

					}
					else if (receivedDataSize < dataSize) // 날아온 데이터와 받아야 할 데이터의 크기보다 작을 때
                    {

						connect.SetTrimedData(wholeByte, wholeByteLen);

						break;

					}
					else  // 날아온 데이터와 받아야 할 데이터의 크기보다 클 때(첫 완전한 데이터를 처리하고 다음 데이터로 다시 receiveData를 수행함)
                    {

						byte[] firstData = new byte[dataSize];

						for (int i = 0; i < dataSize; i++)
						{
							firstData[i] = wholeByte[i + 4];
						}

						byte[] secondData = new byte[receivedDataSize - dataSize];

						for (int i = 0; i < receivedDataSize - dataSize; i++)
						{
							secondData[i] = wholeByte[i + dataSize + 4];
						}

						connect.SetTrimedData(secondData, secondData.Length);

                        Console.WriteLine("receive data - " + Encoding.Default.GetString(firstData, 0, dataSize));

						connect.connectModule.networkHandler.ReceiveData(connect, firstData, dataSize);

						wholeByte = new byte[secondData.Length];

						for (int i = 0; i < secondData.Length; i++)
						{
							wholeByte[i] = secondData[i];
						}

						continue;
					}
				}
			}
			else
			{
				Console.WriteLine("Failed to receive.");
			}

			startReceive(connect); //다음 데이터를 기다림.
        }
	}
}