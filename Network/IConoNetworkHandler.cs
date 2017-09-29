namespace ConoNetworkLibrary
{
	/**
	@brief
	다른 클라이언트, 서버로부터 오는 통신을 이 함수를 통해 받음.

	@details
	ConoNetworkLibrary 사용자들을 위해 제공되는 인터페이스.\n
	ConNetConfig에 등록해두면 이쪽으로 통신이 넘어옴.
	*/
	public interface IConoNetworkHandler
	{
		/**
		@brief
		연결이 됬을 때 호출되는 함수.

		@details
		연결이 되면 이 함수를 호출해준다.

		@param ConoConnect connect\n
		연결 정보가 이곳에 담겨 옴.\n
		만약 null이 온다면 연결실패한 경우임.
		*/
		void Connect(ConoConnect connect);

		/**
		@brief
		상대방으로부터 데이터가 왔을 때 호출하는 함수.

		@details
		데이터와 데이터의 크기를 담아 호출한다.
		*/
		void ReceiveData(ConoConnect connect, byte[] data, int dataLen);

		/**
		@brief
		연결이 끊겼을 때 호출되는 함수.

		@details
		연결이 끊기면 이 함수를 호출해준다.
		*/
		void Disconnect(ConoConnect connect);
	}
}
