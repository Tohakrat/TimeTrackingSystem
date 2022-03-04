using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Operation
    {
        public Operation(string name, AccessRole role, bool active, int number, State state, Action<Int32> operation)
        {
            Name = name;
            AvailableFor = role;
            Active = active;
            NumberOpreation = number;
            StateLogin = state;
            DoOperation = operation;
        }
        public string Name { get; set; }
        public AccessRole AvailableFor { get; set; }
        public bool Active { get; set; }
        public int NumberOpreation { get; set; }
        public State StateLogin { get; set; }

        public Action<Int32> DoOperation;
    }
}
