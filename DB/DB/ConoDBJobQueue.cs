using System;
using System.Collections.Generic;
using System.Threading;

namespace ConoDBLibrary
{
	/**
	@brief
	DBJob을 전달해주는 역할
	@details
	라이브러리 사용자로부터 받은 query를 ConoDBProcessor에게 넘겨줌.\n
	*/
	public class ConoDBJobQueue
	{
		private Queue<ConoDBJob> queue;

		Semaphore sem;
		/**
		@brief
		클래스 내부 변수를 초기화 시키는 함수
		*/
		public bool Init()
		{
			queue = new Queue<ConoDBJob>();
			sem = new Semaphore(0, Int32.MaxValue);

			return true;
		}

		/**
		@brief
		DBJob을 꺼내줌
		@details
		DBJob이 없을 때는 wait.\n
		*/
		public ConoDBJob Pop()
		{
			sem.WaitOne();

			ConoDBJob dbJob;

			lock(queue) //to use with ConoDB
			{
				dbJob = queue.Dequeue();
			}

			return dbJob;
		}

		/**
		@brief
		DBJob을 받아옴
		*/
		public void Push(ConoDBJob DBJob)
		{
			lock(queue)
			{
				queue.Enqueue(DBJob);
			}

			sem.Release();
		}
	}
}
