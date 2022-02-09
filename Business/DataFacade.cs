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
        internal void LogOut(User user)
        {
            UserServicesObj.LogOut(user, ChangeUserDelegate, MessageDelegate);
        }
        internal void GetProjects()
        {
            MessageDelegate(ProjectServicesObj.GetProjectsString());
        }
        internal void SubmitTime(User user)
        {
            string project = RequestDelegate("EnterProjectName:");
            int IdProject = ProjectServicesObj.FindIdByName(project);
            int HoursCount;
            if (int.TryParse(RequestDelegate("Enter Count of Hours:"),out HoursCount)==false)
                 MessageDelegate("Hours is incorrect:");
            DateTime DateOfWork;
            if (DateTime.TryParse(RequestDelegate("Enter Date:"), out DateOfWork) == false)
                MessageDelegate("Date is incorrect:");

            if (UserServicesObj.SubmitTime(user, IdProject, HoursCount,DateOfWork)==true)
                MessageDelegate("Successfully added time");
            else MessageDelegate("Error adding TimeEntry");
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
