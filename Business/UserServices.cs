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
        private DataFacadeDelegates Delegates;
        public event Action<string> UserLogined;
        //public event Action<string> AdminLogined;
        //public event Action<string> ProjectLeaderLogined;
        public event Action<string> LoginFailed;
        //public event LoginChanged LogOutResult;
        //public event RequestString Request;

        internal UserServices (DataFacadeDelegates delegates)
        {
            Delegates = delegates;
            Seed();
            UserLogined += Delegates.MessageDelegate;
            LoginFailed += Delegates.MessageDelegate;
        }
        
        private List<User> UserRepository = new();
        private List<UserData> UserDataList = new List<UserData>();

        private void Seed()
        {
            Add(new User(1,"Vasia", "1234", AccessRole.User));
            Add(new User(2,"Petia", "1234", AccessRole.User));
            Add(new User(3,"u", "u", AccessRole.User));
            Add(new User(4,"Vlad", "1234", AccessRole.Admin));
            Add(new User(5,"a", "a", AccessRole.Admin));
            Add(new User(6,"Ivan", "1234", AccessRole.ProjectLeader));
            Add(new User(7,"p", "p", AccessRole.ProjectLeader));

            int i = 1;
            foreach(UserData Data in UserDataList)
            {
                Data.AddSubmittedTime(new TimeTrackEntry(Data.UserObj.Id, i, i * 10, DateTime.Now));
                    i++;
            }
        }
        internal List<User> GetAllUsers()
        {
            return UserRepository;
        }
        internal string GetAllUsersString()
        {
            StringBuilder Result = new();
            Result.AppendLine();
            Result.AppendLine("All users: ");

            foreach (User User in UserRepository)
            {
                Result.AppendLine();
                
                Result.Append(" User Name: ");
                Result.Append(User.UserName);
                Result.Append(" User FullName: ");
                Result.Append(User.UserName);
                Result.Append(" User Access Role: ");
                Result.Append(User.Role.ToString());
                
                Result.Append(" ");
            }
            //ProjectListTransmitted?.Invoke(Result.ToString());
            return Result.ToString();
            
        }
        public List<User> GetAllActiveUsers()
        {
            IEnumerable<User> ActiveUsers = from U in UserRepository 
                                     where U.IsActive                                
                                select U; 
           
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
                if (LocalUser != null)
                    LocalUser.IsActive = true;
                switch (LocalUser.Role)
                {
                    case AccessRole.User:
                        {
                            UserLogined?.Invoke("User Logined");
                            break;
                        }
                    case AccessRole.Admin:
                        {
                            UserLogined?.Invoke("Admin Logined");
                            break;
                        }
                    case AccessRole.ProjectLeader: 
                        {
                            UserLogined?.Invoke("Project Leader Logined");
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
        public void LogOut(User user, Action<User> ChangeUserDelegate, Action<String> MessageDelegate)         
        {
            if (user!=null)
            {
                user.IsActive = false;
                ChangeUserDelegate(null);
                MessageDelegate("LogOut completed");
                
            }
            else
            {
                MessageDelegate("LogOut hadnt completed, please log in.");
            }            
        }
        public bool SubmitTime(User user,int ProjectId,int HoursCount, DateTime DateOfWork)
        {
            UserData UserDataObj = (from U in UserDataList where user.Equals( U.UserObj) select U).FirstOrDefault();
            if (UserDataObj==null)
                return false;
            TimeTrackEntry TTEntry = new TimeTrackEntry(UserDataObj.UserObj.Id, ProjectId, HoursCount, DateOfWork);
            UserDataObj.AddSubmittedTime(TTEntry);            
            return true;           
        }
        public string ViewSubmittedTime(User user)
        {
            //int Result = 0;
            foreach (UserData UD in UserDataList)
            {
                if (UD.UserObj.Id == user.Id)
                {
                    return UD.GetTimeTrackString();
                }                
            }
            return "\n Submitted time not found!";
            
        }


        public void Add(User user)
        {
            UserRepository.Add(user);
            UserDataList.Add(new UserData(user));
        }
        public bool Add()
        {
            int UserRole;

            try
            {
                UserRole = int.Parse(Delegates.RequestDelegate("Enter User Role 1-user 2-admin 3-Project leader: "));
            }
            catch (Exception E)
            {
                Delegates.MessageDelegate("Incorrect Data. ");
                return false;
            }
            String Name = Delegates.RequestDelegate("Enter user name: ");
            String Password = Delegates.RequestDelegate("Enter password: ");
            String FullName = Delegates.RequestDelegate("Enter Full Name: ");
            int MaxIndex = GetMaxIndex();
            UserRepository.Add(new User(MaxIndex + 1, Name, Password,(AccessRole)UserRole, FullName) );
            return true;    
        }
        private int GetMaxIndex()
        {
            return UserRepository.Max(u => u.Id);
        }
        public void Delete(User user)
        {
            UserRepository.Add(user);
            UserDataList.Add(new UserData(user));
        }

    }
    //public delegate void LoginChanged(string s);
    //public delegate string RequestString(string c);

}
