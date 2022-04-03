using System;
using System.Collections.Generic;
using DataContracts;
using System.Text;
using System.Linq;
using Infrastructure;

namespace Business
{   
    public class UserData
    {
        
        public UserData(User user)
        {
            User = user;
        }        
        internal User User;
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
            if ((LogIn==User.UserName)&&(PassWord==User.PassWord))
                return true;
            else return false;
        }
        internal void SetActive()
        {
            User.IsActive = true;            
        }
        internal void SetNotActive()
        {
            User.IsActive = false;
        }
        internal string GetName()
        {
            return User.UserName;
        }
        internal int GetId()
        {
            return User.Id;
        }
        internal AccessRole GetAccessRole()
        {
            return User.Role;
        }
        internal string GetFullName()
        {
            return User.FullName;
        }
    }    
    
}
