using System;
using System.Threading;
namespace ConoDBLibrary
{
	public class ConoDB
	{
		private ConoDBProcessor[] processores;

		private static volatile ConoDB instance; ///< ConoDB를 싱글톤으로 만들기 위한 변수
		private static object syncRoot = new Object(); ///< 멀티쓰레드환경에서 동기화 문제를 해결하기 위한 변수

		private ConoDB()
		{
		}

		/**
		@brief
		싱글톤 객체를 부르는 함수
		*/
		public static ConoDB Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new ConoDB();
					}
				}

				return instance;
			}
		}
		public bool Init(int threadCount, ConoDBConfig config)
		{
			try
			{
				processores = new ConoDBProcessor[threadCount];

				for (int i = 0; i < threadCount; i++)
				{
					processores[i] = new ConoDBProcessor();
					if (processores[i].Init(config) == false)
					{
						//error
						Console.WriteLine("db init error");
						return false;
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			return true;
		}

		public void Run()
		{
			for (int i = 0; i< processores.Length; i++)
			{
				Thread thread = new Thread(new ThreadStart(processores[i].Run));
				thread.Start();
			}
		}

		public void ProcessQuery(ConoDBJob dbJob)
		{
			//Todo. push to free processor
			processores[0].dbJobQueue.Push(dbJob);
		}
	}
}
