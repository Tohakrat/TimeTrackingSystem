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
        public event Action<string> LoginFailed;        

        internal UserServices (DataFacadeDelegates delegates)
        {
            Delegates = delegates;
            Seed();
            SeedTimeTrackEntry();
            UserLogined += Delegates.MessageDelegate;
            LoginFailed += Delegates.MessageDelegate;
        }
        
        private List<User> UserRepository = new();
        private List<UserData> UserDataList = new List<UserData>();

        private void Seed()
        {
            Add(new User(0,"Vasia", "1234", AccessRole.User,"Vasily Ivanovich"));
            Add(new User(1,"Petia", "1234", AccessRole.User, "Petr Iosifovich"));
            Add(new User(2,"u", "u", AccessRole.User,"Uriy Nickolaevich"));
            Add(new User(3,"w", "w", AccessRole.User,"William"));
            Add(new User(4,"Vlad", "1234", AccessRole.Admin, "Vladimir Ilyich"));
            Add(new User(5,"a", "a", AccessRole.Admin, "Andrey Mihailovich"));
            Add(new User(6,"Vania", "1234", AccessRole.ProjectLeader, "Ivan Vladimirovich"));
            Add(new User(7,"p", "p", AccessRole.ProjectLeader, "Patrick"));

            /*int i = 1;
            foreach(UserData Data in UserDataList)
            {
                Data.AddSubmittedTime(new TimeTrackEntry(Data.UserObj.Id, i, i * 10, DateTime.Now));
                    i++;
            }*/
        }
        internal string GetAllActiveUsers()
        {
            List<string> TempList =  (from UserItem in UserRepository where UserItem.IsActive == true select UserItem.FullName).ToList();

            return string.Join(",", TempList.ToArray());
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
            return Result.ToString();            
        }
        
        public bool LogIn(string userName, string passWord, out User user)
        {            
            user = null;
            try 
            {
                User LocalUser;
                LocalUser = (from U in UserRepository where U.UserName == userName select U).First();
                user = LocalUser;
                if (user.PassWord!=passWord)
                {
                    LoginFailed?.Invoke("Password Failed!");
                    user = null;
                    return false;
                }
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
        public string ViewSubmittedTime(User user,Func<int,string> FindNameById)
        {            
            foreach (UserData UD in UserDataList)
            {
                if (UD.UserObj.Id == user.Id)
                {
                    return UD.GetTimeTrackString(FindNameById);
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
            User UserToAdd = new User(MaxIndex + 1, Name, Password,(AccessRole)UserRole, FullName) ;
            UserRepository.Add(UserToAdd) ;
            UserDataList.Add(new UserData(UserToAdd));
            return true;    
        }
        internal bool DeleteUser(Action<int> DeleteProjectLeader, User MeUser)
        {
            string UserName;
            bool resultUser=false;
            bool resultUserData=false;
            User UserToRemove = null;
            UserData UserDataToRemove = null;
            //int DeletedUserIndex = -1;
            

            UserName = Delegates.RequestDelegate("Enter login:");
            if (UserName==MeUser.UserName) //If I delete me, it is not allowed
            {
                Delegates.MessageDelegate(" You cant delete yourself! ");
                return false;
            }
                foreach (User U in UserRepository)
            {
                if (U.UserName == UserName)
                {
                    UserToRemove = U;
                    if (U.Role==AccessRole.ProjectLeader)// Delete all references from projects to this ProjectLeader
                    {
                        DeleteProjectLeader(U.Id);
                    }
                    
                    break;
                }
            }
            foreach (UserData U in UserDataList)
            {
                if (U.UserObj.UserName == UserName)
                {
                    UserDataToRemove = U;
                    
                    break;
                }

            }
            
            resultUser = UserRepository.Remove(UserToRemove);
            resultUserData = UserDataList.Remove(UserDataToRemove);
            bool result = resultUser&& resultUserData;
            if (result) Delegates.MessageDelegate(" User is deleted successfully. ");
            else Delegates.MessageDelegate(" Error! User is not deleted! ");
            return result;  
        }
        internal int GetUserIdByName(string Name,AccessRole role)
        {
            foreach (User U in UserRepository)
            {
                if (U.UserName== Name&&U.Role==role)
                {
                    return U.Id;
                }
            }
            throw new KeyNotFoundException { };
        }
        internal string GetUserNameById(int id)
        {
            if (id == -1) return "not assigned";
            foreach (User U in UserRepository)
            {
                if (U.Id == id)
                {
                    return U.UserName;
                }
            }
            throw new KeyNotFoundException { };
        }
        private int GetMaxIndex()
        {
            return UserRepository.Max(u => u.Id);
        }     


        internal string ReportActiveUsers(Func<string,int> GetProjectIdByName)
        {
            StringBuilder Result = new StringBuilder();
            string Project = Delegates.RequestDelegate("Enter project:");
            int ProjectId = GetProjectIdByName(Project);
            int HoursCount;
            if (!int.TryParse(Delegates.RequestDelegate("Enter minimum hours count:"), out HoursCount))
            {
                Delegates.MessageDelegate("Wrong count! ");
                return null;
            }
            foreach (UserData UD in UserDataList)
            {
                int CurrentHours = UD.GetSubmittedHoursByProjectId(ProjectId);
                if (CurrentHours>HoursCount)
                {
                    Result.AppendLine();
                    Result.Append(UD.UserObj.FullName+ ": " + CurrentHours.ToString());
                    //Result.Append(": " + CurrentHours.ToString());
                }

            }
            return Result.ToString();
        }
        private void SeedTimeTrackEntry()
        {
            UserDataList[0].AddSubmittedTime(new TimeTrackEntry(0, 5, 15, DateTime.Parse("1.02.2022")));
            UserDataList[0].AddSubmittedTime(new TimeTrackEntry(0, 5, 20, DateTime.Parse("1.01.2022")));
            UserDataList[0].AddSubmittedTime(new TimeTrackEntry(0, 5, 5, DateTime.Parse("15.01.2021")));
            UserDataList[1].AddSubmittedTime(new TimeTrackEntry(1, 6, 15, DateTime.Parse("1.02.2022")));
            UserDataList[1].AddSubmittedTime(new TimeTrackEntry(1, 5, 11, DateTime.Parse("3.02.2022")));
            UserDataList[1].AddSubmittedTime(new TimeTrackEntry(1, 5, 9, DateTime.Parse("1.02.2022")));
            UserDataList[2].AddSubmittedTime(new TimeTrackEntry(1, 6, 15, DateTime.Parse("1.02.2022")));
            UserDataList[2].AddSubmittedTime(new TimeTrackEntry(2, 6, 11, DateTime.Parse("1.02.2022")));
            UserDataList[2].AddSubmittedTime(new TimeTrackEntry(2, 7, 50, DateTime.Parse("3.02.2022")));
            UserDataList[3].AddSubmittedTime(new TimeTrackEntry(3, 7, 15, DateTime.Parse("4.02.2022")));
            UserDataList[3].AddSubmittedTime(new TimeTrackEntry(3, 7, 11, DateTime.Parse("1.02.2022")));
            UserDataList[3].AddSubmittedTime(new TimeTrackEntry(3, 7, 19, DateTime.Parse("1.02.2022")));
            UserDataList[6].AddSubmittedTime(new TimeTrackEntry(6, 6, 29, DateTime.Parse("21.06.2021")));
        }

    }

}
