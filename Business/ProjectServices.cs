using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using System.Linq;

namespace Business
{
    public class ProjectServices
    {
        private DataFacadeDelegates Delegates;
        
        internal ProjectServices(DataFacadeDelegates delegates)
        {
            Delegates = delegates;
            Seed();
        }
        internal List<Project> ProjectRepository = new();
        public List<Project> GetAllProjects()
        {
            return null;
        }
        public List<Project> GetProjectsOfUser(User user)
        {
            return null;
        }
        internal bool AddProject(Func<string,AccessRole, int> GetProjectLeaderId)
        {
            String ProjectName = Delegates.RequestDelegate("Enter project name: ");
            String DateString = Delegates.RequestDelegate("Enter user expiration date: ");
            DateTime Date;
            if (!DateTime.TryParse(DateString, out Date))
            {
                Delegates.MessageDelegate("wrong Date");
                return false;
            }
            int MaxHours = 0;
            if (!int.TryParse(Delegates.RequestDelegate("Enter max hours: "), out MaxHours))
            {
                Delegates.MessageDelegate("wrong value");
                return false;
            }
            String ProjectLeaderName = Delegates.RequestDelegate("Enter Ppoject leader Name: ");
            int ProjectLeaderId;
            try
            {
                ProjectLeaderId = GetProjectLeaderId(ProjectLeaderName,AccessRole.ProjectLeader);
            }
            catch (KeyNotFoundException e)
            {
                Delegates.MessageDelegate("wrong Leader Name");
                return false;
            }
            int MaxProjectId = GetMaxProjectId();
            ProjectRepository.Add(new Project(MaxProjectId, ProjectName, Date, MaxHours, ProjectLeaderId));
            Delegates.MessageDelegate("Project added successfully");
            return true;

        }
        internal bool DeleteProject(String? name)
        {
            if (name == null)
                name = Delegates.RequestDelegate(" Enter project name: ");
            
            foreach (Project project in ProjectRepository)
            {
                if (project.Name == name)
                {
                    ProjectRepository.Remove(project);
                    return true;
                }
            }
            return false;
        }
        //internal int GetProjectId(String SName)
        //{
        //    foreach (Project p in ProjectRepository)
         //   {
        //        if (p.Name==SName)
        //        {
        //            return p.Id;
        //        }                
        //    }
        //    throw (new KeyNotFoundException());            
        //}
        internal int GetMaxProjectId()
        {
            int MaxId = 0;
            foreach (Project p in ProjectRepository)
            {
                if (p.Id>MaxId)
                {
                    MaxId = p.Id;
                }
            }
            return MaxId;
        }
        public string GetProjectsString(Func<int,string> GetLeaderIdByName)
        {
            StringBuilder Result=new();
            Result.AppendLine();
            Result.AppendLine("All projects: ");

            foreach (Project Proj in ProjectRepository)
            {
                Result.Append(System.Environment.NewLine);
                Result.Append(Proj.Name);
                Result.Append(" Exp Date: ");
                Result.Append(Proj.ExpirationDate.Date.ToShortDateString());
                Result.Append(" Max hours: ");
                Result.Append(Proj.MaxHours);
                Result.Append(" Project Leader: ");
                Result.Append(GetLeaderIdByName(Proj.LeaderUserId));

            }
            //ProjectListTransmitted?.Invoke(Result.ToString());
            return Result.ToString();
        }
        internal int FindIdByName(String project)
        {
            int Id = (from ProjectRepositoryItem in ProjectRepository where ProjectRepositoryItem.Name == project select ProjectRepositoryItem.Id).FirstOrDefault();
            return Id;

        }

        internal string FindNameById(int id)
        {
            string Name = (from ProjectRepositoryItem in ProjectRepository where ProjectRepositoryItem.Id == id select ProjectRepositoryItem.Name).FirstOrDefault();
            if (Name != null)
                return Name;
            else return "project not found";

        }
        private void Seed()
        {            
            ProjectRepository.Add(new Project(0,"TimeTrackingSystem", DateTime.Now, 200,6));
            ProjectRepository.Add(new Project(1,"EnsuranceSystem", DateTime.Now, 400, 7));
            ProjectRepository.Add(new Project(2,"GamblingSystem", DateTime.Now, 750, 6));
            ProjectRepository.Add(new Project(3,"EnergySystem", DateTime.Now,470, 7));
            ProjectRepository.Add(new Project(4,"UniversitySystem", DateTime.Now,900, 6));
            ProjectRepository.Add(new Project(5,"p5", DateTime.Now,220, 7));
            ProjectRepository.Add(new Project(6,"p6", DateTime.Now,420, 6));
            ProjectRepository.Add(new Project(7,"p7", DateTime.Now,620, 7));

        }

        
        //internal void DeleteProjectLeader(int projectLeaderIndex)
        //{
            
        //    foreach (Project P in ProjectRepository)
        //    {
        //        if (P.LeaderUserId == projectLeaderIndex)
        //            P.LeaderUserId = -1;
        //    }
        //}
    }
    public delegate void Notify(String N);
}
