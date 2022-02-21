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
        private DataFacade Facade;
        private UserServices UsServices;

        internal ProjectServices(DataFacade facade)
        {
            Facade = facade;            
            Seed();
        }
        internal void SetUserServices(UserServices US)
        {
            UsServices = US;
        }

        
        internal bool AddProject(Func<string,AccessRole, int> GetProjectLeaderId)
        {
            String ProjectName = Facade.Delegates.RequestDelegate("Enter project name: ");
            String DateString = Facade.Delegates.RequestDelegate("Enter user expiration date: ");
            DateTime Date;
            if (!DateTime.TryParse(DateString, out Date))
            {
                Facade.Delegates.MessageDelegate("wrong Date");
                return false;
            }
            int MaxHours = 0;
            if (!int.TryParse(Facade.Delegates.RequestDelegate("Enter max hours: "), out MaxHours))
            {
                Facade.Delegates.MessageDelegate("wrong value");
                return false;
            }
            String ProjectLeaderName = Facade.Delegates.RequestDelegate("Enter Ppoject leader Name: ");
            int ProjectLeaderId;
            try
            {
                ProjectLeaderId = GetProjectLeaderId(ProjectLeaderName,AccessRole.ProjectLeader);
            }
            catch (KeyNotFoundException e)
            {
                Facade.Delegates.MessageDelegate("wrong Leader Name");
                return false;
            }
            int MaxProjectId = GetMaxProjectId();
            ProjectDataRepository.Add(new ProjectData(Facade,MaxProjectId, ProjectName, Date, MaxHours, ProjectLeaderId));
            Facade.Delegates.MessageDelegate("Project added successfully");
            return true;

        }
        internal bool DeleteProject(String? name)
        {
            if (name == null)
                name = Facade.Delegates.RequestDelegate(" Enter project name: ");
            
            foreach (ProjectData project in ProjectDataRepository)
            {
                if (project.GetName() == name)
                {
                    ProjectDataRepository.Remove(project);
                    return true;
                }
            }
            return false;
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
        public string GetProjectsString()
        {
            StringBuilder Result=new();
            Result.AppendLine();
            Result.AppendLine("All projects: ");

            foreach (ProjectData Proj in ProjectDataRepository)
            {
                Result.Append(Proj.GetDataString(FindNameById));   
            }            
            return Result.ToString();
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
        private void Seed()
        {            
            ProjectDataRepository.Add(new ProjectData(Facade, 0,"TimeTrackingSystem", DateTime.Now, 200,6));
            ProjectDataRepository.Add(new ProjectData(Facade, 1,"EnsuranceSystem", DateTime.Now, 400, 7));
            ProjectDataRepository.Add(new ProjectData(Facade, 2,"GamblingSystem", DateTime.Now, 750, 6));
            ProjectDataRepository.Add(new ProjectData(Facade, 3,"EnergySystem", DateTime.Now,470, 7));
            ProjectDataRepository.Add(new ProjectData(Facade, 4,"UniversitySystem", DateTime.Now,900, 6));
            ProjectDataRepository.Add(new ProjectData(Facade, 5,"p5", DateTime.Now,220, 7));
            ProjectDataRepository.Add(new ProjectData(Facade, 6,"p6", DateTime.Now,420, 6));
            ProjectDataRepository.Add(new ProjectData(Facade, 7,"p7", DateTime.Now,620, 7));

        }        
       
    }
    public delegate void Notify(String N);
}
