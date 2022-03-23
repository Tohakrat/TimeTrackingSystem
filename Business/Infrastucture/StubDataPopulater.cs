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
            DataFacade.Instance.ProjectServices.AddObject(new Project() { Id = 0, Name = "TimeTrackingSystem", ExpirationDate = DateTime.Now, MaxHours = 200, LeaderUserId = 6 });
            DataFacade.Instance.ProjectServices.AddObject(new Project() { Id = 1, Name = "EnsuranceSystem", ExpirationDate = DateTime.Now, MaxHours = 400, LeaderUserId = 7 });
            DataFacade.Instance.ProjectServices.AddObject(new Project() { Id = 2, Name = "GamblingSystem", ExpirationDate = DateTime.Now, MaxHours = 750, LeaderUserId = 6 });
            DataFacade.Instance.ProjectServices.AddObject(new Project() { Id = 3, Name = "EnergySystem", ExpirationDate = DateTime.Now, MaxHours = 470, LeaderUserId = 7 });
            DataFacade.Instance.ProjectServices.AddObject(new Project() { Id = 4, Name = "UniversitySystem", ExpirationDate = DateTime.Now, MaxHours = 900, LeaderUserId = 6 });
            DataFacade.Instance.ProjectServices.AddObject(new Project() { Id = 5, Name = "p5", ExpirationDate = DateTime.Now, MaxHours = 220, LeaderUserId = 7 });
            DataFacade.Instance.ProjectServices.AddObject(new Project() { Id = 6, Name = "p6", ExpirationDate = DateTime.Now, MaxHours = 420, LeaderUserId = 6 });
            DataFacade.Instance.ProjectServices.AddObject(new Project() { Id = 7, Name = "p7", ExpirationDate = DateTime.Now, MaxHours = 620, LeaderUserId = 7 });

            DataFacade.Instance.UserServices.AddObject(new User() { Id = 0, UserName = "Vasia", PassWord = "1234", Role = AccessRole.User, FullName = "Vasily Ivanovich" });
            DataFacade.Instance.UserServices.AddObject(new User() { Id = 1, UserName = "Petia", PassWord = "1234", Role = AccessRole.User, FullName = "Petr Iosifovich" });
            DataFacade.Instance.UserServices.AddObject(new User() { Id = 2, UserName = "u", PassWord = "u", Role = AccessRole.User, FullName = "Uriy Nickolaevich" });
            DataFacade.Instance.UserServices.AddObject(new User() { Id = 3, UserName = "w", PassWord = "w", Role = AccessRole.User, FullName = "William" });
            DataFacade.Instance.UserServices.AddObject(new User() { Id = 4, UserName = "Vlad", PassWord = "1234", Role = AccessRole.Admin, FullName = "Vladimir Ilyich"});
            DataFacade.Instance.UserServices.AddObject(new User() { Id = 5, UserName = "a", PassWord = "a", Role = AccessRole.Admin, FullName = "Andrey Mihailovich"});
            DataFacade.Instance.UserServices.AddObject(new User() { Id = 6, UserName = "Vania", PassWord = "1234", Role = AccessRole.ProjectLeader, FullName = "Ivan Vladimirovich"});
            DataFacade.Instance.UserServices.AddObject(new User() { Id = 7, UserName = "p", PassWord = "p", Role = AccessRole.ProjectLeader, FullName = "Patrick" });

            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 0, ProjectId = 5, Value = 15, Date = DateTime.Parse("1.02.2022") });
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 0, ProjectId = 5, Value = 20, Date = DateTime.Parse("1.01.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 0, ProjectId = 5, Value = 5, Date = DateTime.Parse("15.01.2021")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 1, ProjectId = 6, Value = 15, Date = DateTime.Parse("1.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 1, ProjectId = 5, Value = 11, Date = DateTime.Parse("3.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 1, ProjectId = 5, Value = 9, Date = DateTime.Parse("1.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 1, ProjectId = 6, Value = 15, Date = DateTime.Parse("1.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 2, ProjectId = 6, Value = 11, Date = DateTime.Parse("1.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 2, ProjectId = 7, Value = 50, Date = DateTime.Parse("3.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 3, ProjectId = 7, Value = 15, Date = DateTime.Parse("4.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 3, ProjectId = 7, Value = 11, Date = DateTime.Parse("1.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 3, ProjectId = 7, Value = 19, Date = DateTime.Parse("1.02.2022")});
            DataFacade.Instance.UserServices.AddTimeTrackEnty(new TimeTrackEntry() { UserId = 6, ProjectId = 6, Value = 29, Date = DateTime.Parse("21.06.2021") });


        }
    }
}
