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
        
        internal string GetTimeTrackString()
        {
            StringBuilder TimeString = new();
            foreach (TimeTrackEntry Time in SybmittedTimeList )
            {
                TimeString.AppendLine("ProjectId: " + Time.ProjectId.ToString()+"  Submitted time:"+Time.Value.ToString() +"  Time submitting:" + Time.Date.ToString());
            }
            return TimeString.ToString();
        }
        internal void AddSubmittedTime(TimeTrackEntry TTEntry )
        {
            SybmittedTimeList.Add(TTEntry); 
             
        }
    }
    
    
}
