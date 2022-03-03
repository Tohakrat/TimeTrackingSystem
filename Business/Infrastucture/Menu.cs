using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    internal class Menu
    {
        //DataFacade Facade;
        List<Operation> OperationList = new();
        internal Menu()//DataFacade facade)
        {
            //Facade = facade;
            SeedMenu();
        }
        
        private void SeedMenu()
        {
            OperationList.Add(new Operation("LogIn", DataContracts.AccessRole.Any, true,1, State.NotLogined,DataFacade.GetDataFacade().UserServicesObj.Login));
            OperationList.Add(new Operation("LogOut", DataContracts.AccessRole.Any, false, 2, State.Logined, DataFacade.GetDataFacade().UserServicesObj.LogOut));
            OperationList.Add(new Operation("Available Projects", DataContracts.AccessRole.Any, true, 3, State.Logined, DataFacade.GetDataFacade().ProjectServicesObj.GetProjectsString));
            OperationList.Add(new Operation("Submit Time", DataContracts.AccessRole.Any, true, 4, State.Logined, DataFacade.GetDataFacade().UserServicesObj.SubmitTime));
            OperationList.Add(new Operation("View Submitted Time", DataContracts.AccessRole.Any, true, 5, State.Logined, DataFacade.GetDataFacade().UserServicesObj.ViewSubmittedTime));
            OperationList.Add(new Operation("View All Users", DataContracts.AccessRole.Any, true, 6, State.Logined, DataFacade.GetDataFacade().UserServicesObj.GetAllUsersString));
            OperationList.Add(new Operation("Create User", DataContracts.AccessRole.Admin, true, 7, State.Logined, DataFacade.GetDataFacade().UserServicesObj.Add));
            OperationList.Add(new Operation("Delete User", DataContracts.AccessRole.Admin, true, 8, State.Logined, DataFacade.GetDataFacade().UserServicesObj.DeleteUser));
            OperationList.Add(new Operation("Create Project", DataContracts.AccessRole.Admin, true, 9, State.Logined, DataFacade.GetDataFacade().ProjectServicesObj.AddProject));
            OperationList.Add(new Operation("Delete Project", DataContracts.AccessRole.Admin, true, 10, State.Logined, DataFacade.GetDataFacade().ProjectServicesObj.DeleteProject));
            OperationList.Add(new Operation("Report: Active Users in Project", DataContracts.AccessRole.ProjectLeader, true, 12, State.Logined, DataFacade.GetDataFacade().ReportActiveUsers));
            OperationList.Add(new Operation("Report: Users, who are active now", DataContracts.AccessRole.ProjectLeader, true, 13, State.Logined, DataFacade.GetDataFacade().UserServicesObj.GetAllActiveUsers));
            OperationList.Add(new Operation("Quit", DataContracts.AccessRole.Any, true, 0, State.Any, DataFacade.GetDataFacade().Quit));
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

            
            State StateUser;
            AccessRole RoleUser;
            if (UserDataObj == null)
            {
                RoleUser = AccessRole.Any;
                StateUser = State.NotLogined;
            }
            else
            {
                RoleUser = UserDataObj.UserObj.Role;
                StateUser = State.Logined;
            }

            foreach (Operation op in OperationList)
            {
                if (answer== op.NumberOpreation)
                {
                    if ((op.StateLogin == StateUser || op.StateLogin == State.Any) && (op.AvailableFor == RoleUser || op.AvailableFor == DataContracts.AccessRole.Any))
                    {
                        //if (user == null)
                        //    op.DoOperation(null);
                        //else
                        op.DoOperation(user);
                        return true;
                    }
                }         
                
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


            State StateUser;
            AccessRole RoleUser;
            if (UserDataObj == null)
            {
                RoleUser = AccessRole.Any;
                StateUser = State.NotLogined;
            }
            else
            {
                RoleUser = UserDataObj.UserObj.Role;
                StateUser = State.Logined;
            }      

            StringBuilder Result = new();
            Result.AppendLine();
            Result.AppendLine("Available operations");
            foreach (Operation op in OperationList)
            {
                if ((op.StateLogin == StateUser || op.StateLogin == State.Any) && (op.AvailableFor == RoleUser || op.AvailableFor == DataContracts.AccessRole.Any))
                {
                    Result.AppendLine(op.Name + " " +op.NumberOpreation.ToString());
                }
            }
            return Result.ToString();
        }
    }
}
 