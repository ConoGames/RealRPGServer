using System;
using ConoNetworkLibrary;
using ConoDBLibrary;
using LitJson;

namespace FrontServer
{
	public class DBHandler : IConoDBHandler
	{
        public void ReceiveResult(ConoDBJob conoDBJob, JsonData jObject)
		{
			Console.WriteLine("ReceiveResult");
		}
	}

	public class LibraryUsingTest
	{
        
		public LibraryUsingTest()
		{
		}

		public static void Main(String[] args)
		{
            //ClientFrontPacket.ConnectInfoNotifyPacket p = new ClientFrontPacket.ConnectInfoNotifyPacket();
            //p.sessionId = "fef라";

            //byte[] data = ClientFrontPacket.Serialize(p);

            //ClientFrontPacket.Deserialize(data, data.Length);

			//비동기로 받을 데이터를 넘겨주는 함수
			IConoNetworkHandler cnh = new ClientNetworkHandler();
			IConoNetworkHandler lnh = new LobbyNetworkHandler();
			IConoNetworkHandler gnh = new GameNetworkHandler();


			//연결 할거 설정
			ConoNetConfig[] netConfigs = new ConoNetConfig[3];

			netConfigs[0] = new ConoNetConfig();
			netConfigs[0].Init("127.0.0.1", 19000, "client", "client", cnh);

			netConfigs[1] = new ConoNetConfig();
			netConfigs[1].Init("127.0.0.1", 12000, "lobby", "client", lnh);

			netConfigs[2] = new ConoNetConfig();
            netConfigs[2].Init("127.0.0.1", 13000, "game", "client", gnh);


			//init
			ConoNetwork.Instance.Init(1000, netConfigs);

			//run
			ConoNetwork.Instance.Run();

			ConoDBConfig dbConfig = new ConoDBConfig();

			dbConfig.Init("127.0.0.1", 3307, "RealRpgDB", "root", "1234", new DBHandler());

			//db init
			ConoDB.Instance.Init(4, dbConfig);
			//db run
			ConoDB.Instance.Run();

            //create job intance
   //         DJLogin dbJob = new DJLogin(null);
   //         dbJob.LoginToken = "1234";

			////send Query
			//ConoDB.Instance.ProcessQuery(dbJob);


			//dbJob.Init(null);

			//ConoDB.Instance.ProcessQuery(dbJob);
			//ConoDB.Instance.ProcessQuery(dbJob);
			//ConoDB.Instance.ProcessQuery(dbJob);
			//ConoDB.Instance.ProcessQuery(dbJob);
			//ConoDB.Instance.ProcessQuery(dbJob);
			//ConoDB.Instance.ProcessQuery(dbJob);
			//ConoDB.Instance.ProcessQuery(dbJob);
			//ConoDB.Instance.ProcessQuery(dbJob);

			//string sectionDivider = "---------------------------------------- ";

			//Create an instance of AvroSample Class and invoke methods
			////illustrating different serializing approaches
			//LibraryUsingTest Sample = new LibraryUsingTest();

			////Serialization to memory using reflection
			//Sample.SerializeDeserializeObjectUsingReflection();

			//Console.WriteLine(sectionDivider);
			//Console.WriteLine("Press any key to exit.");
			//Console.Read();
		}

		////Serialize and deserialize sample data set represented as an object using reflection.
		////No explicit schema definition is required - schema of serialized objects is automatically built.
		//public void SerializeDeserializeObjectUsingReflection()
		//{

		//	Console.WriteLine("SERIALIZATION USING REFLECTION\n");
		//	Console.WriteLine("Serializing Sample Data Set...");

		//	//Create a new AvroSerializer instance and specify a custom serialization strategy AvroDataContractResolver
		//	//for serializing only properties attributed with DataContract/DateMember
		//	var avroSerializer = AvroSerializer.Create<SensorData>();

		//	//Create a memory stream buffer
		//	using (var buffer = new MemoryStream())
		//	{
  //              string[] d = new string[2];
  //              d[0] = "fefe";
  //              d[1] = "eeee";


		//		//Create a data set by using sample class and struct
		//		var expected = new SensorData { Value = d, Position = new Location { Room = 243, Floor = 1 } };

		//		//var expected = new SensorData(); //{ Value = 1, Position = new Location { Room = 243, Floor = 1 } };
		//		//expected.Value = d;
		//		//expected.Position = new Location();
  //              //expected.Position.Room = 1;
  //              //expected.Position.Floor = 123;

		//		//Serialize the data to the specified stream
		//		avroSerializer.Serialize(buffer, expected);


		//		Console.WriteLine("Deserializing Sample Data Set...");

		//		//Prepare the stream for deserializing the data
		//		buffer.Seek(0, SeekOrigin.Begin);

		//		//Deserialize data from the stream and cast it to the same type used for serialization
		//		var actual = avroSerializer.Deserialize(buffer);

		//		Console.WriteLine("Comparing Initial and Deserialized Data Sets...");

		//		//Finally, verify that deserialized data matches the original one
		//		bool isEqual = this.Equal(expected, actual);



		//		Console.WriteLine("Result of Data Set Identity Comparison is {0}", isEqual);


		//	}
		//}

		////
		////Helper methods
		////

		////Comparing two SensorData objects
		//private bool Equal(SensorData left, SensorData right)
		//{
  //          //return left.Position.Equals(right.Position) && left.GetValue().SequenceEqual(right.GetValue());
  //          return true;
		//}

	}
}
