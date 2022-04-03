using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using System.Diagnostics;


namespace Business
{
    internal class Menu
    {        
        private List<Operation> _OperationList = new();
        internal Menu()
        {            
            SeedMenu();
        }        
        private void SeedMenu()
        {
            _OperationList.Add(new Operation("LogIn", null, true,1, UserLoginState.NotLogined,DataFacade.Instance.UserServices.Login));
            _OperationList.Add(new Operation("LogOut", null, false, 2, UserLoginState.Logined, DataFacade.Instance.UserServices.LogOut));
            _OperationList.Add(new Operation("Available Projects", null, true, 3, UserLoginState.Logined, DataFacade.Instance.ProjectServices.GetProjectsString));
            _OperationList.Add(new Operation("Submit Time", null, true, 4, UserLoginState.Logined, DataFacade.Instance.UserServices.SubmitTime));
            _OperationList.Add(new Operation("View Submitted Time", null, true, 5, UserLoginState.Logined, DataFacade.Instance.UserServices.ViewSubmittedTime));
            _OperationList.Add(new Operation("View All Users", null, true, 6, UserLoginState.Logined, DataFacade.Instance.UserServices.GetAllUsersString));
            _OperationList.Add(new Operation("Create User", AccessRole.Admin, true, 7, UserLoginState.Logined, DataFacade.Instance.UserServices.Add));
            _OperationList.Add(new Operation("Delete User", AccessRole.Admin, true, 8, UserLoginState.Logined, DataFacade.Instance.UserServices.DeleteUser));
            _OperationList.Add(new Operation("Create Project", AccessRole.Admin, true, 9, UserLoginState.Logined, DataFacade.Instance.ProjectServices.AddProject));
            _OperationList.Add(new Operation("Delete Project", AccessRole.Admin, true, 10, UserLoginState.Logined, DataFacade.Instance.ProjectServices.DeleteProject));
            _OperationList.Add(new Operation("Report: Active Users in Project", AccessRole.ProjectLeader, true, 12, UserLoginState.Logined, DataFacade.Instance.ReportActiveUsers));
            _OperationList.Add(new Operation("Report: Users, who are active now", AccessRole.ProjectLeader, true, 13, UserLoginState.Logined, DataFacade.Instance.UserServices.GetAllActiveUsers));
            _OperationList.Add(new Operation("Quit", null, true, 0, null, DataFacade.Instance.Quit));
        }

        internal bool ProcessAnswer(int answer ,int user)
        {
            UserData UserDataObj;
            try 
            {
                UserDataObj = DataFacade.Instance.UserServices.GetUserDataById(user);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.Instance.Delegates.MessageDelegate("Error! User Id not found!");
                return false;
            }
            
            UserLoginState StateUser;
            AccessRole RoleUser;
            if (UserDataObj == null)
            {
                RoleUser = AccessRole.User;
                StateUser = UserLoginState.NotLogined;
            }
            else
            {
                RoleUser = UserDataObj.User.Role;
                StateUser = UserLoginState.Logined;
            }
            
            Operation operation = _OperationList.Single(x => x.NumberOpreation == answer);

            if ((operation.StateLogin == null || operation.StateLogin == StateUser) && (operation.AvailableFor == null || operation.AvailableFor == RoleUser))
            {
                operation.DoOperation(user);
                return true;
            }
            return false;
        }

        internal string GetAvailableOperations(Int32 userId)
        {
            UserData UserDataObj;
            try
            {
                UserDataObj = DataFacade.Instance.UserServices.GetUserDataById(userId);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.Instance.Delegates.MessageDelegate("Error! User Id not found!");
                return "Error!";
            }


            UserLoginState StateUser = UserLoginState.NotLogined;
            AccessRole RoleUser;
            if (UserDataObj == null)
            {
                RoleUser = AccessRole.User;
                StateUser = UserLoginState.NotLogined;
            }
            else
            {
                RoleUser = UserDataObj.User.Role;
                StateUser = UserLoginState.Logined;
            }      

            StringBuilder Result = new();
            Result.AppendLine();
            Result.AppendLine("Available operations");
            foreach (Operation op in _OperationList)
            {
                if ((op.StateLogin == null || op.StateLogin == StateUser) && (op.AvailableFor == null||op.AvailableFor == RoleUser ))
                {
                    Result.AppendLine(op.Name + " " +op.NumberOpreation.ToString());
                }
            }
            return Result.ToString();
        }
    }
}
 