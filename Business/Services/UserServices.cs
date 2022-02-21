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
        private DataFacade Facade;
        public event Action<string> UserLogined;
        internal ProjectServices ProjServices;

        internal UserServices (DataFacade facade)
        {
            Facade = facade;            
            Seed();
            SeedTimeTrackEntry();
            UserLogined += Facade.Delegates.MessageDelegate;            
        }
        internal void SetProjectServices(ProjectServices PS)
        {
            ProjServices = PS;            
        }        
        
        private List<UserData> UserDataList = new List<UserData>();

        private void Seed()
        {            
            Add(new UserData(Facade,0, "Vasia", "1234", AccessRole.User,"Vasily Ivanovich"));
            Add(new UserData(Facade,1, "Petia", "1234", AccessRole.User, "Petr Iosifovich"));
            Add(new UserData(Facade, 2,"u", "u", AccessRole.User,"Uriy Nickolaevich"));
            Add(new UserData(Facade, 3,"w", "w", AccessRole.User,"William"));
            Add(new UserData(Facade, 4,"Vlad", "1234", AccessRole.Admin, "Vladimir Ilyich"));
            Add(new UserData(Facade, 5,"a", "a", AccessRole.Admin, "Andrey Mihailovich"));
            Add(new UserData(Facade, 6,"Vania", "1234", AccessRole.ProjectLeader, "Ivan Vladimirovich"));
            Add(new UserData(Facade, 7,"p", "p", AccessRole.ProjectLeader, "Patrick"));            
        }
        internal string GetAllActiveUsers()
        {
            List<string> TempList =  (from UserDataItem in UserDataList where UserDataItem.UserObj.IsActive == true select UserDataItem.UserObj.FullName).ToList();

            return string.Join(",", TempList.ToArray());
        }
        internal string GetAllUsersString()
        {
            StringBuilder Result = new();
            Result.AppendLine();
            Result.AppendLine("All users: ");

            foreach (UserData Data in UserDataList)
            {
                Result.AppendLine();

                Result.Append(" User Name: ");
                Result.Append(Data.GetName());
                Result.Append(" User FullName: ");
                Result.Append(Data.GetFullName());
                Result.Append(" User Access Role: ");
                Result.Append(Data.GetAccessRole());

                Result.Append(" ");
            }            
            return Result.ToString();            
        }
        
        public bool LogIn(string userName, string passWord, out UserData user)
        {            
            user = null;
            foreach (UserData Data in UserDataList)
            {
                if (Data.CheckCredentials(userName,passWord))
                {
                    user=Data;
                    user.SetActive();
                    UserLogined?.Invoke(user.GetAccessRole().ToString()+" Logined! ");
                    return true;
                }
            }    
            return false;                              
        }        

        public void LogOut(UserData user, Action<UserData> ChangeUserDelegate)         
        {
            if (user!=null)
            {
                user.SetNotActive();
                ChangeUserDelegate(null);
                Facade.Delegates.MessageDelegate("LogOut completed");                
            }
            else
            {
                Facade.Delegates.MessageDelegate("LogOut hadnt completed, please log in.");
            }            
        }
        public bool SubmitTime(UserData user,int ProjectId,int HoursCount, DateTime DateOfWork)
        {
            UserData UserDataObj = (from U in UserDataList where user.Equals( U) select U).FirstOrDefault();
            if (UserDataObj==null)
                return false;
            TimeTrackEntry TTEntry = new TimeTrackEntry(UserDataObj.GetId(), ProjectId, HoursCount, DateOfWork);
            UserDataObj.AddSubmittedTime(TTEntry);            
            return true;           
        }
        public string ViewSubmittedTime(UserData user,Func<int,string> FindNameById)
        {            
            foreach (UserData UD in UserDataList)
            {
                if (UD.UserObj.Id == user.GetId())
                {
                    return UD.GetTimeTrackString(FindNameById);
                }                
            }
            return "\n Submitted time not found!";            
        }

        public void Add(UserData user)
        {
            UserDataList.Add(user);            
        }
        public bool Add()
        {
            int UserRole;

            try
            {
                UserRole = int.Parse(Facade.Delegates.RequestDelegate("Enter User Role 1-user 2-admin 3-Project leader: "));
            }
            catch (Exception E)
            {
                Facade.Delegates.MessageDelegate("Incorrect Data. ");
                return false;
            }
            String Name = Facade.Delegates.RequestDelegate("Enter user name: ");
            String Password = Facade.Delegates.RequestDelegate("Enter password: ");
            String FullName = Facade.Delegates.RequestDelegate("Enter Full Name: ");
            int MaxIndex = GetMaxIndex();
            UserData UserToAdd = new UserData(Facade,MaxIndex + 1, Name, Password,(AccessRole)UserRole, FullName) ;
            UserDataList.Add(UserToAdd) ;            
            return true;    
        }
        internal bool DeleteUser(UserData MeUser)
        {
            string UserName;
            bool result = false;            
            
            UserData UserDataToRemove = null;            
            

            UserName = Facade.Delegates.RequestDelegate("Enter login:");
            if (UserName==MeUser.GetName()) //If I delete me, it is not allowed
            {
                Facade.Delegates.MessageDelegate(" You cant delete yourself! ");
                return false;
            }
            foreach (UserData U in UserDataList)
            {
                if (U.GetName() == UserName)
                {
                    UserDataToRemove = U;
                    if (U.GetAccessRole()==AccessRole.ProjectLeader)// Delete all references from projects to this ProjectLeader
                    {
                        if (ProjServices.IsUserResponsible(U.GetId()))
                        {
                            Facade.Delegates.MessageDelegate(" There are some projects under responsibility of this project leader. Deleting denied!");
                            return false;
                        }                        
                    }
                    result = UserDataList.Remove(U);
                    if (result) Facade.Delegates.MessageDelegate(" User is deleted successfully. ");
                    else Facade.Delegates.MessageDelegate(" Error! User is not deleted! ");
                    return result;
                    
                }
            }
            Facade.Delegates.MessageDelegate(" Deleted object not found! ");
            return false;    
            
            
        }
        internal int GetUserIdByName(string Name,AccessRole role=AccessRole.Any)
        {
            foreach (UserData U in UserDataList)
            {
                if (U.GetName()== Name&&U.GetAccessRole()==role)
                {
                    return U.GetId();
                }
            }
            throw new KeyNotFoundException { };
        }
        internal string GetUserNameById(int id)
        {
            if (id == -1) return "not assigned";
            foreach (UserData U in UserDataList)
            {
                if (U.GetId() == id)
                {
                    return U.GetName();
                }
            }
            throw new KeyNotFoundException { };
        }
        private int GetMaxIndex()
        {
            return UserDataList.Max(u => u.GetId());
        }     


        internal string ReportActiveUsers(Func<string,int> GetProjectIdByName)
        {
            StringBuilder Result = new StringBuilder();
            string Project = Facade.Delegates.RequestDelegate("Enter project:");
            int ProjectId = GetProjectIdByName(Project);
            int HoursCount;
            if (!int.TryParse(Facade.Delegates.RequestDelegate("Enter minimum hours count:"), out HoursCount))
            {
                Facade.Delegates.MessageDelegate("Wrong count! ");
                return null;
            }
            foreach (UserData UD in UserDataList)
            {
                int CurrentHours = UD.GetSubmittedHoursByProjectId(ProjectId);
                if (CurrentHours>HoursCount)
                {
                    Result.AppendLine();
                    Result.Append(UD.UserObj.FullName+ ": " + CurrentHours.ToString());                    
                }

            }
            return Result.ToString();
        }
        internal bool IsResponsible(UserData User)
        {
            int Id = User.GetId();
            return ProjServices.IsUserResponsible(Id);            

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
