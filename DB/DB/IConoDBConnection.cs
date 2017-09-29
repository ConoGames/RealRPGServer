using System;
using LitJson;
using MySql.Data.MySqlClient;

namespace ConoDBLibrary
{
	public interface IConoDBConnection
	{
		MySqlConnection Conn { get; }
		JsonData JsonObject { set; }
	}
}
