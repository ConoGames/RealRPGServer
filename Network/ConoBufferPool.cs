using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ConoNetworkLibrary
{
	/**
	@brief
	연결이 생길때마다 할당과 해제를 반복해야 되서 미리 할당해놓고 꺼내쓰는 바이트풀

	@details
	필요할 때마다 BringBuffer으로 꺼내쓰고, 쓰고 난 버퍼는 ReturnBuffer로 돌려준다.
	*/
	public class ConoBufferPool
	{
		private int bufferCount; ///< whole buffer count
		private Stack<byte[]> freeBufferPool; ///< storage buffers
		internal int bufferSize; ///< one buffer size

		/**
		@brief
		필요한 객체를 생성하는 생성자.
		*/
		public ConoBufferPool()
		{
			freeBufferPool = new Stack<byte[]>();
		}

		/**
		@brief
		ConoBufferPool을 초기화시키는 함수

		@details
		bufferCount와 bufferSize로 변수들을 초기화시킨다.

		@param int bufferCount\n
		생성할 버퍼의 갯수\n

		@param int bufferSize\n
		생성할 버퍼의 크기\n

		@return bool
		성공시 true, 실패시 false 반
		*/
		public bool Init(int bufferCount, int bufferSize)
		{
            this.bufferCount = bufferCount;
			this.bufferSize = bufferSize;

			for (int i = 0; i < this.bufferCount; i++)
			{
				freeBufferPool.Push(new byte[bufferSize]);
			}

			return true;
		}


		/**
		@brief
		buffer를 줌.

		@details
		남아있는 buffer가 있다면 있는걸로 주고 없다면 buffer를 만들어준다
		*/
		public byte[] BringBuffer()
		{
            lock (freeBufferPool)
            {
                if (freeBufferPool.Count > 0)
                {
                    return freeBufferPool.Pop();
                }
                else
                {
                    return new byte[bufferSize];
                }
            }
		}


		/**
		@brief
		buffer를 돌려받음.

		@details
		사용이 끝난 버퍼를 돌려받는다
		*/
		public void FreeBuffer(byte[] buffer)
		{
            lock (freeBufferPool)
            {
                freeBufferPool.Push(buffer);
            }
		}
	}
}
