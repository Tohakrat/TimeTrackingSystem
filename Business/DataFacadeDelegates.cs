using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Business
{
    internal class DataFacadeDelegates
    {
        internal Func<String, String> RequestDelegate { get; set; }
        internal Action<String> MessageDelegate { get; set; }
        internal Action<User> ChangeUserDelegate { get; set; }
    }
}
