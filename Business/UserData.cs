using System;
using System.Collections.Generic;
using DataContracts;
using System.Text;
using System.Linq;

namespace Business
{   
    public class UserData
    {
        public event Action<string> UserLogined;
        public event Action<string> LoginFailed;
        private DataFacade Facade;
        public UserData(User user, DataFacade facade)
        {
            Facade = facade;
            UserObj = user; 
        }
        internal User UserObj;
        private List<TimeTrackEntry> SybmittedTimeList = new();
        
        internal string GetTimeTrackString(Func<int,string> GetProjectNameById)
        {
            StringBuilder TimeString = new();
            foreach (TimeTrackEntry Time in SybmittedTimeList )
            {
                TimeString.AppendLine("ProjectName: " + GetProjectNameById(Time.ProjectId)+"  Submitted time:"+Time.Value.ToString() +"  Time submitting:" + Time.Date.ToString());
            }
            return TimeString.ToString();
        }
        internal void AddSubmittedTime(TimeTrackEntry TTEntry )
        {
            SybmittedTimeList.Add(TTEntry);              
        }
        internal int GetSubmittedHoursByProjectId(int projectId)
        {
            return (from SubmitItem in SybmittedTimeList where SubmitItem.ProjectId == projectId select SubmitItem.Value).Sum();            
        }
        internal bool CheckCredentials(string LogIn, string PassWord)
        {
            if ((LogIn==UserObj.UserName)&&(PassWord==UserObj.PassWord))
                return true;
            else return false;
        }

        internal void SetActive()
        {
            UserObj.IsActive = true;            
        }
        internal void SetNotActive()
        {
            UserObj.IsActive = false;
        }
    }
    
    
}
