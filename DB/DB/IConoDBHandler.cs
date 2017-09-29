using System;
using LitJson;

namespace ConoDBLibrary
{
	public interface IConoDBHandler
	{
		void ReceiveResult(ConoDBJob conoDBJob, JsonData jObject);
	}
}
