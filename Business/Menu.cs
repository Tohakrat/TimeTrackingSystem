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
            OperationList.Add(new Operation("AvailableProjects", DataContracts.AccessRole.Any, true,3, State.Logined));
            OperationList.Add(new Operation("SubmitTime", DataContracts.AccessRole.Any, true,4, State.Logined));
            OperationList.Add(new Operation("ViewSubmittedTime", DataContracts.AccessRole.Any, true,5, State.Logined));
            OperationList.Add(new Operation("RunReport", DataContracts.AccessRole.ProjectLeader, true,6, State.Logined));
            OperationList.Add(new Operation("CreateUser", DataContracts.AccessRole.Admin, true,7, State.Logined));
            OperationList.Add(new Operation("DeleteUser", DataContracts.AccessRole.Admin, true,8, State.Logined));
            OperationList.Add(new Operation("CreateProject", DataContracts.AccessRole.Admin, true,9, State.Logined));
            OperationList.Add(new Operation("DeleteProject", DataContracts.AccessRole.Admin, true,10, State.Logined));
            OperationList.Add(new Operation("Quit", DataContracts.AccessRole.Any, true,0, State.Any));       
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
 