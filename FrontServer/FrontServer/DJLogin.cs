using System;
using ConoDBLibrary;
using MySql.Data.MySqlClient;
using System.Data;
using PacketNameSpace;
using FrameworkNamespace;

namespace FrontServer
{
    public class DJLogin : ConoDBJob
    {
        private string loginToken;

        public DJLogin(Session session) : base(session)
        {
            
        }

		public string LoginToken
		{
			set { loginToken = value; }
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
                Console.WriteLine("not exist user");

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

                FrontUser user = new FrontUser((Session)dbUser, userNo);

                FrontUserManager frontUserManager = FrontManager.Instance.GetOwnerManager((int)NETWORK_MODULE.NETWORK_MODULE_CLIENT) as FrontUserManager;

                if (frontUserManager.AddFrontUser(user) == false)
                {
                    Console.WriteLine("addUser error");

                    //ToDo. send error Packet to client

                    return;
                }

                ServerOwnerManager lobbyOwnerMgr = FrontManager.Instance.GetOwnerManager((int)NETWORK_MODULE.NETWORK_MODULE_LOBBY) as ServerOwnerManager;

                Owner owner = lobbyOwnerMgr.GetFreeOwner();

                if(owner == null)
                {
                    Console.WriteLine("lobby owner");

                    FrontUser frontUser = user as FrontUser;

                    //frontUser.Connect.Send();

                    return;
                }

                Session session = dbUser as Session;

                FrontLobbyPacket.EnterUserReqPacket packet = new FrontLobbyPacket.EnterUserReqPacket();

				packet.userNo = userNo;
                packet.sessionId = session.SessionId;
				
                byte[] data = FrontLobbyPacket.Serialize(packet);

                session.Connect.Send(data, data.Length);
			}
		}
    }
}
