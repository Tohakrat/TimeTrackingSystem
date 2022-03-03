﻿using System;
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
        private static DataFacade _s_DataFacadeObj;

        internal void PopulateData()
        {
            StubDataPopulater Populater = new StubDataPopulater(UserServicesObj, ProjectServicesObj);
            Populater.Populate();
        }      
 
        private DataFacade()
        {
            UserServicesObj = new UserServices();// this);
            ProjectServicesObj = new ProjectServices();// this);
            UserServicesObj.SetProjectServices(ProjectServicesObj);
            ProjectServicesObj.SetUserServices(UserServicesObj); 
            

            MenuObj = new(this);
        }
        public static DataFacade GetDataFacade()
        {
            if (_s_DataFacadeObj == null)
                _s_DataFacadeObj = new DataFacade();
            return _s_DataFacadeObj;
        }
        //internal void Initialize(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        //internal void Initialize(Func<String, String> Request, Action<String> Message, Action<Int32> SetUser)

        //{
        //    Delegates.RequestDelegate = Request;
        //    Delegates.MessageDelegate = Message;
        //    Delegates.ChangeUserDelegate = SetUser;
        //}

        internal void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<Int32> SetUser)
        {
            Delegates.RequestDelegate = Request;
            Delegates.MessageDelegate = Message;
            Delegates.ChangeUserDelegate = SetUser;
            UserServicesObj.SetEventsDelegates();
        }
        internal bool ProcessAnswer(int answer, int user)
        {
            return MenuObj.ProcessAnswer(answer, user);                  
        }       

        internal void Login(Int32 user)
        {
            UserServicesObj.Login(ref user);            
        }       

        internal void LogOut(Int32 user)
        {
            UserServicesObj.LogOut(user);//, Delegates.ChangeUserDelegate);
        }        

        internal void SubmitTime(Int32 user)
        {
            UserServicesObj.SubmitTime(user);            
        }       
        internal void ReportActiveUsers(Int32 user )
        {
            Delegates.MessageDelegate(UserServicesObj.ReportActiveUsers(ProjectServicesObj.FindIdByName));
        }

        internal void AddUser(Int32 user )
        {
            bool Result = UserServicesObj.Add();
        }

        internal void DeleteUser(Int32 UserId)
        {
            bool deleted = UserServicesObj.DeleteUser(UserId);         
        }

        internal void AddProject(Int32 user )
        {
            bool result = ProjectServicesObj.AddProject(GetUserIdByName);
        }
        internal void DeleteProject(Int32 user)
        {
            ProjectServicesObj.DeleteProject(null);            
        }          
       
        internal int GetUserIdByName(string UserName,AccessRole role)
        {
            return UserServicesObj.GetUserIdByName(UserName,role);
        }
        public void ViewSubmittedTime(Int32 UserId)
        {
            Delegates.MessageDelegate(UserServicesObj.ViewSubmittedTime(UserId));//, ProjectServicesObj.FindNameById);;);
        }
                
        public List<Project> GetProjectsOfUser(Int32 user)
        {
            return null;
        }
        public void GetProjectsString(Int32 user)
        {
            Delegates.MessageDelegate(ProjectServicesObj.GetProjectsString());
        }

        public string GetOperations(Int32 userId)
        {
            return MenuObj.GetAvailableOperations(userId); 
        }
        public void ViewAllActiveUsers(Int32 user )
        {
            Delegates.MessageDelegate(UserServicesObj.GetAllActiveUsers());
        }
        public void ViewAllUsers(Int32 user )
        {
            Delegates.MessageDelegate(UserServicesObj.GetAllUsersString());
        }
        public void Quit(Int32 user )
        {
            Environment.Exit(0);
        }                     
    }
}
