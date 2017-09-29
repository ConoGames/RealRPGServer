using System;
using MySql.Data.MySqlClient;
using LitJson;
namespace ConoDBLibrary
{
	public class ConoDBConnection : IConoDBConnection
	{
		private MySqlConnection conn;
        internal JsonData jsonObject;
		private String strConn;
		public ConoDBConnection()
		{
		}

		public MySqlConnection Conn
		{
			get
			{
				return conn;
			}	
		}

		public JsonData JsonObject
		{
			set
			{
				jsonObject = value;
			}
		}

		/**
		@brief
		클래스 내부 변수를 초기화 시키는 함수

		@details
		받은 정보들을 클래스 변수들로 세팅한다.
		*/
		public bool Init(string ip, int port, string dbName, string uid, string pwd)
		{
			strConn = "Server=" + ip + ";Port=" + port + ";Uid=" + uid + ";Pwd=" + pwd + ";Database=" + dbName + ";";

		    try
		    {
				conn = new MySqlConnection(strConn);

		        conn.Open();
		    }
		    catch (Exception e)
		    {
		        Console.WriteLine(e.StackTrace);

				return false;
		    }

			return true;
		}
	}
}