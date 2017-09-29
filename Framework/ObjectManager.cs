using System;
namespace FrameworkNamespace
{
    public class ObjectManager
    {
        protected OwnerManager[] ownerManagers;
        private long ownerNo;

        public long OwnerNo
        {
            get 
            { 
                return ownerNo; 
            }
        }

        public ObjectManager()
        {
            ownerNo = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public OwnerManager GetOwnerManager(int moduleType)
        {
            return ownerManagers[moduleType];
        }
    }
}
