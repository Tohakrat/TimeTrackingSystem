using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
   
    public class UserStringEventArgs : EventArgs
    {
        public UserStringEventArgs(string u)
        {
            Info = u;
        }
        public string Info { get; set; }
    }
}
