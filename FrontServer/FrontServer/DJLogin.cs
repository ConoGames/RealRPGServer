using System;
using ConoDBLibrary;
using ConoNetworkLibrary;
using MySql.Data.MySqlClient;
using System.Data;
using PacketNameSpace;
using FrameworkNamespace;

namespace FrontServer
{
    public class DJLogin : ConoDBJob
    {
        private string loginToken;
        private string sessionId;

        public DJLogin(FrontUser user) : base(user)
        {
            
        }

		public string LoginToken
		{
			set { loginToken = value; }
		}

		public string SessionId
		{
			set { sessionId = value; }
		}

		public override void Process(IConoDBConnection dbConnection)
        {
            string selectSql = "SELECT * FROM Account WHERE loginToken = \"" + loginToken + "\";";

		    MySqlCommand selectCommand = new MySqlCommand();
            selectCommand.Connection = dbConnection.Conn;
            selectCommand.CommandText = selectSql;

			DataSet ds = new DataSet();
			MySqlDataAdapter da = new MySqlDataAdapter(selectSql, dbConnection.Conn);
			da.Fill(ds);

            DataRow row = ds.Tables[0].Rows[0];

            if(row == null)
            {
                Console.WriteLine("Process not exist user");

			}
            else
            {
				long userNo = (long)row["accountNo"];
                //string nickname = (string)row["nickname"];
                //int exp = (int)row["exp"];
                //int jam = (int)row["jam"];
                //int gold = (int)row["gold"];
                //int mileage = (int)row["mileage"];
                //int advTicket = (int)row["advTicket"];
                //DateTime advTicketUseTime = (DateTime)row["advTicketUseTime"];

                //Console.WriteLine(string.Format("이름 : " + userNo + ", " + nickname + ", " + exp + ", " + jam + ", " + gold + ", " + mileage + ", " + advTicket + ", " + advTicketUseTime.ToLongDateString()));

                ((FrontUser)user).OwnerNo = userNo;

                FrontManager.Instance.GetOwnerManager((int)NETWORK_MODULE.NETWORK_MODULE_CLIENT).AddConnectOwner(userNo, (FrontUser)user);

                Owner owner = FrontManager.Instance.GetOwnerManager((int)NETWORK_MODULE.NETWORK_MODULE_LOBBY).GetFreeOwner();

                if(owner == null)
                {
                    Console.WriteLine("lobby owner");

                    FrontUser frontUser = (FrontUser)user;

                    //frontUser.Connect.Send();

                    return;
                }

                FrontLobbyPacket.EnterUserReqPacket packet = new FrontLobbyPacket.EnterUserReqPacket();

				packet.userNo = userNo;
                packet.sessionId = sessionId;
				
                byte[] data = FrontLobbyPacket.Serialize(packet);

                owner.Connect.Send(data, data.Length);
			}


			//foreach (DataRow row in ds.Tables[0].Rows)
			//{
			

			//}
		}
    }
}
