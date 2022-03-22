using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using Infrastructure;

namespace Business
{
    internal class StubDataPopulater
    {
        
        internal StubDataPopulater()//UserServices userServices, ProjectServices projectServices)
        {
            
        }
        internal void Populate()
        {            
            DataFacade.Instance.ProjectServices.AddObject(new Project(0, "TimeTrackingSystem", DateTime.Now, 200, 6));
            DataFacade.Instance.ProjectServices.AddObject(new Project(1, "EnsuranceSystem", DateTime.Now, 400, 7));
            DataFacade.Instance.ProjectServices.AddObject(new Project(2, "GamblingSystem", DateTime.Now, 750, 6));
            DataFacade.Instance.ProjectServices.AddObject(new Project(3, "EnergySystem", DateTime.Now, 470, 7));
            DataFacade.Instance.ProjectServices.AddObject(new Project(4, "UniversitySystem", DateTime.Now, 900, 6));
            DataFacade.Instance.ProjectServices.AddObject(new Project(5, "p5", DateTime.Now, 220, 7));
            DataFacade.Instance.ProjectServices.AddObject(new Project(6, "p6", DateTime.Now, 420, 6));
            DataFacade.Instance.ProjectServices.AddObject(new Project(7, "p7", DateTime.Now, 620, 7));

            DataFacade.Instance.UserServices.AddObject(new User(0, "Vasia", "1234", AccessRole.User, "Vasily Ivanovich"));
            DataFacade.Instance.UserServices.AddObject(new User(1, "Petia", "1234", AccessRole.User, "Petr Iosifovich"));
            DataFacade.Instance.UserServices.AddObject(new User(2, "u", "u", AccessRole.User, "Uriy Nickolaevich"));
            DataFacade.Instance.UserServices.AddObject(new User(3, "w", "w", AccessRole.User, "William"));
            DataFacade.Instance.UserServices.AddObject(new User(4, "Vlad", "1234", AccessRole.Admin, "Vladimir Ilyich"));
            DataFacade.Instance.UserServices.AddObject(new User(5, "a", "a", AccessRole.Admin, "Andrey Mihailovich"));
            DataFacade.Instance.UserServices.AddObject(new User(6, "Vania", "1234", AccessRole.ProjectLeader, "Ivan Vladimirovich"));
            DataFacade.Instance.UserServices.AddObject(new User(7, "p", "p", AccessRole.ProjectLeader, "Patrick"));

            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(0, 5, 15, DateTime.Parse("1.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(0, 5, 20, DateTime.Parse("1.01.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(0, 5, 5,  DateTime.Parse("15.01.2021")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(1, 6, 15, DateTime.Parse("1.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(1, 5, 11, DateTime.Parse("3.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(1, 5, 9,  DateTime.Parse("1.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(1, 6, 15, DateTime.Parse("1.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(2, 6, 11, DateTime.Parse("1.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(2, 7, 50, DateTime.Parse("3.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(3, 7, 15, DateTime.Parse("4.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(3, 7, 11, DateTime.Parse("1.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(3, 7, 19, DateTime.Parse("1.02.2022")));
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry(6, 6, 29, DateTime.Parse("21.06.2021")));


        }
    }
}
