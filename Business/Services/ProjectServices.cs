using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using Infrastructure;

namespace Business
{
    public class ProjectServices
    {
        private List<ProjectData> ProjectDataRepository = new();       
        //private UserServices UsServices;
        public delegate void Notify(String N);
        public event EventHandler<UserEventArgs> UserAdded;
        public event EventHandler<UserEventArgs> UserDeleted;

        internal ProjectServices()
        {            
        }
        //internal void SetUserServices()
        //{
        //    //UsServices = US;
        //}
        
        internal void AddProject(Int32 us)
        {
            String ProjectName = DataFacade.Instance.Delegates.RequestDelegate("Enter project name: ");
            String DateString = DataFacade.Instance.Delegates.RequestDelegate("Enter project expiration date: ");
            DateTime Date;
            if (!DateTime.TryParse(DateString, out Date))
            {
                DataFacade.Instance.Delegates.MessageDelegate("wrong Date");
                return;
            }
            int MaxHours = 0;
            if (!int.TryParse(DataFacade.Instance.Delegates.RequestDelegate("Enter max hours: "), out MaxHours))
            {
                DataFacade.Instance.Delegates.MessageDelegate("wrong value");
                return;
            }
            String ProjectLeaderName = DataFacade.Instance.Delegates.RequestDelegate("Enter Ppoject leader Name: ");
            int ProjectLeaderId;
            try
            {
                ProjectLeaderId = DataFacade.Instance.UserServices.GetUserIdByName(ProjectLeaderName,AccessRole.ProjectLeader);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.Instance.Delegates.MessageDelegate("wrong Leader Name");
                return;
            }
            int MaxProjectId = GetMaxProjectId();            
            ProjectDataRepository.Add(new ProjectData(MaxProjectId, ProjectName, Date, MaxHours, ProjectLeaderId));
            DataFacade.Instance.Delegates.MessageDelegate("Project added successfully");
            return;

        }
        internal void DeleteProject(Int32 userId)
        {                      
            string name = DataFacade.Instance.Delegates.RequestDelegate(" Enter project name: ");
            
            foreach (ProjectData project in ProjectDataRepository)
            {
                if (project.GetName() == name)
                {
                    if (ProjectDataRepository.Remove(project))
                        DataFacade.Instance.Delegates.MessageDelegate("Deleted successfully");
                    else DataFacade.Instance.Delegates.MessageDelegate("Unknown error.");
                    return ;
                }
            }
            return ;
        }
        
        internal void AddObject(Project proj)
        {
            ProjectDataRepository.Add(new ProjectData(proj));
        }
        internal int GetMaxProjectId()
        {
            int MaxId = 0;
            foreach (ProjectData p in ProjectDataRepository)
            {
                if (p.GetProjId()>MaxId)
                {
                    MaxId = p.GetProjId();
                }
            }
            return MaxId;
        }
        public void GetProjectsString(Int32 Id)
        {
            StringBuilder Result=new();
            Result.AppendLine();
            Result.AppendLine("All projects: ");

            foreach (ProjectData Proj in ProjectDataRepository)
            {
                Result.Append(Proj.GetDataString(FindNameById));   
            }
            DataFacade.Instance.Delegates.MessageDelegate(Result.ToString());
            return;
        }
        internal int FindIdByName(String project)
        {
            int Id = (from ProjectRepositoryItem in ProjectDataRepository where ProjectRepositoryItem.GetName() == project select ProjectRepositoryItem.GetProjId()).FirstOrDefault();
            return Id;
        }

        internal string FindNameById(int id)
        {
            string Name = (from ProjectRepositoryItem in ProjectDataRepository where ProjectRepositoryItem.GetProjId() == id select ProjectRepositoryItem.GetName()).FirstOrDefault();
            if (Name != null)
                return Name;
            else return "project not found"; 
        }
        internal bool IsUserResponsible(int id)
        {
            foreach(ProjectData project in ProjectDataRepository)
            {
                if (project.GetLeaderId() == id)
                    return true;
            }
            return false;
        }
        public void AddRange(IEnumerable<Project> Range)
        {
            foreach (Project ProjectCurrent in Range)
            {
                ProjectDataRepository.Add(new ProjectData(ProjectCurrent));
            }
        }

    }
    
}
