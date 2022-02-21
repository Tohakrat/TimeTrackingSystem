using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Business
{
    internal class StubDataPopulater
    {
        UserServices UserServicesObj;
        ProjectServices ProjectServicesObj;
        internal StubDataPopulater(UserServices userServices, ProjectServices projectServices)
        {
            UserServicesObj = userServices;
            ProjectServicesObj = projectServices;
        }
        internal void Populate()
        {            
            ProjectServicesObj.AddObject(new ProjectData(0, "TimeTrackingSystem", DateTime.Now, 200, 6));
            ProjectServicesObj.AddObject(new ProjectData(1, "EnsuranceSystem", DateTime.Now, 400, 7));
            ProjectServicesObj.AddObject(new ProjectData(2, "GamblingSystem", DateTime.Now, 750, 6));
            ProjectServicesObj.AddObject(new ProjectData(3, "EnergySystem", DateTime.Now, 470, 7));
            ProjectServicesObj.AddObject(new ProjectData(4, "UniversitySystem", DateTime.Now, 900, 6));
            ProjectServicesObj.AddObject(new ProjectData(5, "p5", DateTime.Now, 220, 7));
            ProjectServicesObj.AddObject(new ProjectData(6, "p6", DateTime.Now, 420, 6));
            ProjectServicesObj.AddObject(new ProjectData(7, "p7", DateTime.Now, 620, 7));

            UserServicesObj.AddObject(new UserData(0, "Vasia", "1234", AccessRole.User, "Vasily Ivanovich"));
            UserServicesObj.AddObject(new UserData(1, "Petia", "1234", AccessRole.User, "Petr Iosifovich"));
            UserServicesObj.AddObject(new UserData(2, "u", "u", AccessRole.User, "Uriy Nickolaevich"));
            UserServicesObj.AddObject(new UserData(3, "w", "w", AccessRole.User, "William"));
            UserServicesObj.AddObject(new UserData(4, "Vlad", "1234", AccessRole.Admin, "Vladimir Ilyich"));
            UserServicesObj.AddObject(new UserData(5, "a", "a", AccessRole.Admin, "Andrey Mihailovich"));
            UserServicesObj.AddObject(new UserData(6, "Vania", "1234", AccessRole.ProjectLeader, "Ivan Vladimirovich"));
            UserServicesObj.AddObject(new UserData(7, "p", "p", AccessRole.ProjectLeader, "Patrick"));

            UserServicesObj.SeedTimeTrackEntry();
            //UserDataList[0].AddSubmittedTime(new TimeTrackEntry(0, 5, 15, DateTime.Parse("1.02.2022")));
            //UserDataList[0].AddSubmittedTime(new TimeTrackEntry(0, 5, 20, DateTime.Parse("1.01.2022")));
            //UserDataList[0].AddSubmittedTime(new TimeTrackEntry(0, 5, 5, DateTime.Parse("15.01.2021")));
            //UserDataList[1].AddSubmittedTime(new TimeTrackEntry(1, 6, 15, DateTime.Parse("1.02.2022")));
            //UserDataList[1].AddSubmittedTime(new TimeTrackEntry(1, 5, 11, DateTime.Parse("3.02.2022")));
            //UserDataList[1].AddSubmittedTime(new TimeTrackEntry(1, 5, 9, DateTime.Parse("1.02.2022")));
            //UserDataList[2].AddSubmittedTime(new TimeTrackEntry(1, 6, 15, DateTime.Parse("1.02.2022")));
            //UserDataList[2].AddSubmittedTime(new TimeTrackEntry(2, 6, 11, DateTime.Parse("1.02.2022")));
            //UserDataList[2].AddSubmittedTime(new TimeTrackEntry(2, 7, 50, DateTime.Parse("3.02.2022")));
            //UserDataList[3].AddSubmittedTime(new TimeTrackEntry(3, 7, 15, DateTime.Parse("4.02.2022")));
            //UserDataList[3].AddSubmittedTime(new TimeTrackEntry(3, 7, 11, DateTime.Parse("1.02.2022")));
            //UserDataList[3].AddSubmittedTime(new TimeTrackEntry(3, 7, 19, DateTime.Parse("1.02.2022")));
            //UserDataList[6].AddSubmittedTime(new TimeTrackEntry(6, 6, 29, DateTime.Parse("21.06.2021")));



        }
    }
}
