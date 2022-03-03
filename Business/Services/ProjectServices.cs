using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
//using System.Linq;

namespace Business
{
    public class ProjectServices
    {
        private List<ProjectData> ProjectDataRepository = new();        
        //private DataFacade Facade;
        private UserServices UsServices;

        internal ProjectServices()//DataFacade facade)
        {
            //Facade = facade;
            //ProjectData.SetFacade(Facade);            
        }
        internal void SetUserServices(UserServices US)
        {
            UsServices = US;
        }
        
        internal void AddProject(Int32 us)//Func<string,AccessRole, int> GetProjectLeaderId)
        {
            String ProjectName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter project name: ");
            String DateString = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter project expiration date: ");
            DateTime Date;
            if (!DateTime.TryParse(DateString, out Date))
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong Date");
                return;// false;
            }
            int MaxHours = 0;
            if (!int.TryParse(DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter max hours: "), out MaxHours))
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong value");
                return;// false;
            }
            String ProjectLeaderName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter Ppoject leader Name: ");
            int ProjectLeaderId;
            try
            {
                ProjectLeaderId = DataFacade.GetDataFacade().GetUserIdByName(ProjectLeaderName,AccessRole.ProjectLeader);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("wrong Leader Name");
                return;// false;
            }
            int MaxProjectId = GetMaxProjectId();            
            ProjectDataRepository.Add(new ProjectData(MaxProjectId, ProjectName, Date, MaxHours, ProjectLeaderId));
            DataFacade.GetDataFacade().Delegates.MessageDelegate("Project added successfully");
            return;// true;

        }
        internal void DeleteProject(Int32 userId)
        {          
            //if (name == null)
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
        //internal void AddObject(ProjectData projData)
        //{
        //    ProjectDataRepository.Add(projData);
        //}
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
            return;// Result.ToString();
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
    public delegate void Notify(String N);
}
