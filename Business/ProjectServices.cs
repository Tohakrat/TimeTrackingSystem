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
        internal List<Project> ProjectRepository = new();
        public List<Project> GetAllProjects()
        {
            return null;
        }
        public List<Project> GetProjectsOfUser(User user)
        {
            return null;
        }
    }
}
