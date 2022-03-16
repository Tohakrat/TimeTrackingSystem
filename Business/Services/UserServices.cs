using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using Infrastructure;

namespace Business
{
    public sealed class UserServices
    {                
        public event Action<string> UserLogined;
        internal ProjectServices ProjServices;
        private List<UserData> _UserDataList = new List<UserData>();
        public event EventHandler<ObjectEventArgs<User>> UserAdded;
        public event EventHandler<ObjectEventArgs<User>> UserDeleted;

        internal UserServices ()
        { 
        }
        
        internal void SetEventsDelegates()
        {
            UserLogined += DataFacade.GetDataFacade().Delegates.MessageDelegate;
        }
        internal void SetProjectServices(ProjectServices PS)
        {
            ProjServices = PS;            
        }
        //internal void AddObject(UserData userData)
        //{
        //    _UserDataList.Add(userData);
        //}
        internal void AddObject(User user)
        {
            _UserDataList.Add(new UserData(user));
            UserAdded?.Invoke(this,new ObjectEventArgs<User> (user));
        }        
        
        internal void GetAllActiveUsers(Int32 user)
        {
            List<string> TempList =  (from UserDataItem in _UserDataList where UserDataItem.UserObj.IsActive == true select UserDataItem.UserObj.FullName).ToList();

            DataFacade.GetDataFacade().Delegates.MessageDelegate(string.Join(",", TempList.ToArray()));
        }
        internal void GetAllUsersString(int user)
        {
            StringBuilder Result = new();
            Result.AppendLine();
            Result.AppendLine("All users: ");

            foreach (UserData Data in _UserDataList)
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
            DataFacade.GetDataFacade().Delegates.MessageDelegate( Result.ToString());            
        }
        internal void Login(Int32 userId)
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
                UserName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter login:");
                Password = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter password:");
                UserData TempUser = user;
                foreach (UserData Data in _UserDataList)
                {
                    if (Data.CheckCredentials(UserName, Password))
                    {
                        user = Data;
                        user.SetActive();
                        UserLogined?.Invoke(user.GetAccessRole().ToString() + " Logined! ");
                        DataFacade.GetDataFacade().Delegates.ChangeUserDelegate(user.GetId());   
                        return;
                    }
                }
                DataFacade.GetDataFacade().Delegates.MessageDelegate("User not found, please check login/password");
                

            }
            else DataFacade.GetDataFacade().Delegates.MessageDelegate("You are already logined, please log out!");
            return;
        }
        
        public void LogOut(Int32 user)   
        {
            UserData UserDataObj = GetUserDataById(user);       

            if (UserDataObj != null)
            {
                UserDataObj.SetNotActive();
                DataFacade.GetDataFacade().Delegates.ChangeUserDelegate(-1);
                DataFacade.GetDataFacade().Delegates.MessageDelegate("LogOut completed");                
            }
            else
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("LogOut hadnt completed, please log in.");
            }            
        }
        public bool SubmitTime(int userDataId,int ProjectId,int HoursCount, DateTime DateOfWork)
        {
            UserData UserDataObj = (from U in _UserDataList where (userDataId == U.UserObj.Id) select U).FirstOrDefault();
            if (UserDataObj==null)
                return false;
            TimeTrackEntry TTEntry = new TimeTrackEntry(UserDataObj.GetId(), ProjectId, HoursCount, DateOfWork);
            UserDataObj.AddSubmittedTime(TTEntry);            
            return true;           
        }
        internal void SubmitTime(Int32 userId)
        {
            UserData UserObj = GetUserDataById(userId);
            string project = DataFacade.GetDataFacade().Delegates.RequestDelegate("EnterProjectName:");
            int IdProject = DataFacade.GetDataFacade().ProjectServicesObj.FindIdByName(project);
            int HoursCount;
            if (int.TryParse(DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter Count of Hours:"), out HoursCount) == false)
                DataFacade.GetDataFacade().Delegates.MessageDelegate("Hours is incorrect:");
            DateTime DateOfWork;
            if (DateTime.TryParse(DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter Date:"), out DateOfWork) == false)
                DataFacade.GetDataFacade().Delegates.MessageDelegate("Date is incorrect:");

            if (SubmitTime(userId, IdProject, HoursCount, DateOfWork) == true)
                DataFacade.GetDataFacade().Delegates.MessageDelegate("Successfully added time");
            else DataFacade.GetDataFacade().Delegates.MessageDelegate("Error adding TimeEntry");
        }
        internal void AddTimeTrackEnty(TimeTrackEntry entry)
        {
            int UserId = entry.UserId;
            var UserDataToPasteEnty = _UserDataList.Single(x => x.UserObj.Id == UserId);
            UserDataToPasteEnty.AddSubmittedTime(entry);
        }

        public void ViewSubmittedTime(Int32 userId)
        {            
            foreach (UserData UD in _UserDataList)
            {
                if (UD.UserObj.Id == userId)
                {
                    DataFacade.GetDataFacade().Delegates.MessageDelegate( UD.GetTimeTrackString(DataFacade.GetDataFacade().ProjectServicesObj.FindNameById));
                    return;
                }                
            }
            DataFacade.GetDataFacade().Delegates.MessageDelegate("\n User not found!");            
        }

        //public void Add(UserData user)
        //{
        //    _UserDataList.Add(user);            
        //}
        public void Add(int user)
        {
            int UserRole;

            try
            {
                UserRole = int.Parse(DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter User Role 1-user 2-admin 3-Project leader: "));
                if (UserRole>3||UserRole<1)
                {
                    DataFacade.GetDataFacade().Delegates.MessageDelegate("Wrong number!");
                    return;// false;
                }
            }
            catch (Exception E)
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("Incorrect Data. ");
                return;// false;
            }
            String Name = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter user name: ");
            String Password = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter password: ");
            String FullName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter Full Name: ");
            int MaxIndex = GetMaxIndex();
            AddObject(new User(MaxIndex + 1, Name, Password, (AccessRole)UserRole, FullName));
            //UserData UserToAdd = new UserData(MaxIndex + 1, Name, Password,(AccessRole)UserRole, FullName) ;
            //_UserDataList.Add(UserToAdd) ;
            DataFacade.GetDataFacade().Delegates.MessageDelegate("Successfully ");
            return;// true;    
        }
        internal void DeleteUser(Int32 MeUserId)
        {
            string UserName;
            bool result = false;
            UserData UserDataToRemove = null;
            UserData MeUserObj = GetUserDataById(MeUserId);
            if (MeUserObj==null)
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("Error. User not found");
            }                    
            
            UserName = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter login:");
            if (UserName== MeUserObj.GetName()) //If I delete me, it is not allowed
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate(" You cant delete yourself! ");
                return;// false;
            }
            foreach (UserData U in _UserDataList)
            {
                if (U.GetName() == UserName)
                {
                    UserDataToRemove = U;
                    if (U.GetAccessRole()==AccessRole.ProjectLeader)
                    {
                        if (ProjServices.IsUserResponsible(U.GetId()))
                        {
                            DataFacade.GetDataFacade().Delegates.MessageDelegate(" There are some projects under responsibility of this project leader. Deleting denied!");
                            return;
                        }                        
                    }
                    UserDeleted?.Invoke(this, new ObjectEventArgs<User>(UserDataToRemove.UserObj));
                    result = _UserDataList.Remove(U);
                    
                    if (result) DataFacade.GetDataFacade().Delegates.MessageDelegate(" User is deleted successfully. ");
                    else DataFacade.GetDataFacade().Delegates.MessageDelegate(" Error! User is not deleted! ");
                    return;             
                }
            }
            DataFacade.GetDataFacade().Delegates.MessageDelegate(" Deleted object not found! ");
            return;               
            
        }
        internal int GetUserIdByName(string Name,AccessRole role=AccessRole.User)
        {
            foreach (UserData U in _UserDataList)
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
            foreach (UserData U in _UserDataList)
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
                var UserDataObj = _UserDataList.Single(x => x.UserObj.Id == id);
                if (UserDataObj != null)
                    return UserDataObj;
                else
                {
                    DataFacade.GetDataFacade().Delegates.MessageDelegate("User Services error. User not found!");
                    return null;
                }
            }
        }
        
        private int GetMaxIndex()
        {
            return _UserDataList.Max(u => u.GetId());
        }     

        internal string ReportActiveUsers(Func<string,int> GetProjectIdByName)
        {
            StringBuilder Result = new StringBuilder();
            string Project = DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter project:");
            int ProjectId = GetProjectIdByName(Project);
            int HoursCount;
            if (!int.TryParse(DataFacade.GetDataFacade().Delegates.RequestDelegate("Enter minimum hours count:"), out HoursCount))
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("Wrong count! ");
                return null;
            }
            foreach (UserData UD in _UserDataList)
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
