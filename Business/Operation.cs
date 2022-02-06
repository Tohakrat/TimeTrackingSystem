using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    internal class Operation
    {
        internal Operation(string name, DataContracts.AccessRole role, bool active, int number, State state)
        {
            Name = name;
            AvailableFor = role;
            Active = active;
            NumberOpreation = number;
            StateLogin = state;
        }

        internal string Name { get; set; }
        internal DataContracts.AccessRole  AvailableFor { get; set; }
        internal bool Active { get; set; }
        internal int NumberOpreation { get; set; }
        internal State StateLogin { get; set; }        
    }
    

}
