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
        UserServices UserServices { get; set; }
        UserServices UserServicesObj = new UserServices();
        ProjectServices ProjectServicesObj = new ProjectServices();
        TimeTrackEntryServices TimeTrackEntryServicesObj = new TimeTrackEntryServices();
        private void Seed()
        {
            UserServices.UserRepository.Add(new User("Vasia", "1234", AccessRole.User)); 
            UserServices.UserRepository.Add(new User("Petia", "1234", AccessRole.User)); 
            UserServices.UserRepository.Add(new User("Vlad", "1234", AccessRole.Admin)); 
            UserServices.UserRepository.Add(new User("Ivan", "1234", AccessRole.ProjectLeader)); 
        }
    }
    public class UserServices
    {
        internal List<User> UserRepository = new();
        public List<User> GetAllUsers()
        {
            return null;
        }
        public List<User> GetAllActiveUsers()
        {
            return null;
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
    public class TimeTrackEntryServices
    {
        internal List<TimeTrackEntry> TimeTrackEntryRepository = new();

    }
    public class UserData
    {
        User UserObj;
        List<TimeTrackEntry> SybmittedTime;
        event StateChanged IsActiveChanged;

    }
    delegate void StateChanged(int UserId, int ProjectId, string message);
    //event AccountHandler Notify;

}
