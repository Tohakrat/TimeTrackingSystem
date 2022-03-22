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
            _OperationList.Add(new Operation("LogIn", null, true,1, UserLoginState.NotLogined,DataFacade.GetDataFacade().UserServicesObj.Login));
            _OperationList.Add(new Operation("LogOut", null, false, 2, UserLoginState.Logined, DataFacade.GetDataFacade().UserServicesObj.LogOut));
            _OperationList.Add(new Operation("Available Projects", null, true, 3, UserLoginState.Logined, DataFacade.GetDataFacade().ProjectServicesObj.GetProjectsString));
            _OperationList.Add(new Operation("Submit Time", null, true, 4, UserLoginState.Logined, DataFacade.GetDataFacade().UserServicesObj.SubmitTime));
            _OperationList.Add(new Operation("View Submitted Time", null, true, 5, UserLoginState.Logined, DataFacade.GetDataFacade().UserServicesObj.ViewSubmittedTime));
            _OperationList.Add(new Operation("View All Users", null, true, 6, UserLoginState.Logined, DataFacade.GetDataFacade().UserServicesObj.GetAllUsersString));
            _OperationList.Add(new Operation("Create User", AccessRole.Admin, true, 7, UserLoginState.Logined, DataFacade.GetDataFacade().UserServicesObj.Add));
            _OperationList.Add(new Operation("Delete User", AccessRole.Admin, true, 8, UserLoginState.Logined, DataFacade.GetDataFacade().UserServicesObj.DeleteUser));
            _OperationList.Add(new Operation("Create Project", AccessRole.Admin, true, 9, UserLoginState.Logined, DataFacade.GetDataFacade().ProjectServicesObj.AddProject));
            _OperationList.Add(new Operation("Delete Project", AccessRole.Admin, true, 10, UserLoginState.Logined, DataFacade.GetDataFacade().ProjectServicesObj.DeleteProject));
            _OperationList.Add(new Operation("Report: Active Users in Project", AccessRole.ProjectLeader, true, 12, UserLoginState.Logined, DataFacade.GetDataFacade().ReportActiveUsers));
            _OperationList.Add(new Operation("Report: Users, who are active now", AccessRole.ProjectLeader, true, 13, UserLoginState.Logined, DataFacade.GetDataFacade().UserServicesObj.GetAllActiveUsers));
            _OperationList.Add(new Operation("Quit", null, true, 0, null, DataFacade.GetDataFacade().Quit));
        }

        internal bool ProcessAnswer(int answer ,int user)
        {
            UserData UserDataObj;
            try 
            {
                UserDataObj = DataFacade.GetDataFacade().UserServicesObj.GetUserDataById(user);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("Error! User Id not found!");
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
                RoleUser = UserDataObj.UserObj.Role;
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
                UserDataObj = DataFacade.GetDataFacade().UserServicesObj.GetUserDataById(userId);
            }
            catch (KeyNotFoundException e)
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("Error! User Id not found!");
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
                RoleUser = UserDataObj.UserObj.Role;
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
 