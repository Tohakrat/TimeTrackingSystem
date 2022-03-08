using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    internal class EventCover
    {
        public event EventHandler<UserEventArgs> EventObj;
        private List<EventHandler<UserEventArgs>> ActionList = new List<EventHandler<UserEventArgs>>();
        public EventCover( EventHandler<UserEventArgs> actionObj)
        {
            EventObj = actionObj;
        }
        public void AddAction(EventHandler<UserEventArgs> action)
        {
            ActionList.Add(action);
        }
        public void DeleteAction(EventHandler<UserEventArgs> action)
        {
            ActionList.Remove(action);
        }
    }
}
