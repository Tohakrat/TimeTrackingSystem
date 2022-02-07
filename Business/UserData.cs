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
        internal User UserObj;
        private List<TimeTrackEntry> SybmittedTimeList = new();
        internal event StateChanged IsActiveChanged;
        internal string GetTimeTrackString()
        {
            StringBuilder TimeString = new();
            foreach (TimeTrackEntry Time in SybmittedTimeList )
            {
                TimeString.AppendLine(Time.UserId + " " + Time.ProjectId.ToString()+" "+Time.Value.ToString());
            }
            return TimeString.ToString();
        }
    }
    delegate void StateChanged(int UserId, int ProjectId, string message);
    //event AccountHandler Notify;
}
