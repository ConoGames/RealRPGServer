namespace ConoNetworkLibrary
{
	/**
	@brief
	연결된 상대방의 정보를 담고 있는 인터페이스.

	@details
	ConoNetworkLibrary 사용자들을 위해 제공되는 인터페이스.
	연결된 상대방에게 데이터 전송, 소유자 등록 등을 할 수 있다.
	*/
	public interface IConoConnect
	{

		/**
		@brief
		상대방에게 데이터를 전송해줌.

		@details
		보낼 데이터와 데이터의 길이를 보내서 상대방에게 전송함.
		*/
        void Send(byte[] data, int dataLen);


		/**
		@brief
		Owner를 등록하면 앞으로 이객체에서 바로 Owner를 가져올 수 있다.
		*/
		void SetOwner(IConoOwner owner);

		/**
		@brief
		Owner가져오는 함수.
		*/
		IConoOwner GetOwner();
	}
}
