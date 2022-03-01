using System;
using System.Collections.Generic;
using DataContracts;
using System.Text;
using System.Linq;

namespace Business
{   
    public class UserData
    {     
        private static DataFacade Facade;
        public UserData(int id, string username, string password, AccessRole role, string fullName = null )
        {   
            UserObj = new User( id,  username,  password,  role,  fullName); 
        }
        public UserData(User user)
        {
            UserObj = user;
        }
        internal static void SetFacade(DataFacade facade)
        {
            Facade = facade;
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
        internal string GetName()
        {
            return UserObj.UserName;
        }
        internal int GetId()
        {
            return UserObj.Id;
        }
        internal AccessRole GetAccessRole()
        {
            return UserObj.Role;
        }

        internal string GetFullName()
        {
            return UserObj.FullName;
        }
    }
    
    
}
