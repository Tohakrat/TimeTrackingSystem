using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
   
    public class UserEventArgs : EventArgs
    {
        public UserEventArgs(string u)
        {
            Info = u;
        }
        public string Info { get; set; }
    }
}
