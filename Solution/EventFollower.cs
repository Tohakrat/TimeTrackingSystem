using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    internal class EventFollower
    {
        Mediator MediatorObj;
        EventFollower(Mediator mediatorObj)
        {
            MediatorObj = mediatorObj;
        }
        private void FollowToUser(Object sender, UserStringEventArgs e)
        {

            Console.WriteLine(" EventFollower:"+e.Info);
        }
    }
}
