using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using System.Runtime.CompilerServices;



namespace Business
{
    internal delegate void OperationDelegate(UserData? UD=null);
    public sealed class DataFacade
    {
        internal Menu MenuObj; 
        internal DataFacadeDelegates Delegates = new DataFacadeDelegates();
        public UserServices UserServicesObj;
        public ProjectServices ProjectServicesObj;

        internal void PopulateData()
        {
            StubDataPopulater Populater = new StubDataPopulater(UserServicesObj, ProjectServicesObj);
            Populater.Populate();
        }

        private static DataFacade DataFacadeObj;

        //public DataFacade(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        //{
        //    Delegates.RequestDelegate = Request;
        //    Delegates.MessageDelegate = Message;
        //    Delegates.ChangeUserDelegate = SetUser;
        //    UserServicesObj = new UserServices(this);
        //    ProjectServicesObj = new ProjectServices(this);
        //    UserServicesObj.SetProjectServices(ProjectServicesObj);
        //    ProjectServicesObj.SetUserServices(UserServicesObj);
        //    MenuObj = new(this);
        //}
        private DataFacade()
        {
        }
        public static DataFacade GetDataFacade()
        {
            if (DataFacadeObj == null)
                DataFacadeObj = new DataFacade();
            return DataFacadeObj;
        }
        internal void Initialize(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        {
            
            Delegates.RequestDelegate = Request;
            Delegates.MessageDelegate = Message;
            Delegates.ChangeUserDelegate = SetUser;
            UserServicesObj = new UserServices(this);
            ProjectServicesObj = new ProjectServices(this);
            UserServicesObj.SetProjectServices(ProjectServicesObj);
            ProjectServicesObj.SetUserServices(UserServicesObj);
            
            MenuObj = new(this);
        }

            internal void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        {
            Delegates.RequestDelegate = Request;
            Delegates.MessageDelegate = Message;
            Delegates.ChangeUserDelegate = SetUser;
        }
        internal bool ProcessAnswer(int answer, UserData user)
        {
            if (user == null)
            { return MenuObj.ProcessAnswer(answer,AccessRole.Any, State.NotLogined,user); }
            else return MenuObj.ProcessAnswer(answer,user.UserObj.Role, State.Logined,user);            
        }       

        internal void Login(UserData user)
        {
            if (user == null)
            {
                string UserName, Password;
                UserName = Delegates.RequestDelegate("Enter login:");
                Password = Delegates.RequestDelegate("Enter password:");
                UserData TempUser = user;
                bool Logined = UserServicesObj.LogIn(UserName, Password, out TempUser);
                Delegates.ChangeUserDelegate(TempUser);
            }
            else Delegates.MessageDelegate("You are already logined, please log out!");
        }

        

        internal void LogOut(UserData user)
        {
            UserServicesObj.LogOut(user, Delegates.ChangeUserDelegate);
        }        

        internal void SubmitTime(UserData user)
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

        internal void ReportActiveUsers(UserData user=null)
        {
            Delegates.MessageDelegate(UserServicesObj.ReportActiveUsers(ProjectServicesObj.FindIdByName));
        }

        internal void AddUser(UserData user = null)
        {
            bool Result = UserServicesObj.Add();
        }
        internal void DeleteUser(UserData Me)
        {
            bool deleted = UserServicesObj.DeleteUser(Me);            
            
        }
        internal void AddProject(UserData user = null)
        {
            bool result = ProjectServicesObj.AddProject(GetUserIdByName);
        }
        internal void DeleteProject(UserData user = null)
        {
            if (ProjectServicesObj.DeleteProject(null))
                Delegates.MessageDelegate("Deleted successfully");
            else Delegates.MessageDelegate("Error. Project hadnt been deleted!");
        }          
        //internal viod AddProjectObj()
       
        internal int GetUserIdByName(string UserName,AccessRole role)
        {
            return UserServicesObj.GetUserIdByName(UserName,role);
        }
        public void ViewSubmittedTime(UserData user)
        {
            Delegates.MessageDelegate(UserServicesObj.ViewSubmittedTime(user,ProjectServicesObj.FindNameById));
        }
                
        public List<Project> GetProjectsOfUser(UserData user)
        {
            return null;
        }
        public void GetProjectsString(UserData user = null)
        {
            Delegates.MessageDelegate(ProjectServicesObj.GetProjectsString());
        }

        public string GetOperations(UserData user)
        {
            if (user == null)
            { return MenuObj.GetAvailableOperations(AccessRole.Any, State.NotLogined); }
            else return MenuObj.GetAvailableOperations(user.UserObj.Role, State.Logined);
        }
        public void ViewAllActiveUsers(UserData user = null)
        {
            Delegates.MessageDelegate(UserServicesObj.GetAllActiveUsers());
        }
        public void ViewAllUsers(UserData user = null)
        {
            Delegates.MessageDelegate(UserServicesObj.GetAllUsersString());
        }
        public void Quit(UserData user = null)
        {
            Environment.Exit(0);
        }        
              

    }
    
}
