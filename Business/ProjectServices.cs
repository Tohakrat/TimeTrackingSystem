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
        //public event Notify ProjectListTransmitted;
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
        internal bool AddProject()
        {











            throw new NotImplementedException();
        }
        public string GetProjectsString()
        {
            StringBuilder Result=new();
            Result.AppendLine();
            Result.AppendLine("All projects: ");

            foreach (Project Proj in ProjectRepository)
            {
                Result.Append(System.Environment.NewLine);
                Result.Append(Proj.Name);
                Result.Append(" ");
                Result.Append(Proj.ExpirationDate);
                Result.Append(" ");
                Result.Append(Proj.MaxHours);
            }
            //ProjectListTransmitted?.Invoke(Result.ToString());
            return Result.ToString();
        }
        internal int FindIdByName(String project)
        {
            int Id = (from ProjectRepositoryItem in ProjectRepository where ProjectRepositoryItem.Name == project select ProjectRepositoryItem.Id).FirstOrDefault();
            return Id;

        }
        private void Seed()
        {            
            ProjectRepository.Add(new Project(1,"TimeTrackingSystem", DateTime.Now, 200));
            ProjectRepository.Add(new Project(2,"EnsuranceSystem", DateTime.Now, 400));
            ProjectRepository.Add(new Project(3,"GamblingSystem", DateTime.Now, 750));
            ProjectRepository.Add(new Project(4,"EnergySystem", DateTime.Now,470));
            ProjectRepository.Add(new Project(5,"UniversitySystem", DateTime.Now,900));
            ProjectRepository.Add(new Project(6,"p6", DateTime.Now,220));
            ProjectRepository.Add(new Project(7,"p7", DateTime.Now,420));
            ProjectRepository.Add(new Project(8,"p8", DateTime.Now,620));

        }

        
    }
    public delegate void Notify(String N);
}
