using System;
using System.Collections.Generic;
using DataContracts;

namespace Business
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }
    }
    public class DataFacade
    {
        public DataFacade()
        {
            Seed();
        }
        //UserServices UserServices { get; set; }
        UserServices UserServicesObj = new UserServices();
        ProjectServices ProjectServicesObj = new ProjectServices();
        TimeTrackEntryServices TimeTrackEntryServicesObj = new TimeTrackEntryServices();
        private void Seed()
        {
            UserServicesObj.Add(new User("Vasia", "1234", AccessRole.User));
            UserServicesObj.Add(new User("Petia", "1234", AccessRole.User));
            UserServicesObj.Add(new User("Vlad", "1234", AccessRole.Admin));
            UserServicesObj.Add(new User("Ivan", "1234", AccessRole.ProjectLeader));
             
        }
    }
    public class UserServices
    {
        private List<User> UserRepository = new();
        private List<UserData> UserData = new List<UserData>(); 
        public List<User> GetAllUsers()
        {
            return UserRepository;
        }
        public List<User> GetAllActiveUsers()
        {
            List<User> ActiveUsers = new();
            foreach(User U in UserRepository)
            {
                if (U.IsActive)
                {
                    ActiveUsers.Add(U);
                }
            }
            return ActiveUsers;
        }
        public void Add(User user)
        {
            UserRepository.Add(user);
            UserData.Add(new UserData(user));
        }
        
    }
    
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
   
    public class UserData
    {
        public UserData(User user)
        {
            UserObj = user; 
        }
        User UserObj;
        List<TimeTrackEntry> SybmittedTime = new();
        event StateChanged IsActiveChanged;

    }
    delegate void StateChanged(int UserId, int ProjectId, string message);
    //event AccountHandler Notify;

}
