namespace ConoDBLibrary
{
	public abstract class ConoDBJob
	{
		protected IConoDBUser dbUser;

		public ConoDBJob(IConoDBUser dbUser)
		{
            this.dbUser = dbUser;
		}

		public abstract void Process(IConoDBConnection dbConnection);
	}
}


