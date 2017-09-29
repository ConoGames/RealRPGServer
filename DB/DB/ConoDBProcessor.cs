using System;

namespace ConoDBLibrary
{
	public class ConoDBProcessor
	{
		IConoDBHandler handler;
		internal ConoDBJobQueue dbJobQueue;
		private ConoDBConnection dbConnection;

		public ConoDBProcessor()
		{
		}

		public bool Init(ConoDBConfig config)
		{
			try
			{
				dbJobQueue = new ConoDBJobQueue();

				if (dbJobQueue.Init() == false)
				{
					return false;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			dbConnection = new ConoDBConnection();

			if (dbConnection.Init(config.ip, config.port, config.dbName, config.uid, config.pwd) == false)
			{
				return false;
			}

			this.handler = config.handler;

			return true;
		}

		public void Run()
		{
			while (true)
			{
				ConoDBJob dbJob = dbJobQueue.Pop();
				dbJob.Process(dbConnection);

				handler.ReceiveResult(dbJob, dbConnection.jsonObject);
			}
		}
	}
}
