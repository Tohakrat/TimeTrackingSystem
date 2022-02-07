using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Business
{
    public class UserServices
    {
        public event LoginChanged UserLogined;
        public event LoginChanged AdminLogined;
        public event LoginChanged ProjectLeaderLogined;
        public event LoginChanged LoginFailed;
        public event LoginChanged LogOutResult;
        public event RequestString Request;

        public UserServices ()
        {            
        }
        //public event LoginDelegate LoginEvent;
        private List<User> UserRepository = new();
        private List<UserData> UserData = new List<UserData>();
        public List<User> GetAllUsers()
        {
            return UserRepository;
        }
        public List<User> GetAllActiveUsers()
        {
            IEnumerable<User> ActiveUsers = from U in UserRepository 
                                     where U.IsActive                                
                                select U; 
            /*List<User> ActiveUsers = new();
            foreach (User U in UserRepository)
            {
                if (U.IsActive)
                {
                    ActiveUsers.Add(U);
                }
            }*/
            return ActiveUsers.ToList();
        }
        public bool LogIn(string userName, string passWord, out User user)
        {
            //successfullyFinded = false;
            user = null;
            try 
            {
                User LocalUser;
                LocalUser = (from U in UserRepository where U.UserName == userName select U).First();
                user = LocalUser;
                switch (LocalUser.Role)
                {
                    case AccessRole.User:
                        {
                            UserLogined?.Invoke("User Logined");
                            break;
                        }
                    case AccessRole.Admin:
                        {
                            AdminLogined?.Invoke("Admin Logined");
                            break;
                        }
                    case AccessRole.ProjectLeader: 
                        {
                            ProjectLeaderLogined?.Invoke("Project Leader Logined");
                            break;
                        }
                }
                return true;
            }
            catch (Exception ex)
            {
                LoginFailed?.Invoke("Login Failed!");
                return false;
            }           
            
        }
        public User LogOut(User user)
        {
            if (user!=null)
            {
                LogOutResult?.Invoke("LogOut completed");
            }
            else
            {
                LogOutResult?.Invoke("LogOut impossible. Please, LogIn");
            }
            return null;
        }
        public bool SubmitTime(User user)
        {
            string ProjectName = Request?.Invoke(" Enter project Name");
            int Hours;
            int.TryParse(Request?.Invoke(" Enter hours count : "), out Hours);
            DateTime Date;
            DateTime.TryParse(Request?.Invoke(" Enter hours count : "), out Date);
            return false;
        }






        public void Add(User user)
        {
            UserRepository.Add(user);
            UserData.Add(new UserData(user));
        }
        public void Delete(User user)
        {
            UserRepository.Add(user);
            UserData.Add(new UserData(user));
        }

    }
    public delegate void LoginChanged(string s);
    public delegate string RequestString(string c);

}
