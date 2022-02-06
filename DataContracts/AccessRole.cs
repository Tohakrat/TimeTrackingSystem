using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Business")]

namespace DataContracts
{ 
  
    public enum AccessRole
    {
        Any,
        User,
        Admin,
        ProjectLeader
    }     


}
