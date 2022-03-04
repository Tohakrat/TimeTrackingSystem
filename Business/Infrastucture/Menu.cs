using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;


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
            OperationList.Add(new Operation("LogIn", AccessRole.Any, true,1, State.NotLogined,DataFacade.GetDataFacade().UserServicesObj.Login));
            OperationList.Add(new Operation("LogOut", AccessRole.Any, false, 2, State.Logined, DataFacade.GetDataFacade().UserServicesObj.LogOut));
            OperationList.Add(new Operation("Available Projects", AccessRole.Any, true, 3, State.Logined, DataFacade.GetDataFacade().ProjectServicesObj.GetProjectsString));
            OperationList.Add(new Operation("Submit Time", AccessRole.Any, true, 4, State.Logined, DataFacade.GetDataFacade().UserServicesObj.SubmitTime));
            OperationList.Add(new Operation("View Submitted Time", AccessRole.Any, true, 5, State.Logined, DataFacade.GetDataFacade().UserServicesObj.ViewSubmittedTime));
            OperationList.Add(new Operation("View All Users", AccessRole.Any, true, 6, State.Logined, DataFacade.GetDataFacade().UserServicesObj.GetAllUsersString));
            OperationList.Add(new Operation("Create User", AccessRole.Admin, true, 7, State.Logined, DataFacade.GetDataFacade().UserServicesObj.Add));
            OperationList.Add(new Operation("Delete User", AccessRole.Admin, true, 8, State.Logined, DataFacade.GetDataFacade().UserServicesObj.DeleteUser));
            OperationList.Add(new Operation("Create Project", AccessRole.Admin, true, 9, State.Logined, DataFacade.GetDataFacade().ProjectServicesObj.AddProject));
            OperationList.Add(new Operation("Delete Project", AccessRole.Admin, true, 10, State.Logined, DataFacade.GetDataFacade().ProjectServicesObj.DeleteProject));
            OperationList.Add(new Operation("Report: Active Users in Project", AccessRole.ProjectLeader, true, 12, State.Logined, DataFacade.GetDataFacade().ReportActiveUsers));
            OperationList.Add(new Operation("Report: Users, who are active now", AccessRole.ProjectLeader, true, 13, State.Logined, DataFacade.GetDataFacade().UserServicesObj.GetAllActiveUsers));
            OperationList.Add(new Operation("Quit", AccessRole.Any, true, 0, State.Any, DataFacade.GetDataFacade().Quit));
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

            Operation operation = OperationList.Single(x => x.NumberOpreation == answer);

            //if operation.AvailableFor== UserDataObj.UserObj.
            //// here are some checks
            //operation.DoOperation(user);







            foreach (Operation op in OperationList)
            {
                if (answer== op.NumberOpreation)
                {
                    if ((op.StateLogin == StateUser || op.StateLogin == State.Any) && (op.AvailableFor == RoleUser || op.AvailableFor == AccessRole.Any))
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
                if ((op.StateLogin == StateUser || op.StateLogin == State.Any) && (op.AvailableFor == RoleUser || op.AvailableFor == AccessRole.Any))
                {
                    Result.AppendLine(op.Name + " " +op.NumberOpreation.ToString());
                }
            }
            return Result.ToString();
        }
    }
}
 