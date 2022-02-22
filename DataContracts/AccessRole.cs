using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Business")]
[assembly: InternalsVisibleTo("Infrastructure")]


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
