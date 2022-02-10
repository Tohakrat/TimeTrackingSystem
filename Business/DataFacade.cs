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
        internal Menu MenuObj = new();
        internal DataFacadeDelegates Delegates = new DataFacadeDelegates();
        public UserServices UserServicesObj;
        public ProjectServices ProjectServicesObj;
        public DataFacade()
        {            
            UserServicesObj = new UserServices(Delegates);
            ProjectServicesObj = new ProjectServices(Delegates);
        }  
        
        
        
        internal void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<User> SetUser)
        {
            Delegates.RequestDelegate = Request;
            Delegates.MessageDelegate = Message;
            Delegates.ChangeUserDelegate = SetUser;
        }


        internal bool CheckAnswer(int answer, User user)
        {
            if (user == null)
            { return MenuObj.CheckAnswer(answer,AccessRole.Any, State.NotLogined); }
            else return MenuObj.CheckAnswer(answer,user.Role, State.Logined);            
        }
       

        internal void Login(User user)
        {
            if (user == null)
            {
                string UserName, Password;
                UserName = Delegates.RequestDelegate("Enter login:");
                Password = Delegates.RequestDelegate("Enter password:");
                User TempUser = user;
                bool Logined = UserServicesObj.LogIn(UserName, Password, out TempUser);
                Delegates.ChangeUserDelegate(TempUser);
            }
            else Delegates.MessageDelegate("You are already logined, please log out!");
        }
        internal void LogOut(User user)
        {
            UserServicesObj.LogOut(user, Delegates.ChangeUserDelegate, Delegates.MessageDelegate);
        }
        internal void GetProjects()
        {
            Delegates.MessageDelegate(ProjectServicesObj.GetProjectsString());
        }
        internal void SubmitTime(User user)
        {
            string project = Delegates.RequestDelegate("EnterProjectName:");
            int IdProject = ProjectServicesObj.FindIdByName(project);
            int HoursCount;
            if (int.TryParse(Delegates.RequestDelegate("Enter Count of Hours:"),out HoursCount)==false)
                Delegates.MessageDelegate("Hours is incorrect:");
            DateTime DateOfWork;
            if (DateTime.TryParse(Delegates.RequestDelegate("Enter Date:"), out DateOfWork) == false)
                Delegates.MessageDelegate("Date is incorrect:");

            if (UserServicesObj.SubmitTime(user, IdProject, HoursCount,DateOfWork)==true)
                Delegates.MessageDelegate("Successfully added time");
            else Delegates.MessageDelegate("Error adding TimeEntry");
        }
        internal void AddUser()
        {
            bool Result = UserServicesObj.Add();
        }


        public void ViewSubmittedTime(User user)
        {
            Delegates.MessageDelegate(UserServicesObj.ViewSubmittedTime(user));
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
        public void ViewAllUsers()
        {
            Delegates.MessageDelegate(UserServicesObj.GetAllUsersString());
        }
        

    }
    
        
}
