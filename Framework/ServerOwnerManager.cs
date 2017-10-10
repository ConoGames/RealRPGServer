using System;
using System.Collections.Concurrent;

namespace FrameworkNamespace
{
    public class ServerOwnerManager : OwnerManager
    {
        protected ConcurrentDictionary<long, ServerOwner> ownerDict = new ConcurrentDictionary<long, ServerOwner>();

        public ServerOwnerManager()
        {
            
        }

        public ServerOwner GetOwner(long ownerNo)
        {
            ServerOwner owner = ownerDict[ownerNo];

            if (owner != null)
            {
                return owner;
            }

            return null;
        }

        public ServerOwner GetFreeOwner()
        {
            if(ownerDict.Count == 0)
            {
                return null;
            }

            var e = ownerDict.GetEnumerator();
            e.MoveNext();
			
            var anElement = e.Current;

            return anElement.Value;
        }

        //public bool AddServerOwner(ServerOwner owner)
        //{
        //    if (ownerDict.TryAdd(owner.OwnerNo, owner) == false)
        //    {
        //        Console.WriteLine("already exist ownerNo - " + owner.OwnerNo);

        //        return false;
        //    }

        //    return true;
        //}
        
        //public void RemoveServerOwner(ServerOwner owner)
        //{
        //    if (ownerDict.TryRemove(owner.OwnerNo, out owner) == false)
        //    {
        //        Console.WriteLine("not exist in ownerDict - ownerDict : " + owner.OwnerNo);

        //        return;
        //    }
        //}

        public bool AddOwner(Owner owner)
        {
            ServerOwner serverOwner = owner as ServerOwner;

            if (ownerDict.TryAdd(serverOwner.OwnerNo, serverOwner) == false)
            {
                Console.WriteLine("already exist ownerNo - " + serverOwner.OwnerNo);

                return false;
            }

            return true;
        }

        public void RemoveOwner(Owner owner)
        {
            ServerOwner serverOwner = owner as ServerOwner;

            if (ownerDict.TryRemove(serverOwner.OwnerNo, out serverOwner) == false)
            {
                Console.WriteLine("not exist in ownerDict - ownerDict : " + serverOwner.OwnerNo);

                return;
            }
        }
    }
}
