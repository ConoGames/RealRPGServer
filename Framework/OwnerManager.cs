using System;
using System.Collections.Generic;

namespace FrameworkNamespace
{
    public interface OwnerManager
    {
        bool AddOwner(Owner owner);
        void RemoveOwner(Owner owner);

    }
}
