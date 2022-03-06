using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Infrastructure
{
    [DebuggerDisplay("{NumberOpreation} : {Name}")]
    public class Operation
    {
        public Operation(string name, AccessRole? role, bool active, int number, UserLoginState? state, Action<Int32> operation)
        {
            Name = name;
            AvailableFor = role;
            Active = active;
            NumberOpreation = number;
            StateLogin = state;
            DoOperation = operation;
        }
        public string Name { get; set; }
        public AccessRole? AvailableFor { get; set; }
        public bool Active { get; set; }
        public int NumberOpreation { get; set; }
        public UserLoginState? StateLogin { get; set; }

        public Action<Int32> DoOperation;
    }
}
