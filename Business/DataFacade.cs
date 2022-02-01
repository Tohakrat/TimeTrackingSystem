using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Business
{
    public class DataFacade
    {
        public DataFacade()
        {
            Seed();
        }
        //UserServices UserServices { get; set; }
        public UserServices UserServicesObj = new UserServices();
        public ProjectServices ProjectServicesObj = new ProjectServices();
        //TimeTrackEntryServices TimeTrackEntryServicesObj = new TimeTrackEntryServices();
        private void Seed()
        {
            UserServicesObj.Add(new User("Vasia", "1234", AccessRole.User));
            UserServicesObj.Add(new User("Petia", "1234", AccessRole.User));
            UserServicesObj.Add(new User("Vlad", "1234", AccessRole.Admin));
            UserServicesObj.Add(new User("Ivan", "1234", AccessRole.ProjectLeader));

        }
    }
}
