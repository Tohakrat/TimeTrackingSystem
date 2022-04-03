using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using Infrastructure;

namespace Business
{
    internal class ProjectData
    {
        internal Project Project;
        
        internal ProjectData(int id, string name, DateTime expirationDate, int maxHours, int leaderUserId)
        {           
            Project = new Project(id, name, expirationDate, maxHours, leaderUserId);
        }
        internal ProjectData(Project project)
        {
            Project = project;
        }
        
        internal ProjectData BuildprojectData(Func<string, AccessRole, int> GetProjectLeaderId, Func<int> MaxId)
        {
            String ProjectName = DataFacade.Instance.Delegates.RequestDelegate("Enter project name: ");
            String DateString = DataFacade.Instance.Delegates.RequestDelegate("Enter user expiration date: ");
            DateTime Date;
            if (!DateTime.TryParse(DateString, out Date))
            {
                DataFacade.Instance.Delegates.MessageDelegate("wrong Date");
                return null;
            }
            int MaxHours = 0;
            if (!int.TryParse(DataFacade.Instance.Delegates.RequestDelegate("Enter max hours: "), out MaxHours))
            {
                DataFacade.Instance.Delegates.MessageDelegate("wrong value");
                return null;
            }
            String ProjectLeaderName = DataFacade.Instance.Delegates.RequestDelegate("Enter Ppoject leader Name: ");
            int ProjectLeaderId;
            try
            {
                ProjectLeaderId = GetProjectLeaderId(ProjectLeaderName, AccessRole.ProjectLeader);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.Instance.Delegates.MessageDelegate("wrong Leader Name");
                return null;
            }
            int MaxProjectId = MaxId();
            return new ProjectData(MaxProjectId, ProjectName, Date, MaxHours, ProjectLeaderId);         

        }
        internal int GetProjId()
        {
            return Project.Id;
        }
        internal string GetName()
        {
            return Project.Name;
        }
        internal string GetDataString(Func<int,string> GetLeaderIdByName)
        {
            StringBuilder Result = new StringBuilder();
            Result.Append(System.Environment.NewLine);
            Result.Append(Project.Name);
            Result.Append(" Exp Date: ");
            Result.Append(Project.ExpirationDate.Date.ToShortDateString());
            Result.Append(" Max hours: ");
            Result.Append(Project.MaxHours);
            Result.Append(" Project Leader: ");
            Result.Append(GetLeaderIdByName(Project.LeaderUserId));
            return Result.ToString();
        }
        internal int GetLeaderId()
        {
            return Project.Id;
        }
    }
}
