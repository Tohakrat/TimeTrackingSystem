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
        public ProjectServices()
        {
            
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
        private void Seed()
        {            
            ProjectRepository.Add(new Project(Project", "1234", AccessRole.User));
            ProjectRepository.Add(new Project("Project", "1234", AccessRole.User));
            ProjectRepository.Add(new Project("Project", "1234", AccessRole.Admin));
            ProjectRepository.Add(new Project("Project", "1234", AccessRole.ProjectLeader));
        }
    }
}
