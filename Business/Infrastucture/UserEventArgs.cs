using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Infrastructure
{
    public class UserEventArgs:EventArgs
    {
        public UserEventArgs(User U)
        {
            UserObj = U;
        }
        public User UserObj { get; }
    }
}
