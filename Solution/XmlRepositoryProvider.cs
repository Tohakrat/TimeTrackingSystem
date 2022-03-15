using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Repositories.Xml;
using DataContracts;


namespace Solution
{
    internal class XmlRepositoryProvider : AbstractRepositoryProvider
    {
        public override IRepository<User> GetUserRepository()
        {
            return new XmlObjectRepository<User>();
        }

        public override IRepository<Project> GetProjectRepository()
        {
            return new XmlObjectRepository<Project>();
        }
        public override IRepository<TimeTrackEntry> GetTimeTrackEntryRepository()
        {
            return new XmlObjectRepository<TimeTrackEntry>();
        }
    }
}
