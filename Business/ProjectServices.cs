using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Business
{
    public class ProjectServices
    {
        public event Notify ProjectListTransmitted;
        public ProjectServices()
        {
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
        public void GetProjectsString()
        {
            StringBuilder Result=new();
            Result.AppendLine();
            Result.AppendLine("Available operations");

            foreach (Project Proj in ProjectRepository)
            {
                Result.Append(System.Environment.NewLine);
                Result.Append(Proj.Name);
                Result.Append(" ");
                Result.Append(Proj.ExpirationDate);
                Result.Append(" ");
                Result.Append(Proj.MaxHours);
            }
            ProjectListTransmitted?.Invoke(Result.ToString());
            //return Result.ToString();
        }
        private void Seed()
        {            
            ProjectRepository.Add(new Project("TimeTrackingSystem", DateTime.Now, 200));
            ProjectRepository.Add(new Project("EnsuranceSystem", DateTime.Now, 400));
            ProjectRepository.Add(new Project("GamblingSystem", DateTime.Now, 750));
            ProjectRepository.Add(new Project("EnergySystem", DateTime.Now,470));
        }
    }
    public delegate void Notify(String N);
}
