using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Business
{
    public class DataFacade
    {
        public DataFacade()
        {
            //Seed();            
        }
        //UserServices UserServices { get; set; }
        
        internal Menu MenuObj = new();
        public UserServices UserServicesObj = new UserServices();
        public ProjectServices ProjectServicesObj = new ProjectServices();
        //TimeTrackEntryServices TimeTrackEntryServicesObj = new TimeTrackEntryServices();

        public void ViewSubmittedTime(User user)
        {
            UserServicesObj.ViewSubmittedTime(user);
        }

        public List<Project> GetAllProjects()
        {
            return ProjectServicesObj.GetAllProjects();
            
        }
        public List<Project> GetProjectsOfUser(User user)
        {
            return null;
        }
        public void GetProjectsString()
        {
            ProjectServicesObj.GetProjectsString();
        }

        public string GetOperations(User user)
        {
            if (user == null)
            { return MenuObj.GetAvailableOperations(AccessRole.Any, State.NotLogined); }
            else return MenuObj.GetAvailableOperations(user.Role, State.Logined);
        }      

    }
        
}
