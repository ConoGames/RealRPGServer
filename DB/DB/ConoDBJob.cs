namespace ConoDBLibrary
{
	public abstract class ConoDBJob
	{
		internal IConoDBUser user;

		public ConoDBJob(IConoDBUser user)
		{
            this.user = user;
		}

		public abstract void Process(IConoDBConnection dbConnection);
	}
}


