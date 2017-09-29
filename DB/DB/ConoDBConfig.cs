using System;
namespace ConoDBLibrary
{
	public class ConoDBConfig
	{
		public ConoDBConfig()
		{
		}
internal string ip; ///< ip
internal int port; ///< port
internal string dbName; ///< database name
internal string uid; ///< user name
internal string pwd; ///< password
		internal IConoDBHandler handler; ///< handling task

		public string Ip
		{
			get
			{
				return ip;
			}
			set
			{
				ip = value;
			}
		}
		public int Port
		{
			get
			{
				return port;
			}
			set
			{
				port = value;
			}
		}

		public string DBName
		{
			get
			{
				return dbName;
			}
			set
			{
				dbName = value;
			}
		}
		public string Uid
		{
			get
			{
				return uid;
			}
			set
			{
				uid = value;
			}
		}
		public string Pwd
		{
			get
			{
				return pwd;
			}
			set
			{
				pwd = value;
			}
		}
		public IConoDBHandler Handler
		{
			get
			{
				return handler;
			}
			set
			{
				handler = value;
			}
		}

		/**
		@brief
		클래스 내부 변수를 초기화 시키는 함수

		@details
		받은 정보들을 클래스 변수들로 세팅한다.
		*/
		public bool Init(string ip, int port, string dbName, string uid, string pwd, IConoDBHandler handler)
		{
			this.ip = ip;
			this.port = port;
			this.dbName = dbName;
            this.uid = uid;
            this.pwd = pwd;
			this.handler = handler;

			return true;
		}
	}
}
