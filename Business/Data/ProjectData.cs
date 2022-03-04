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
        private Project ProjectObj;
        //private static DataFacade Facade;        
        internal ProjectData(int id, string name, DateTime expirationDate, int maxHours, int leaderUserId)
        {           
            ProjectObj = new Project(id, name, expirationDate, maxHours, leaderUserId);
        }
        internal ProjectData(Project project)
        {
            ProjectObj = project;
        }
        //internal static void SetFacade()//DataFacade facade)
        //{
        //    //Facade = facade;
        //}


        internal ProjectData BuildprojectData(Func<string, AccessRole, int> GetProjectLeaderId, Func<int> MaxId)
        {
            String ProjectName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter project name: ");
            String DateString = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter user expiration date: ");
            DateTime Date;
            if (!DateTime.TryParse(DateString, out Date))
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong Date");
                return null;
            }
            int MaxHours = 0;
            if (!int.TryParse(DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter max hours: "), out MaxHours))
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong value");
                return null;
            }
            String ProjectLeaderName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter Ppoject leader Name: ");
            int ProjectLeaderId;
            try
            {
                ProjectLeaderId = GetProjectLeaderId(ProjectLeaderName, AccessRole.ProjectLeader);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong Leader Name");
                return null;
            }
            int MaxProjectId = MaxId();
            return new ProjectData(MaxProjectId, ProjectName, Date, MaxHours, ProjectLeaderId);         

        }
        internal int GetProjId()
        {
            return ProjectObj.Id;
        }
        internal string GetName()
        {
            return ProjectObj.Name;
        }
        internal string GetDataString(Func<int,string> GetLeaderIdByName)
        {
            StringBuilder Result = new StringBuilder();
            Result.Append(System.Environment.NewLine);
            Result.Append(ProjectObj.Name);
            Result.Append(" Exp Date: ");
            Result.Append(ProjectObj.ExpirationDate.Date.ToShortDateString());
            Result.Append(" Max hours: ");
            Result.Append(ProjectObj.MaxHours);
            Result.Append(" Project Leader: ");
            Result.Append(GetLeaderIdByName(ProjectObj.LeaderUserId));
            return Result.ToString();
        }
        internal int GetLeaderId()
        {
            return ProjectObj.Id;
        }
    }
}
