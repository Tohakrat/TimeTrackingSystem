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
        private UserServices UsServices;
        public delegate void Notify(String N);

        internal ProjectServices()
        {            
        }
        internal void SetUserServices(UserServices US)
        {
            UsServices = US;
        }
        
        internal void AddProject(Int32 us)
        {
            String ProjectName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter project name: ");
            String DateString = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter project expiration date: ");
            DateTime Date;
            if (!DateTime.TryParse(DateString, out Date))
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong Date");
                return;
            }
            int MaxHours = 0;
            if (!int.TryParse(DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter max hours: "), out MaxHours))
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong value");
                return;
            }
            String ProjectLeaderName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter Ppoject leader Name: ");
            int ProjectLeaderId;
            try
            {
                ProjectLeaderId = DataFacade.GetDataFacade().UserServicesObj.GetUserIdByName(ProjectLeaderName,AccessRole.ProjectLeader);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong Leader Name");
                return;
            }
            int MaxProjectId = GetMaxProjectId();            
            ProjectDataRepository.Add(new ProjectData(MaxProjectId, ProjectName, Date, MaxHours, ProjectLeaderId));
            DataFacade.GetDataFacade().Delegates.MessageDelegate("Project added successfully");
            return;

        }
        internal void DeleteProject(Int32 userId)
        {                      
            string name = DataFacade.GetDataFacade().Delegates.RequestDelegate(" Enter project name: ");
            
            foreach (ProjectData project in ProjectDataRepository)
            {
                if (project.GetName() == name)
                {
                    if (ProjectDataRepository.Remove(project))
                        DataFacade.GetDataFacade().Delegates.MessageDelegate("Deleted successfully");
                    else DataFacade.GetDataFacade().Delegates.MessageDelegate("Unknown error.");
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
            DataFacade.GetDataFacade().Delegates.MessageDelegate(Result.ToString());
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
    }
    
}
