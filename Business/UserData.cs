using System;
using System.Collections.Generic;
using DataContracts;
using System.Text;

namespace Business
{   
    public class UserData
    {
        public UserData(User user)
        {
            UserObj = user; 
        }
        public User UserObj;
        public List<TimeTrackEntry> SybmittedTime = new();
        event StateChanged IsActiveChanged;
        public string ShowTimeTrackString()
        {
            StringBuilder TimeString = new();
            foreach (TimeTrackEntry Time in SybmittedTime )
            {
                TimeString.AppendLine(Time.UserId + " " + Time.ProjectId.ToString()+" "+Time.Value.ToString());
            }
            return TimeString.ToString();
        }
    }
    delegate void StateChanged(int UserId, int ProjectId, string message);
    //event AccountHandler Notify;
}
