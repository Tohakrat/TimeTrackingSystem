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
        private List<UserData> UserDataList = new List<UserData>();

        internal UserServices (DataFacade facade)
        {
            Facade = facade;
            UserData.SetFacade(Facade);            
            UserLogined += Facade.Delegates.MessageDelegate;            
        }
        internal void SetProjectServices(ProjectServices PS)
        {
            ProjServices = PS;            
        }

        internal void AddObject(UserData userData)
        {
            UserDataList.Add(userData);
        }
        internal void AddObject(User user)
        {
            UserDataList.Add(new UserData(user));
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
        internal void Login(ref Int32 userId)
        {
            UserData user;
            try
            {
                user = GetUserDataById(userId);
            }
            catch (KeyNotFoundException e)
            {
                userId=-1;
                return;
            }

            if (user == null)
            {
                string UserName, Password;
                UserName = Facade.Delegates.RequestDelegate("Enter login:");
                Password = Facade.Delegates.RequestDelegate("Enter password:");
                UserData TempUser = user;
                foreach (UserData Data in UserDataList)
                {
                    if (Data.CheckCredentials(UserName, Password))
                    {
                        user = Data;
                        user.SetActive();
                        UserLogined?.Invoke(user.GetAccessRole().ToString() + " Logined! ");
                        Facade.Delegates.ChangeUserDelegate(user.GetId());
                        userId = user.UserObj.Id;
                        return;
                    }
                }
                Facade.Delegates.MessageDelegate("User not found, please check login/password");
                //userId = -1;

            }
            else Facade.Delegates.MessageDelegate("You are already logined, please log out!");
            return;
        }
        //public bool LogIn(string userName, string passWord, out UserData user)
        //{            
        //    user = null;
        //    foreach (UserData Data in UserDataList)
        //    {
        //        if (Data.CheckCredentials(userName,passWord))
        //        {
        //            user=Data;
        //            user.SetActive();
        //            UserLogined?.Invoke(user.GetAccessRole().ToString()+" Logined! ");
        //            return true;
        //        }
        //    }    
        //    return false;                              
        //}        

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
        public bool SubmitTime(int userDataId,int ProjectId,int HoursCount, DateTime DateOfWork)
        {
            UserData UserDataObj = (from U in UserDataList where (userDataId == U.UserObj.Id) select U).FirstOrDefault();
            if (UserDataObj==null)
                return false;
            TimeTrackEntry TTEntry = new TimeTrackEntry(UserDataObj.GetId(), ProjectId, HoursCount, DateOfWork);
            UserDataObj.AddSubmittedTime(TTEntry);            
            return true;           
        }
        internal void SubmitTime(UserData user)
        {
            string project = Facade.Delegates.RequestDelegate("EnterProjectName:");
            int IdProject = Facade.ProjectServicesObj.FindIdByName(project);
            int HoursCount;
            if (int.TryParse(Facade.Delegates.RequestDelegate("Enter Count of Hours:"), out HoursCount) == false)
                Facade.Delegates.MessageDelegate("Hours is incorrect:");
            DateTime DateOfWork;
            if (DateTime.TryParse(Facade.Delegates.RequestDelegate("Enter Date:"), out DateOfWork) == false)
                Facade.Delegates.MessageDelegate("Date is incorrect:");

            if (SubmitTime(user.GetId(), IdProject, HoursCount, DateOfWork) == true)
                Facade.Delegates.MessageDelegate("Successfully added time");
            else Facade.Delegates.MessageDelegate("Error adding TimeEntry");
        }
        internal void AddTimeTrackEnty(TimeTrackEntry entry)
        {
            int UserId = entry.UserId;
            var UserDataToPasteEnty = UserDataList.Single(x => x.UserObj.Id == UserId);
            UserDataToPasteEnty.AddSubmittedTime(entry);
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
                if (UserRole>3||UserRole<1)
                {
                    Facade.Delegates.MessageDelegate("Wrong number!");
                    return false;
                }
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
            UserData UserToAdd = new UserData(MaxIndex + 1, Name, Password,(AccessRole)UserRole, FullName) ;
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
                    if (U.GetAccessRole()==AccessRole.ProjectLeader)
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
        internal UserData GetUserDataById(int id)
        {
            if (id == -1)//if user not logined, client stores -1.
                return null;
            else
            {
                
                var UserDataObj = UserDataList.Single(x => x.UserObj.Id == id);
                if (UserDataObj != null)
                    return UserDataObj;
                else throw new KeyNotFoundException();
            }
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
        
    }

}
