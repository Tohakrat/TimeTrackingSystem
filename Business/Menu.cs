﻿using DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Text;

namespace Business
{
    internal class Menu
    {
        List<Operation> OperationList = new();
        internal Menu()
        {
            SeedMenu();
        }
        
        private void SeedMenu()
        {
            OperationList.Add(new Operation("LogIn", DataContracts.AccessRole.Any, true,1, State.NotLogined));
            OperationList.Add(new Operation("LogOut", DataContracts.AccessRole.Any, false,2, State.Logined));
            OperationList.Add(new Operation("Available Projects", DataContracts.AccessRole.Any, true,3, State.Logined));
            OperationList.Add(new Operation("Submit Time", DataContracts.AccessRole.Any, true,4, State.Logined));
            OperationList.Add(new Operation("View Submitted Time", DataContracts.AccessRole.Any, true,5, State.Logined));
            //OperationList.Add(new Operation("Run Report", DataContracts.AccessRole.ProjectLeader, true,6, State.Logined));
            OperationList.Add(new Operation("Create User", DataContracts.AccessRole.Admin, true,7, State.Logined));
            OperationList.Add(new Operation("Delete User", DataContracts.AccessRole.Admin, true,8, State.Logined));
            OperationList.Add(new Operation("Create Project", DataContracts.AccessRole.Admin, true,9, State.Logined));
            OperationList.Add(new Operation("Delete Project", DataContracts.AccessRole.Admin, true,10, State.Logined));
            OperationList.Add(new Operation("View All Users", DataContracts.AccessRole.Any, true,11, State.Logined));
            OperationList.Add(new Operation("Report: Active Users in Project", DataContracts.AccessRole.ProjectLeader, true,12, State.Logined));
            OperationList.Add(new Operation("Report: Users, who are active now", DataContracts.AccessRole.ProjectLeader, true,13, State.Logined));


            OperationList.Add(new Operation("Quit", DataContracts.AccessRole.Any, true,0, State.Any));       
        }

        internal bool CheckAnswer(int answer, AccessRole role,State state)
        {

            foreach (Operation op in OperationList)
            {
                if (answer== op.NumberOpreation)
                {
                    if ((op.StateLogin == state || op.StateLogin == State.Any) && (op.AvailableFor == role || op.AvailableFor == DataContracts.AccessRole.Any))
                    {
                        return true;
                    }
                }         
                
            }
            return false;
        }

        internal string GetAvailableOperations(DataContracts.AccessRole role,State state)
        {
            StringBuilder Result = new();
            Result.AppendLine();
            Result.AppendLine("Available operations");
            foreach (Operation op in OperationList)
            {
                if ((op.StateLogin == state || op.StateLogin == State.Any) && (op.AvailableFor == role || op.AvailableFor == DataContracts.AccessRole.Any))
                {
                    Result.AppendLine(op.Name + " " +op.NumberOpreation.ToString());
                }
            }
            return Result.ToString();
        }
    }
}
 