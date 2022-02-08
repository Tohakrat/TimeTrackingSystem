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
        private Func<String,String> RequestDelegate;
        private Action<String> MessageDelegate;
        private Action<User> ChangeUserDelegate;
        
        public DataFacade()
        {
            //Seed();            
        }
        //UserServices UserServices { get; set; }
        
        internal Menu MenuObj = new();
        public UserServices UserServicesObj = new UserServices();
        public ProjectServices ProjectServicesObj = new ProjectServices();
        //TimeTrackEntryServices TimeTrackEntryServicesObj = new TimeTrackEntryServices();
        internal void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<User> SetUser)
        {
            RequestDelegate = Request;
            MessageDelegate = Message;
            ChangeUserDelegate = SetUser;
        }
        internal void Login(User user)
        {
            if (user == null)
            {
                string UserName, Password;
                UserName = RequestDelegate("Enter login:");
                Password = RequestDelegate("Enter password:");
                User TempUser = user;
                bool Logined = UserServicesObj.LogIn(UserName, Password, out TempUser);
                ChangeUserDelegate(TempUser);
            }
            else MessageDelegate("You are already logined, please log out!");
        }



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
